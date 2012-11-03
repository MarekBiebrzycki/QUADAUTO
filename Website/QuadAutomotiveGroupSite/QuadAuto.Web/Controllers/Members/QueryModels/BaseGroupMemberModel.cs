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

namespace QuadAuto.Web.Controllers.Members.QueryModels
{
    public class BaseGroupMemberModel : BaseModel
    {
        public BaseGroupMemberModel(Guid id, string firstName, string lastName, string emailAddress, bool active,
                                    string password, DateTime birthDate) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Active = active;
            Password = password;
            BirthDate = birthDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}