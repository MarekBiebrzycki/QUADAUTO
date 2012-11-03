using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuadAutomotiveGroupSite.ControllerServices;

namespace QuadAutomotiveGroupSite.Controllers
{
    public class MemberApplicationController : Controller
    {
        private MemberApplicationService _vs;
        //
        // GET: /MemberApplication/
        public MemberApplicationController(MemberApplicationService vs)
        {
            _vs = vs;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitApplication(string FirstName, string LastName, string EmailAddress, string BirthDate, string HowWillIContributeToTheProject, string SkillsBackground, string CardNumber,string ExpMonth, string ExpYear,string Cvv)
        {
            _vs.SubmitApplication(FirstName,LastName,EmailAddress,BirthDate,HowWillIContributeToTheProject,SkillsBackground,_vs.ProcessPayment(FirstName,LastName,EmailAddress,CardNumber,ExpMonth,ExpYear,Cvv));
            return View();
        }
    }
}
