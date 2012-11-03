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
using QuadAuto.Web.Controllers.Meetings.QueryModels;
using QuadAuto.Web.Controllers.Members.QueryModels;
using QuadAuto.Web.Controllers.Pages.QueryModels;

namespace QuadAuto.Web.Controllers.Committees.QueryModels
{
    public class CommitteeModel : PageModel
    {
        public CommitteeModel(Guid id, string menuTitle, string title, string content, IEnumerable<PageModel> childPages,
                              string name, IEnumerable<CommitteeMeeting> committeeMeetings,
                              IEnumerable<BaseGroupMemberModel> members)
            : base(id, menuTitle, title, content, childPages)
        {
            Name = name;
            CommitteeMeetings = committeeMeetings;
            Members = members;
        }

        public string Name { get; set; }
        public BaseGroupMemberModel President { get; set; }
        public IEnumerable<CommitteeMeeting> CommitteeMeetings { get; set; }
        public IEnumerable<BaseGroupMemberModel> Members { get; set; }
    }
}