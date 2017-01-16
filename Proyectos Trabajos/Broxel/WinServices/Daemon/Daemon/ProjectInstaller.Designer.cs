namespace PubliPayments
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcesoInstaladorServicio = new System.ServiceProcess.ServiceProcessInstaller();
            this.InstaladorServicio = new System.ServiceProcess.ServiceInstaller();
            // 
            // ProcesoInstaladorServicio
            // 
            this.ProcesoInstaladorServicio.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ProcesoInstaladorServicio.Password = null;
            this.ProcesoInstaladorServicio.Username = null;
            // 
            // InstaladorServicio
            // 
            this.InstaladorServicio.Description = "Disk And Execution Monitor PubliPayments";
            this.InstaladorServicio.DisplayName = "Daemon PubliPayments";
            this.InstaladorServicio.ServiceName = "PubliPaymentsService";
            this.InstaladorServicio.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ProcesoInstaladorServicio,
            this.InstaladorServicio});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ProcesoInstaladorServicio;
        private System.ServiceProcess.ServiceInstaller InstaladorServicio;
    }
}