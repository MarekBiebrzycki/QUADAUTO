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
using QuadAuto.Web.Controllers.Members.QueryModels;

namespace QuadAuto.Web.Controllers.Meetings.QueryModels
{
    public class EventItem : BaseModel
    {
        public EventItem(Guid id, string description, DateTime endTime, DateTime startTime, string subject,
                         EventLocation @where) : base(id)
        {
            Description = description;
            EndTime = endTime;
            StartTime = startTime;
            Subject = subject;
            Where = @where;
        }

        public EventLocation Where { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Description { get; set; }
        public IEnumerable<BaseGroupMemberModel> Attendance { get; set; }
    }
}