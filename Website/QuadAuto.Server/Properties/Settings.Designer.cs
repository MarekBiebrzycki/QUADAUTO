﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuadAuto.Server.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("rabbitmq://192.168.79.1/QuadAuto")]
        public string MassTransitReceiveFrom {
            get {
                return ((string)(this["MassTransitReceiveFrom"]));
            }
            set {
                this["MassTransitReceiveFrom"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("rabbitmq://192.168.79.1/QuadAuto")]
        public string MassTransitReceiveFromDebug {
            get {
                return ((string)(this["MassTransitReceiveFromDebug"]));
            }
            set {
                this["MassTransitReceiveFromDebug"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mongodb://192.168.79.1/?safe=true")]
        public string MongoDbServer {
            get {
                return ((string)(this["MongoDbServer"]));
            }
            set {
                this["MongoDbServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("QuadAutomotive")]
        public string MongoDbDatabase {
            get {
                return ((string)(this["MongoDbDatabase"]));
            }
            set {
                this["MongoDbDatabase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mongodb://192.168.79.1/?safe=true")]
        public string MongoDbServerDebug {
            get {
                return ((string)(this["MongoDbServerDebug"]));
            }
            set {
                this["MongoDbServerDebug"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("QuadAutomotive")]
        public string MongoDbDatabaseDebug {
            get {
                return ((string)(this["MongoDbDatabaseDebug"]));
            }
            set {
                this["MongoDbDatabaseDebug"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=192.168.79.1;Port=5432;User Id=tmassey;Password=Poilkjmnb!1;Database=Event" +
            "Store;Protocol=3;SSL=false;SslMode=Disable;Timeout=1024;")]
        public string EventStoreConnectionString {
            get {
                return ((string)(this["EventStoreConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=192.168.79.1;Port=5432;User Id=tmassey;Password=Poilkjmnb!1;Database=Event" +
            "Store;Protocol=3;SSL=false;SslMode=Disable;Timeout=1024;")]
        public string EventStoreConnectionStringDebug {
            get {
                return ((string)(this["EventStoreConnectionStringDebug"]));
            }
        }
    }
}