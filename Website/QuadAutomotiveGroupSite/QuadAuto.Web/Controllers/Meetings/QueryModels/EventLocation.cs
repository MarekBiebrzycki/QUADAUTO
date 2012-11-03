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

namespace QuadAuto.Web.Controllers.Meetings.QueryModels
{
    public class EventLocation : BaseModel
    {
        public EventLocation(Guid id, string address, string city, bool isVirtual, string name, string state,
                             string virtualAddress, string zipCode) : base(id)
        {
            Address = address;
            City = city;
            IsVirtual = isVirtual;
            Name = name;
            State = state;
            VirtualAddress = virtualAddress;
            ZipCode = zipCode;
        }

        public string Name { get; set; }
        public bool IsVirtual { get; set; }
        public string VirtualAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}