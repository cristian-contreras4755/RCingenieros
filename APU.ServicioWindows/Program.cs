using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace APU.ServicioWindows
{
    //static class Program
    //{
    //    /// <summary>
    //    /// Punto de entrada principal para la aplicación.
    //    /// </summary>
    //    static void Main()
    //    {
    //        ServiceBase[] ServicesToRun;
    //        ServicesToRun = new ServiceBase[]
    //        {
    //            new APUServicioWindows()
    //        };
    //        ServiceBase.Run(ServicesToRun);
    //    }
    //}
    public class EMSServicio : ServiceBase
    {
        private Container components = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new APUServicioWindows()
            };
            ServiceBase.Run(ServicesToRun);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}