using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using Service;
using TP1_Session.Models;

namespace TP1_Session.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IFriendService FriendService { get; set; }

        public HomeController(ILogger<HomeController> logger, IFriendService _friendService)
        {
            _logger = logger;
            FriendService = _friendService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("FriendsEmail");
        }

        public IActionResult FriendsEmail()
        {
            List<Friend> Friends = FriendService.GetFriends();
            foreach(Friend friend in Friends)
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(friend.Id.ToString())))
                {
                    HttpContext.Session.SetString(friend.Id.ToString(), "false");
                }
            }
            return View("FriendsEmail", Friends);
        }

        public IActionResult FriendsBirthday()
        {
            List<Friend> Friends = FriendService.GetFriends();
            return View("FriendsBirthday", Friends);
        }

        [HttpPost]
        public IActionResult HandleSelection(SelectionOptions options)
        {
            List<Friend> Friends = FriendService.GetFriends();

            var sessionString = HttpContext.Session.GetString(options.Id);

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(options.Id)))
            {
                HttpContext.Session.SetString(options.Id, options.NewValue);
            }

            return View(options.ViewName, Friends);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class SelectionOptions
    {
        public string Id { get; set; }
        public string NewValue { get; set; }
        public string ViewName { get; set; }

        public SelectionOptions()
        {
        }

        public SelectionOptions(string _Id, string _NewValue, string _ViewName)
        {
            Id = _Id;
            NewValue = _NewValue;
            ViewName = _ViewName;
        }
    }
}
