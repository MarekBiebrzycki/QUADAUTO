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
using Castle.MicroKernel;
using MassTransit;

namespace QuadAuto.Server.Services.Application
{
    public class WindsorBusService : IApplicationService, IDisposable
    {
        private IServiceBus _bus;
        private readonly IKernel _kernel;

        public WindsorBusService(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Start()
        {
            _bus = _kernel.Resolve<IServiceBus>();
        }

        public void Stop()
        {
            if (_bus == null)
                return;

            _bus.Dispose();
            _kernel.ReleaseComponent(_bus);
            _bus = null;
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Continue()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}