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

using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using CommonDomain;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Persistence.SqlPersistence;
using QuadAuto.Server.Domain;
using QuadAuto.Server.Domain.MassTransit;

namespace QuadAuto.Server.Services
{
    public class EventStoreFacility : AbstractFacility
    {
        public virtual string ConnectionString { get; set; }
        public virtual ISqlDialect SqlDialect { get; set; }

        private IStoreEvents CreateStore()
        {
            return Wireup.Init()
                .UsingSqlPersistence(ConnectionString)
                .WithDialect(SqlDialect)
                .InitializeStorageEngine()
                .EnlistInAmbientTransaction()
                .UsingServiceStackJsonSerialization()
                .UsingAsynchronousDispatchScheduler(
                    Kernel.Resolve<IDispatchCommits>())
                .Build();
        }

        protected override void Init()
        {
            Kernel.Register(
                Component.For<IDetectConflicts>().ImplementedBy<ConflictDetector>(),
                Component.For<IConstructAggregates>().ImplementedBy<AggregateFactory>(),
                Component.For<IDispatchCommits>().ImplementedBy<MassTransitPublisher>(),
                Component.For<IRepository>().ImplementedBy<EventStoreRepository>(),
                Component.For<ISagaRepository>().ImplementedBy<SagaEventStoreRepository>());

            Kernel.Register(Component.For<IStoreEvents>().UsingFactoryMethod(CreateStore));
        }
    }
}