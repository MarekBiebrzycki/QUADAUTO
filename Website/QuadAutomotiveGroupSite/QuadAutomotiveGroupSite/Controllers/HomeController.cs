// Copyright (c)  2012 QuadAutomotive Group.
// All rights reserved.
// 
// Redistribution and use in source and binary forms are permitted
// provided that the above copyright notice and this paragraph are
// duplicated in all such forms and that any documentation,
// advertising materials, and other materials related to such
// distribution and use acknowledge that the software was developed
// by the <organization>.  The name of the
// University may not be used to endorse or promote products derived
// from this software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED ``AS IS'' AND WITHOUT ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.

using System.Web.Mvc;
using QuadAuto.Web.Controllers.Committees.QueryModels;
using QuadAutomotiveGroupSite.ControllerServices;

namespace QuadAutomotiveGroupSite.Controllers
{
    public class HomeController : Controller
    {
        private CommitteesService _vs;

        public HomeController(CommitteesService service)
        {
            _vs = service;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
           
            return View(DataProvider.DataProvider.GetQueryModel(typeof(CommitteesQueryModel)));
        }

        public ActionResult About()
        {
            return View();
        }
    }
}