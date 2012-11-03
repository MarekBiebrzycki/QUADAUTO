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
using MassTransit;
using QuadAuto.Web.Controllers.Committees.Commands;

namespace QuadAutomotiveGroupSite.ControllerServices
{
    public class CommitteesService
    {
        private IServiceBus _bus;

        public CommitteesService(IServiceBus bus)
        {
            _bus = bus;
        }

        public void AddNewCommittee(string title, string menuTitle, string content)
        {
            var m = new AddNewCommittee(Guid.NewGuid(), content, menuTitle, Guid.NewGuid(), title);
            _bus.Publish(m);
            //return new CarriersQueryModel.Carrier(m.Id, name);
        }
    }
}