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
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace QuadAuto.Server
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly().BasedOn<IApplicationService>().WithServiceFirstInterface());

            container.Register(
                AllTypes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.BaseDirectory,
                                                                    "QuadAuto.Server.*.dll"))
                    .BasedOn<IApplicationService>().WithServiceFirstInterface());
        }
    }
}