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
using CommonDomain;
using CommonDomain.Persistence;

namespace QuadAuto.Server.Domain
{
    public class AggregateFactory : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            //return Activator.CreateInstance(type, id) as IAggregate; 
            // Note: here we could provide some nice AOP support!
            return Activator.CreateInstance(type, new object[1] {id}) as IAggregate;
        }
    }
}