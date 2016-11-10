using System;
using System.Windows.Forms;
using Fiddler;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

[assembly: Fiddler.RequiredVersion("2.2.8.6")]
namespace FiddlerClientCertificateSelect
{
    public class FiddlerClientCertificateSelect : IFiddlerExtension
    {
        private LocalCertificateSelectionCallback previousClientCertificateProvider;
        private CertificateSelector certificateSelectorForm;
        private MenuItem rulesParentMenu;
        private MenuItem enableMenuItem;
        private MenuItem defaultClientCertMenuItem;
        private MenuItem clearDefaultClientCertMenuItem;
        private X509Certificate defaultClientCertificate;
        private bool enabled;

        private void Disable() 
        {
            enabled = false;
            Fiddler.FiddlerApplication.ClientCertificateProvider = previousClientCertificateProvider;
            if (enableMenuItem != null)
            {
                enableMenuItem.Checked = false;
            }
        }

        private void Enable() 
        {
            enabled = true;
            if (previousClientCertificateProvider == null)
                previousClientCertificateProvider = Fiddler.FiddlerApplication.ClientCertificateProvider;
            Fiddler.FiddlerApplication.ClientCertificateProvider = this.LocalCertificateSelectionCallback;

            if (enableMenuItem != null)
            {
                enableMenuItem.Checked = true;
            }
        }

        public void OnBeforeUnload()
        {
            Properties.Settings.Default.Enabled = enabled;
            Properties.Settings.Default.Save();

            Disable();
        }

        public void OnLoad()
        {
            rulesParentMenu = null;
            foreach (MenuItem menuItem in Fiddler.FiddlerApplication.UI.Menu.MenuItems)
            {
                if (menuItem.Text == "&Rules")
                    rulesParentMenu = menuItem;
            }

            if (rulesParentMenu != null)
            {
                enableMenuItem = new MenuItem(FiddlerClientCertificateSelectResources.EnableClientCertificateSelection, Enable_Click);
                rulesParentMenu.MenuItems.Add(enableMenuItem);

                defaultClientCertMenuItem = new MenuItem(FiddlerClientCertificateSelectResources.SetDefault, SetDefault_Click);
                rulesParentMenu.MenuItems.Add(defaultClientCertMenuItem);

                clearDefaultClientCertMenuItem = new MenuItem(FiddlerClientCertificateSelectResources.ClearDefault, ClearDefault_Click);
                clearDefaultClientCertMenuItem.Enabled = false;
                rulesParentMenu.MenuItems.Add(clearDefaultClientCertMenuItem);
            }

            if (Properties.Settings.Default.Enabled)
            {
                Enable();
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.DefaultClientCertificate))
            {
                SetDefault(Properties.Settings.Default.DefaultClientCertificate);
            }
        }

        private void SetDefault(string thumbprint = null)
        {
            if (thumbprint == null)
            {
                this.defaultClientCertificate = GetCertificate(null, FiddlerClientCertificateSelectResources.GlobalDefault);
            }
            else
            {
                X509Certificate2 certificate = null;
                X509Store store = new X509Store();
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
                foreach(X509Certificate2 cert in store.Certificates) 
                {
                    if (string.Equals(cert.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase)) 
                    {
                        certificate = cert;
                    }
                }

                this.defaultClientCertificate = certificate;
            }
            
            if (this.defaultClientCertificate != null)
            {
                this.defaultClientCertMenuItem.Text = ((X509Certificate2)defaultClientCertificate).Thumbprint;
                this.clearDefaultClientCertMenuItem.Enabled = true;
                this.defaultClientCertMenuItem.Checked = true;
            }
        }

        private void SetDefault_Click(object sender, EventArgs e)
        {
            SetDefault();
            if (this.defaultClientCertificate != null) 
            {
                Properties.Settings.Default.DefaultClientCertificate = ((X509Certificate2)this.defaultClientCertificate).Thumbprint;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.DefaultClientCertificate = null;
                Properties.Settings.Default.Save();
            }
        }

        private void ClearDefault()
        {
            this.defaultClientCertificate = null;
            this.defaultClientCertMenuItem.Text = FiddlerClientCertificateSelectResources.SetDefault;
            this.clearDefaultClientCertMenuItem.Enabled = false;
            this.defaultClientCertMenuItem.Checked = false;
        }

        private void ClearDefault_Click(object sender, EventArgs e)
        {
            ClearDefault();
            Properties.Settings.Default.DefaultClientCertificate = null;
            Properties.Settings.Default.Save();
        }

        private void Enable_Click(object sender, EventArgs e)
        {
            if (this.enabled)
            {
                this.Disable();
            }
            else
            {
                this.Enable();
            }
        }

        public X509Certificate GetCertificate(X509CertificateCollection localCertificates, string targetHost)
        {
            X509CertificateCollection collection = localCertificates;
            if (collection == null || collection.Count == 0)
            {
                X509Store localPersonalStore = new X509Store();
                localPersonalStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                collection = localPersonalStore.Certificates;
            }

            certificateSelectorForm = new CertificateSelector(
                collection,
                targetHost);
            var dialogResult = certificateSelectorForm.ShowDialog();
            return certificateSelectorForm.SelectedCertificate;
        }

        public X509Certificate LocalCertificateSelectionCallback(object sender, 
            string targetHost, 
            X509CertificateCollection localCertificates, 
            X509Certificate remoteCertificate, 
            string[] acceptableIssuers)
        {
            // If a remote certificate has not been presented then the client certificate is not requested yet
            if (remoteCertificate == null)
            {
                return null;
            }

            if (defaultClientCertificate != null)
            {
                return defaultClientCertificate;
            }

            return GetCertificate(localCertificates, targetHost);
        }
    }
}
