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

using CommonDomain.Persistence;
using MassTransit;

namespace QuadAuto.Server.Domain
{
    public abstract class AbstractConsumeBase
    {
        public IRepository Repository { get; set; }
    }

    public abstract class AbstractConsumeAll<T> : AbstractConsumeBase, Consumes<T>.All where T : class
    {
        public abstract void Consume(T message);
    }

    public abstract class AbstractConsumeContext<T> : AbstractConsumeBase, Consumes<T>.Context where T : class
    {
        public abstract void Consume(IConsumeContext<T> message);
    }

    public abstract class AbstractConsumeFor<T, TCorrelationId> : AbstractConsumeBase, Consumes<T>.For<TCorrelationId>
        where T : class
    {
        public abstract void Consume(T message);
        public abstract TCorrelationId CorrelationId { get; }
    }

    public abstract class AbstractConsumeSelected<T> : AbstractConsumeBase, Consumes<T>.Selected where T : class
    {
        public abstract void Consume(T message);
        public abstract bool Accept(T message);
    }
}