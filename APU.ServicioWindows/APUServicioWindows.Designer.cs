namespace APU.ServicioWindows
{
    partial class APUServicioWindows
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.timEnvioSunat = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timEnvioSunat)).BeginInit();
            // 
            // timEnvioSunat
            // 
            this.timEnvioSunat.Enabled = true;
            this.timEnvioSunat.Elapsed += new System.Timers.ElapsedEventHandler(this.timEnvioSunat_Elapsed);
            // 
            // APUServicioWindows
            // 
            this.ServiceName = "APU.ServicioWindows";
            ((System.ComponentModel.ISupportInitialize)(this.timEnvioSunat)).EndInit();

        }

        #endregion

        private System.Timers.Timer timEnvioSunat;
    }
}
