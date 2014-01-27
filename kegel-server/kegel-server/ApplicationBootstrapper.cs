using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kegel_server
{
    public class ApplicationBootstrapper : Nancy.DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("Static", @"Static"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
