// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.Extensions.DependencyInjection.Tests.Fakes
{
    public class DependencyOnCircularDependency
    {
        public DependencyOnCircularDependency(DirectCircularDependencyA a)
        {

        }
    }
}
