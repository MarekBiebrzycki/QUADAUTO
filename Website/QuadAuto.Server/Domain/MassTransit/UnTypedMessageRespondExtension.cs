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
    public class UnTypedMessageRespondExtension
    {
        private readonly IConsumeContext _consumeContext;

        public UnTypedMessageRespondExtension(IConsumeContext consumeContext)
        {
            _consumeContext = consumeContext;
        }

        public void Respond(object message)
        {
            Respond(message, new Dictionary<string, string>());
        }

        public void Respond(object message, Dictionary<string, string> headers)
        {
            this.FastInvoke(new[] {message.GetType(), headers.GetType()}, "Respond", message, headers);
        }

        protected void Respond<T>(T message, Dictionary<string, string> headers) where T : class
        {
            _consumeContext.Respond(message, ctx =>
                                                 {
                                                     foreach (var header in headers)
                                                         ctx.SetHeader(header.Key, header.Value);
                                                 });
        }
    }
}