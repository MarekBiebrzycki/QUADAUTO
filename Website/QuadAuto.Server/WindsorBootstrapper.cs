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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Topshelf.Configuration.Dsl;
using Topshelf.Shelving;

namespace QuadAuto.Server
{
    public abstract class WindsorBootstrapper<T> : Bootstrapper<T>, IDisposable where T : class
    {
        protected IWindsorContainer Container { get; set; }

        protected WindsorBootstrapper()
        {
            Container = GetContainer();
            RegisterModels();
        }

        public abstract void InitializeHostedService(IServiceConfigurator<T> cfg);

        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
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

        private static IWindsorContainer GetContainer()
        {
            var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            var container = new WindsorContainer();
            var installers = new List<IWindsorInstaller>();

            container.Kernel.ComponentModelBuilder.AddContributor(new RequireRepositoryProperties());

            var windsorConfigFile = Path.Combine(currentFolder, "windsor.xml");
            if (File.Exists(windsorConfigFile))
                installers.Add(Configuration.FromXmlFile(windsorConfigFile));

            installers.AddRange(new[]
                                    {
                                        FromAssembly.InThisApplication(),
                                        FromAssembly.InDirectory(new AssemblyFilter(currentFolder,
                                                                                    "QuadAuto.Server.*.dll"))
                                    });

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
            container.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>());
            container.Register(Component.For<IServerStart>().ImplementedBy<ServerStart>());
            container.Install(installers.ToArray());

            return container;
        }
    }
}