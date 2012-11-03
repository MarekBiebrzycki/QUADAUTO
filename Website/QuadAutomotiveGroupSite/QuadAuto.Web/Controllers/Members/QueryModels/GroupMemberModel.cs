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
using QuadAuto.Web.Controllers.Committees.QueryModels;

namespace QuadAuto.Web.Controllers.Members.QueryModels
{
    public class GroupMemberModel : BaseGroupMemberModel
    {
        public GroupMemberModel(Guid id, string firstName, string lastName, string emailAddress, bool active,
                                string password, DateTime birthDate, IEnumerable<CommitteeModel> committees,
                                MembershipTypeModel membershipType)
            : base(id, firstName, lastName, emailAddress, active, password, birthDate)
        {
            Committees = committees;
            MembershipType = membershipType;
        }

        public IEnumerable<CommitteeModel> Committees { get; set; }
        public MembershipTypeModel MembershipType { get; set; }
    }
}