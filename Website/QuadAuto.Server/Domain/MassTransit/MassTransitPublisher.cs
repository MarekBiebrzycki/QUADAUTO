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
using EventStore;
using EventStore.Dispatcher;
using MassTransit;

namespace QuadAuto.Server.Domain.MassTransit
{
    public sealed class MassTransitPublisher : IDispatchCommits
    {
        private readonly IServiceBus _bus;

        public MassTransitPublisher(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Dispatch(Commit commit)
        {
            for (var i = 0; i < commit.Events.Count; i++)
            {
                var eventMessage = commit.Events[i];
                _bus.Extensions().Publish(eventMessage.Body, new Dictionary<string, string>
                                                                 {
                                                                     {"aggregate", commit.StreamId.ToString()},
                                                                     {"feed", "realtime"}
                                                                 });
            }
        }
    }
}