using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitee.Repositories;
using websitee.Models;
using websitee.ViewModels;

namespace websitee.Controllers
{
    public class HomeController : Controller
    {
        public IHomeRepository homeRepository;


        public HomeController()
        {
            homeRepository = new HomeRepository(new ApplicationDbContext());
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("IsAdmin")) return View("AdminDashboard");
                return View("UserDashboard");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "IsAdmin")]
        [HttpPost]
        public ActionResult AddResolution(HelpModels helpData)
        {
            return View("Help", helpData);
        }

        [Authorize]
        public ActionResult History()
        {
            IEnumerable<HelpModels> helpRecords = homeRepository.GetHelpRecordByEmail(System.Web.HttpContext.Current.User.Identity.Name);
            var viewModel = new NewHelpViewModel
            {
                HelpModelRecord = helpRecords
            };
            return View("HelpResolve", viewModel);
        }

        //get :help
        [Authorize]
        public ActionResult Help()
        {
            return View();
        }
        // POST: help
        [Authorize]
        [HttpPost]
        public ActionResult Help(HelpModels helpData)
        {
            if(helpData.HelpId != Guid.Empty)
            {
                homeRepository.UpdateHelpAdminResolution(helpData);
                homeRepository.Save();
                return RedirectToAction("Index");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    homeRepository.InsertHelpRecord(helpData);
                    homeRepository.Save();
                    return RedirectToAction("Index");
                }
            }catch(Exception e)
            {
            }
            return View();
        }


        //get :resolvehelp
        [Authorize]
        public ActionResult HelpResolve()
        {
            IEnumerable<HelpModels> helpRecords = homeRepository.GetHelpRecords();
            var viewModel = new NewHelpViewModel
            {
                HelpModelRecord = helpRecords
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult ViewSingleHelpRequest(Guid helpId)
        {
            if(helpId != Guid.Empty)
            {
                HelpModels helpRecord = homeRepository.GetHelpRecordById(helpId);
                return View(helpRecord);
            }
            return RedirectToAction("HelpResolve");
        }

    }
}