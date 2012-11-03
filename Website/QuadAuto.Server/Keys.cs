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

using System.Diagnostics;
using System.Reflection;
using QuadAuto.Server.Properties;

namespace QuadAuto.Server
{
    public static class Keys
    {
        private static readonly string PropertyPrefix;

        static Keys()
        {
            PropertyPrefix = Assembly.GetExecutingAssembly().GetName().Name + ".Properties.Settings.";
        }

        public static string MongoDbDatabase
        {
            get { return Get<string>("MongoDbDatabase"); }
        }

        public static string MongoDbServer
        {
            get { return Get<string>("MongoDbServer"); }
        }

        public static string MassTransitReceiveFrom
        {
            get { return Get<string>("MassTransitReceiveFrom"); }
        }

        public static string EventStoreKeyName
        {
            get { return GetKey("EventStoreConnectionString"); }
        }

        private static T Get<T>(string key)
        {
            var prop = Settings.Default.Properties[key + (Debugger.IsAttached ? "Debug" : "")];
            return prop == null ? default(T) : (T) prop.DefaultValue;
        }

        private static string GetKey(string key)
        {
            return PropertyPrefix + key + (Debugger.IsAttached ? "Debug" : "");
        }
    }
}