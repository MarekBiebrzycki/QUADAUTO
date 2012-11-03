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

namespace QuadAutomotiveGroupSite.Models
{
    public class PageModel : BaseModel
    {
        public string MenuTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<PageModel> ChildPages { get; set; }
    }

    public class BaseGroupMemberModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class GroupMemberModel : BaseGroupMemberModel
    {
        public IEnumerable<CommitteeModel> Committees { get; set; }
        public MembershipTypeModel MembershipType { get; set; }
    }

    public class MembershipTypeModel : BaseModel
    {
        public string Name { get; set; }

        public class SubscriptionModel : BaseModel
        {
        }
    }

    public class BaseMembershipModel : BaseModel
    {
        public MembershipInterval DuesInterval { get; set; }
        public int DuesAmmount { get; set; }


        public enum MembershipInterval
        {
            Month,
            Year
        }
    }

    public class CommitteeModel : PageModel
    {
        public string Name { get; set; }
        public IEnumerable<CommitteeMeeting> CommitteeMeetings { get; set; }
        public IEnumerable<BaseGroupMemberModel> Members { get; set; }
    }

    public class CommitteeMeeting : EventItem
    {
        public IEnumerable<AgendaItem> AgendaItems { get; set; }
        public IEnumerable<BaseGroupMemberModel> RequiredAttendance { get; set; }
    }

    public class AgendaItem : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class EventItem : BaseModel
    {
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Description { get; set; }
        public IEnumerable<BaseGroupMemberModel> Attendance { get; set; }
    }
}