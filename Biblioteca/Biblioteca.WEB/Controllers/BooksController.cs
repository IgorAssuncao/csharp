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
    public class BooksController : Controller
    {
        RestClient restClient { get; set; }

        public BooksController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            restClient = new RestClient("http://localhost:5002/");
        }

        // GET: Books
        public async Task<ActionResult> Index()
        {
            restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
            RestRequest restRequest = new RestRequest("api/Books", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync<List<BookMapping>>(restRequest);
            var authors = response.Data;
            return View(authors);
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
            RestRequest restRequest = new RestRequest($"api/Books/{id}", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync<BookMapping>(restRequest);
            var book = response.Data;
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.Message = "Not found";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Create
        public async Task<ActionResult> Create()
        {
            restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
            RestRequest restRequest = new RestRequest("api/Authors", Method.GET, DataFormat.Json);
            var response = await restClient.ExecuteAsync<List<AuthorMapping>>(restRequest);
            var authors = response.Data;
            BookMapping book = new BookMapping() { Authors = new List<Guid>() };
            foreach (var author in authors)
            {
                book.Authors.Add(author.Id);
            }
            return View(book);
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookMapping book)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest("api/Books/", Method.POST, DataFormat.Json);
                restRequest.AddJsonBody(book);
                var response = await restClient.ExecuteAsync<BookMapping>(restRequest);
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

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Books/{id}", Method.GET, DataFormat.Json);
                var response = await restClient.ExecuteAsync<BookMapping>(restRequest);
                var bookRes = response.Data;
                var book = new Book()
                {
                    Id = bookRes.Id,
                    Title = bookRes.Title,
                    ISBN = bookRes.ISBN,
                    Year = bookRes.Year
                };
                return View(book);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Book book)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Books/{id}", Method.PUT, DataFormat.Json);
                restRequest.AddJsonBody(book);
                var response = await restClient.ExecuteAsync<BookMapping>(restRequest);
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

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Books/{id}", Method.GET, DataFormat.Json);
                var response = await restClient.ExecuteAsync<BookMapping>(restRequest);
                var bookRes = response.Data;
                var book = new Book()
                {
                    Id = bookRes.Id,
                    Title = bookRes.Title,
                    ISBN = bookRes.ISBN,
                    Year = bookRes.Year
                };
                return View(book);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Book book)
        {
            try
            {
                restClient.Authenticator = new JwtAuthenticator(HttpContext.Session.GetString("Token"));
                RestRequest restRequest = new RestRequest($"api/Books/{id}", Method.DELETE, DataFormat.Json);
                var response = await restClient.ExecuteAsync<BookMapping>(restRequest);
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