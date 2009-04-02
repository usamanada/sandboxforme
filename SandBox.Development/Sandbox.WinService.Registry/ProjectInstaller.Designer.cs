namespace Sandbox.WinService.Registry
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
            this.SandboxWinServiceRegistryProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SandboxWinServiceRegistryInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SandboxWinServiceRegistryProcessInstaller
            // 
            this.SandboxWinServiceRegistryProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SandboxWinServiceRegistryProcessInstaller.Password = null;
            this.SandboxWinServiceRegistryProcessInstaller.Username = null;
            // 
            // SandboxWinServiceRegistryInstaller
            // 
            this.SandboxWinServiceRegistryInstaller.Description = "Used to set registry setting based on config file.";
            this.SandboxWinServiceRegistryInstaller.DisplayName = "Sandbox WinService Registry";
            this.SandboxWinServiceRegistryInstaller.ServiceName = "SandboxWinServiceRegistry";
            this.SandboxWinServiceRegistryInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SandboxWinServiceRegistryProcessInstaller,
            this.SandboxWinServiceRegistryInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SandboxWinServiceRegistryProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SandboxWinServiceRegistryInstaller;
    }
}