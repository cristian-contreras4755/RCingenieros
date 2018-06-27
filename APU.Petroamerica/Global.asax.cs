﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace APU.Petroamerica
{
    public class Global : System.Web.HttpApplication
    {
        public static IHttpModule Module = new QueryString();

        public override void Init()
        {
            //base.Init();
            Module.Init(this);
        }
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
    }
}