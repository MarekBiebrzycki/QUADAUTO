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
using System.Linq.Expressions;

namespace QuadAuto.Server.Domain.MassTransit
{
    public class DelegateAdjuster
    {
        public static Action<TBase> CastArgument<TBase, TDerived>(Expression<Action<TDerived>> source)
            where TDerived : TBase
        {
            if (typeof (TDerived) == typeof (TBase))
                return (Action<TBase>) ((Delegate) source.Compile());

            var sourceParameter = Expression.Parameter(typeof (TBase), "source");
            var result =
                Expression.Lambda<Action<TBase>>(
                    Expression.Invoke(source, Expression.Convert(sourceParameter, typeof (TDerived))), sourceParameter);
            return result.Compile();
        }
    }
}