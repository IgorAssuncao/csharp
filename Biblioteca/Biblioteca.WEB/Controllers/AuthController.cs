using System;
using System.Net;
using System.Threading.Tasks;
using Biblioteca.Models;
using Biblioteca.WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Biblioteca.WEB.Controllers
{
    public class AuthController : Controller
    {
        RestClient restClient { get; set; }

        public AuthController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            restClient = new RestClient("http://localhost:5002/");
        }

        // GET: Auth/Create
        public ActionResult Index()
        {
            string token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return View("Login");
            }

            return RedirectToAction(nameof(AuthorsController.Index), "Authors");
        }

        // POST: Auth/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
                    return RedirectToAction(nameof(AuthorsController.Index), "Authors");

                RestRequest restRequest = new RestRequest("api/Auth", Method.POST, DataFormat.Json);
                restRequest.AddJsonBody(loginRequest);
                var response = await restClient.ExecuteAsync<LoginResponse>(restRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new Exception("Wrong email or password");

                HttpContext.Session.SetString("Token", response.Data.Token);
                return RedirectToAction(nameof(AuthorsController.Index), "Authors");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            return View("Login");
        }

        public ActionResult Register()
        {
            return RedirectToAction(nameof(AuthorsController.Create), "Authors");
        }
    }
}