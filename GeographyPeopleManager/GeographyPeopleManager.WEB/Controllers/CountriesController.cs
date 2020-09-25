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
    public class CountriesController : Controller
    {
        RestClient RestClient { get; set; }

        public CountriesController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            RestClient = new RestClient("https://localhost:44385/");
        }

        // GET: Countries
        public async Task<ActionResult> Index()
        {
            RestRequest restRequest = new RestRequest("api/Countries", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<List<Country>>(restRequest);
            List<Country> countries = response.Data;
            return View(countries);
        }

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/Countries/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Country>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Country country = response.Data;
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CountryRequest countryRequest)
        {
            try
            {
                RestRequest restRequest = new RestRequest("api/Countries", Method.POST);
                restRequest.AddHeader("content-type", "multipart/form-data");
                restRequest.AddParameter("name", countryRequest.Name);
                using (var ms = new MemoryStream())
                {
                    countryRequest.PhotoUrl.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    restRequest.AddFile("PhotoUrl", fileBytes, countryRequest.PhotoUrl.FileName, countryRequest.PhotoUrl.ContentType);
                }
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Not Created");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Countries/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/Countries/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Country>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Country country = response.Data;
            return View(country);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Country country)
        {
            try
            {
                RestRequest restRequest = new RestRequest($"api/Countries", Method.PUT, DataFormat.Json);
                restRequest.AddJsonBody(country);
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Not Created");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            RestRequest restRequest = new RestRequest($"api/Countries/{id}", Method.GET, DataFormat.Json);
            var response = await RestClient.ExecuteAsync<Country>(restRequest);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }

            Country country = response.Data;
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Country country)
        {
            try
            {
                RestRequest restRequest = new RestRequest($"api/Countries/{id}", Method.DELETE, DataFormat.Json);
                var response = await RestClient.ExecuteAsync(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Not Created");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
    }
}