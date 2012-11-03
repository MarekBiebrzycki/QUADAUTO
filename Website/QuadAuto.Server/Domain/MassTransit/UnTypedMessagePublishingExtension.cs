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

using System.Collections.Generic;
using Magnum.Reflection;
using MassTransit;

namespace QuadAuto.Server.Domain.MassTransit
{
    public class UnTypedMessagePublishingExtension
    {
        private readonly IServiceBus _bus;

        public UnTypedMessagePublishingExtension(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Publish(object message)
        {
            Publish(message, new Dictionary<string, string>());
        }

        public void Publish(object message, Dictionary<string, string> headers)
        {
            this.FastInvoke(new[] {message.GetType(), headers.GetType()}, "Publish", message, headers);
        }

        protected void Publish<T>(T message, Dictionary<string, string> headers) where T : class
        {
            _bus.Publish(message, ctx =>
                                      {
                                          foreach (var header in headers)
                                              ctx.SetHeader(header.Key, header.Value);
                                      });
        }
    }
}