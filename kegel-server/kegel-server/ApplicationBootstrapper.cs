using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Diagnostics;
using Nancy;
using Nancy.Hosting.Self;

namespace kegel_server
{
	public class ApplicationBootstrapper : Nancy.DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions (Nancy.Conventions.NancyConventions nancyConventions)
		{
			nancyConventions.StaticContentsConventions.Add (Nancy.Conventions.StaticContentConventionBuilder.AddDirectory ("Static", @"Static"));
            
			base.ConfigureConventions (nancyConventions);
		}

		protected override DiagnosticsConfiguration DiagnosticsConfiguration {
			get { return new DiagnosticsConfiguration { Password = @"test"}; }
		}

		protected override void ApplicationStartup (Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			StaticConfiguration.DisableErrorTraces = false;

			base.ApplicationStartup (container, pipelines);
		}
	}
}
