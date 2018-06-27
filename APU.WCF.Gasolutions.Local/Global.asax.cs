using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace APU.WCF.Gasolutions.Local
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            IConfigurationSource source = ConfigurationSourceFactory.Create();

            var logwriterFactory = new LogWriterFactory(source);
            var logWriter = logwriterFactory.Create();
            Logger.SetLogWriter(logWriter);

            var exceptionPolicyFactory = new ExceptionPolicyFactory(source);
            var exceptionManager = exceptionPolicyFactory.CreateManager();
            ExceptionPolicy.SetExceptionManager(exceptionManager);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}