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
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace QuadAutomotiveGroupSite.DataProvider
{
    public static class DataProvider
    {
        public static object GetAllQueryModels(Type type)
        {
            var server = MongoServer.Create("mongodb://192.168.79.1/?safe=true");
            var database = server.GetDatabase("QuadAutomotive");
            var collection = database.GetCollection("WebSite");
            var query = Query.EQ("_t", type.Name);
            var r = collection.FindAs(type, query);
            return r;
        }

        public static object GetQueryModel(Type type)
        {
            var server = MongoServer.Create("mongodb://192.168.79.1/?safe=true");
            var database = server.GetDatabase("QuadAutomotive");
            var collection = database.GetCollection(type.Name);
            var query = Query.EQ("_t", type.Name);
            var r = collection.FindOneAs(type, query);
            return r;
        }
    }
}