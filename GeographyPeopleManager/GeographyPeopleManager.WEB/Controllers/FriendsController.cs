using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using RestSharp;

namespace GeographyPeopleManager.WEB.Controllers
{
    public class FriendsController : Controller
    {
        RestClient RestClient { get; set; }
        RestClient GeoClient { get; set; }

        public FriendsController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            RestClient = new RestClient("https://localhost:44319/");
            GeoClient = new RestClient("https://localhost:44385/");
        }

        // GET: Friends
        public async Task<ActionResult> Index()
        {
            RestRequest restRequest = new RestRequest("api/Friends", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<List<Friend>>(restRequest);
            List<Friend> friends = response.Data;
            return View(friends);
        }

        // GET: Friends/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/Friends/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Friend>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Friend friend = response.Data;

            RestRequest friendsRequest = new RestRequest($"api/Friends/all/{id}", Method.GET, DataFormat.Json);
            var friendsRes = await RestClient.ExecuteAsync<List<Friend>>(friendsRequest);
            var friends = new List<Friend>();
            foreach (var item in friendsRes.Data)
            {
                if (friend.Id != item.Id && !item.Friends.Any(i => i.PersonId == friend.Id))
                    friends.Add(item);
            }
            ViewData["friends"] = friends;
            return View(friend);
        }

        public async Task<ActionResult> AddFriendship(Friendship friendship)
        {
            RestRequest restRequest = new RestRequest("api/Friendships", Method.POST);
            restRequest.AddJsonBody(friendship);
            var res = await RestClient.ExecuteAsync(restRequest);

            return RedirectToAction(nameof(Details), friendship.PersonId);
        }

        // GET: Friends/Create
        public async Task<ActionResult> Create()
        {
            RestRequest restRequest = new RestRequest("api/States", Method.GET, DataFormat.Json);
            var response = await GeoClient.ExecuteAsync<List<State>>(restRequest);
            List<State> states = response.Data;
            ViewData["states"] = states;
            return View();
        }

        // POST: Friends/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FriendRequest friend)
        {
            try
            {
                RestRequest stateRequest = new RestRequest($"api/States/{friend.StateId}", Method.GET, DataFormat.Json);
                var friendRes = await GeoClient.ExecuteAsync<State>(stateRequest);
                State state = friendRes.Data;

                RestRequest restRequest = new RestRequest("api/Friends", Method.POST);
                restRequest.AddHeader("content-type", "multipart/form-data");
                restRequest.AddParameter("name", friend.Name);
                restRequest.AddParameter("lastname", friend.Lastname);
                restRequest.AddParameter("email", friend.Email);
                restRequest.AddParameter("phone", friend.Phone);
                restRequest.AddParameter("birthday", friend.Birthday);
                restRequest.AddParameter("stateId", friend.StateId);
                restRequest.AddParameter("countryId", state.CountryId);
                using (var ms = new MemoryStream())
                {
                    friend.PhotoURL.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    restRequest.AddFile("PhotoUrl", fileBytes, friend.PhotoURL.FileName, friend.PhotoURL.ContentType);
                }
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception("Not Created");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                RestRequest restRequest = new RestRequest("api/States", Method.GET, DataFormat.Json);
                var response = await GeoClient.ExecuteAsync<List<State>>(restRequest);
                List<State> states = response.Data;
                ViewData["states"] = states;
                return View();
            }
        }

        // GET: Friends/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/Friends/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Friend>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Friend friend = response.Data;
            return View(friend);
        }

        // POST: Friends/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Friend friend)
        {
            RestRequest restRequest = new RestRequest($"api/Friends/{id}", Method.PUT, DataFormat.Json);
            restRequest.AddJsonBody(friend);
            var response = await RestClient.ExecuteAsync<Friend>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Friends/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/Friends/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Friend>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Friend friend = response.Data;
            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                RestRequest restRequest = new RestRequest($"api/Friends/{id}", Method.DELETE, DataFormat.Json);
                var response = await RestClient.ExecuteAsync<Friend>(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Message = "Not found";
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}