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
    public class StatesController : Controller
    {
        RestClient RestClient { get; set; }

        public StatesController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            RestClient = new RestClient("https://localhost:44385/");
        }

        // GET: States
        public async Task<ActionResult> Index()
        {
            RestRequest restRequest = new RestRequest("api/States", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<List<State>>(restRequest);
            List<State> states = response.Data;

            return View(states);
        }

        // GET: States/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/States/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<State>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            State state = response.Data;
            return View(state);
        }

        // GET: States/Create
        public async Task<ActionResult> Create()
        {
            RestRequest restRequest = new RestRequest("api/Countries", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<List<Country>>(restRequest);
            List<Country> countries = response.Data;
            ViewData["countries"] = countries;
            return View();
        }

        // POST: States/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StateRequest state)
        {
            try
            {
                RestRequest restRequest = new RestRequest("api/States", Method.POST);
                restRequest.AddHeader("content-type", "multipart/form-data");
                restRequest.AddParameter("name", state.Name);
                restRequest.AddParameter("countryId", state.CountryId);
                using (var ms = new MemoryStream())
                {
                    state.PhotoUrl.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    restRequest.AddFile("PhotoUrl", fileBytes, state.PhotoUrl.FileName, state.PhotoUrl.ContentType);
                }
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception("Not Created");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                RestRequest restRequest = new RestRequest("api/Countries", Method.GET, DataFormat.Json);
                var response = await RestClient.ExecuteAsync<List<Country>>(restRequest);
                List<Country> countries = response.Data;
                ViewData["countries"] = countries;
                return View();
            }
        }

        // GET: States/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/States/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Country>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Country country = response.Data;
            return View(country);
        }

        // POST: States/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, State state)
        {
            try
            {
                RestRequest restRequest = new RestRequest($"api/States", Method.PUT, DataFormat.Json);
                restRequest.AddJsonBody(state);
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Not Created");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                RestRequest restRequest = new RestRequest($"api/States/{id}", Method.GET, DataFormat.Json);
                var response = await RestClient.ExecuteAsync<Country>(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Message = "Not found";
                    return RedirectToAction(nameof(Index));
                }

                Country country = response.Data;
                return View(country);
            }
        }

        // GET: States/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/States/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<State>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            State state = response.Data;
            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                RestRequest restRequest = new RestRequest($"api/States/{id}", Method.DELETE, DataFormat.Json);
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Not Created");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                RestRequest restRequest = new RestRequest($"api/States/{id}", Method.GET, DataFormat.Json);
                var response = await RestClient.ExecuteAsync<State>(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Message = "Not found";
                    return RedirectToAction(nameof(Index));
                }

                State state = response.Data;
                return View(state);
            }
        }
    }
}