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
using System.Linq;
using MassTransit;
using MongoDB.Driver.Builders;
using QuadAuto.Server.Denormalizers;
using QuadAuto.Web.Controllers.Committees.Events;
using QuadAuto.Web.Controllers.Committees.QueryModels;
using QuadAuto.Web.Controllers.Meetings.QueryModels;
using QuadAuto.Web.Controllers.Members.QueryModels;
using QuadAuto.Web.Controllers.Pages.QueryModels;

namespace QuadAuto.Server.Web.Controllers.Committees.Denormalizers
{
    public class CommitteesDenormalizer : AbstractDenormalizerBase, Consumes<NewCommitteeAdded>.All
    {
        public void Consume(NewCommitteeAdded message)
        {
            var query = Query.EQ("_t", "CommitteesQueryModel");
            var col = Db.GetCollection<CommitteesQueryModel>("CommitteesQueryModel");
            CommitteesQueryModel cm = col.FindAs<CommitteesQueryModel>(query).FirstOrDefault();
            ;
            if (cm == null)
            {
                cm = new CommitteesQueryModel(Guid.NewGuid());
                cm.Committees = new List<CommitteeModel>();
                cm.Committees.Add(new CommitteeModel(message.Id, message.MenuTitle, message.Title, message.Content,
                                                     new List<PageModel>(), message.Title, new List<CommitteeMeeting>(),
                                                     new List<BaseGroupMemberModel>()));
                col.Insert(cm);
            }
            else
            {
                cm.Committees.Add(new CommitteeModel(message.Id, message.MenuTitle, message.Title, message.Content,
                                                     new List<PageModel>(), message.Title, new List<CommitteeMeeting>(),
                                                     new List<BaseGroupMemberModel>()));
                col.Save(cm);
            }
        }
    }
}