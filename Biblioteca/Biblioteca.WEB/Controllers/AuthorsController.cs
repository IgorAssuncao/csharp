using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Biblioteca.Mappings;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;

namespace Biblioteca.WEB.Controllers
{
    // [Authorize]
    public class AuthorsController : Controller
    {
        RestClient restClient { get; set; }

        public AuthorsController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            restClient = new RestClient("http://localhost:5002/");
        }

        // GET: Authors
        public async Task<ActionResult> Index()
        {
            restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
            RestRequest restRequest = new RestRequest("api/Authors", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync<List<AuthorMapping>>(restRequest);
            var authors = response.Data;
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
            RestRequest restRequest = new RestRequest($"api/Authors/{id}", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync<AuthorMapping>(restRequest);
            var author = response.Data;
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest("api/Authors/", Method.POST, DataFormat.Json);
                restRequest.AddJsonBody(author);
                var response = await restClient.ExecuteAsync<AuthorMapping>(restRequest);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Failed");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Authors/{id}", Method.GET, DataFormat.Json);
                var response = await restClient.ExecuteAsync<AuthorMapping>(restRequest);
                var authorRes = response.Data;
                var author = new Author()
                {
                    Id = authorRes.Id,
                    Email = authorRes.Email,
                    Name = authorRes.Name,
                    Lastname = authorRes.Lastname,
                    Birthday = authorRes.Birthday
                };
                return View(author);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Author author)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Authors/{id}", Method.PUT, DataFormat.Json);
                restRequest.AddJsonBody(author);
                var response = await restClient.ExecuteAsync<AuthorMapping>(restRequest);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Failed");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Authors/{id}", Method.GET, DataFormat.Json);
                var response = await restClient.ExecuteAsync<AuthorMapping>(restRequest);
                var authorRes = response.Data;
                var author = new Author()
                {
                    Id = authorRes.Id,
                    Email = authorRes.Email,
                    Name = authorRes.Name,
                    Lastname = authorRes.Lastname,
                    Birthday = authorRes.Birthday
                };
                return View(author);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Authors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Author author)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Authors/{id}", Method.DELETE, DataFormat.Json);
                var response = await restClient.ExecuteAsync<AuthorMapping>(restRequest);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Failed");
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