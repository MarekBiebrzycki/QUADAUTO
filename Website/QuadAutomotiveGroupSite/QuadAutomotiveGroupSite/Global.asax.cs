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

using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using QuadAuto;
using QuadAutomotiveGroupSite.Models;
using QuadAutomotiveGroupSite.Plumbing;

namespace QuadAutomotiveGroupSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        private static IWindsorContainer container;

        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            MongoRegistration.RegisterModels();
            BootstrapContainer();
            RegisterModels();
        }

        private static void RegisterModels()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = from x in assembly.GetTypes() where typeof (IMongoInstaller).IsAssignableFrom(x) select x;
                foreach (var type in types)
                {
                    try
                    {
                        var instance = Activator.CreateInstance(type) as IMongoInstaller;
                        if (instance == null)
                            continue;
                        instance.Install();
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}