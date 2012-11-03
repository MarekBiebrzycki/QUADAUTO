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

using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using CommonDomain.Persistence;

namespace QuadAuto.Server
{
    public class RequireRepositoryProperties : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            model.Properties
                .Where(
                    p =>
                    p.Dependency.TargetType == typeof (IRepository) ||
                    p.Dependency.TargetType == typeof (ISagaRepository))
                .All(p => p.Dependency.IsOptional = false);
        }
    }
}