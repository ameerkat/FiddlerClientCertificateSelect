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
        private bool isPreviousProviderSet = false;
        private LocalCertificateSelectionCallback previousClientCertificateProvider = null;

        private IClientCertificateSelector clientCertificateSelector;
        private X509Certificate defaultClientCertificate = null;
        private bool isExtensionEnabled;

        /*
         * Menu Items
         */
        private const string parentMenuIdentifier = "&Rules";

        private MenuItem rulesParentMenu;
        private MenuItem enableMenuItem;
        private MenuItem useDefaultGlobalClientCertificate;
        private MenuItem defaultClientCertMenuItem;
        private MenuItem clearDefaultClientCertMenuItem;
        private MenuItem[] secondaryMenuItems;

        #region IFiddlerExtension

        public void OnBeforeUnload()
        {
            Properties.Settings.Default.Enabled = isExtensionEnabled;
            Properties.Settings.Default.UseWindowsUI = this.clientCertificateSelector is WindowsDefaultCertificateSelector;
            Properties.Settings.Default.Save();

            Disable();
        }

        public void OnLoad()
        {
            secondaryMenuItems = new MenuItem[] { 
                useDefaultGlobalClientCertificate, 
                defaultClientCertMenuItem, 
                clearDefaultClientCertMenuItem 
            };

            clientCertificateSelector = new CertificateGridViewSelector();

            rulesParentMenu = null;
            foreach (MenuItem menuItem in Fiddler.FiddlerApplication.UI.Menu.MenuItems)
            {
                if (string.Equals(menuItem.Text, parentMenuIdentifier))
                    rulesParentMenu = menuItem;
            }

            if (rulesParentMenu != null)
            {
                var spacer = new MenuItem("-");
                rulesParentMenu.MenuItems.Add(spacer);

                enableMenuItem = new MenuItem(FiddlerClientCertificateSelectResources.EnableClientCertificateSelection, Enable_Click);
                rulesParentMenu.MenuItems.Add(enableMenuItem);

                useDefaultGlobalClientCertificate = new MenuItem(FiddlerClientCertificateSelectResources.UseWindowsDefaultUI, Use_Windows_UI_Click);
                rulesParentMenu.MenuItems.Add(useDefaultGlobalClientCertificate);

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
            else
            {
                Disable();
            }

            if (Properties.Settings.Default.UseWindowsUI)
            {
                this.Use_Windows_UI_Click(null, null);
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.DefaultClientCertificate))
            {
                SetGlobalDefaultClientCertificate(Properties.Settings.Default.DefaultClientCertificate);
            }
        }

        #endregion IFiddlerExtension

        /// <summary>
        /// Disables the extension, falling back to the previous callback
        /// </summary>
        private void Disable() 
        {
            isExtensionEnabled = false;
            Fiddler.FiddlerApplication.ClientCertificateProvider = previousClientCertificateProvider;
            if (enableMenuItem != null)
            {
                enableMenuItem.Checked = false;
            }

            foreach (var menuItem in secondaryMenuItems)
            {
                menuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the extension, directing the callback to the appropriate cert selection UI
        /// </summary>
        private void Enable() 
        {
            isExtensionEnabled = true;
            if (!isPreviousProviderSet)
            {
                previousClientCertificateProvider = Fiddler.FiddlerApplication.ClientCertificateProvider;
                isPreviousProviderSet = true;
            }

            Fiddler.FiddlerApplication.ClientCertificateProvider = this.LocalCertificateSelectionCallback;

            if (enableMenuItem != null)
            {
                enableMenuItem.Checked = true;
            }

            foreach (var menuItem in secondaryMenuItems)
            {
                menuItem.Enabled = true;
            }

            if (defaultClientCertificate == null)
            {
                clearDefaultClientCertMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Sets the default certificate
        /// </summary>
        /// <param name="thumbprint"></param>
        private void SetGlobalDefaultClientCertificate(string thumbprint = null)
        {
            if (thumbprint == null)
            {
                this.defaultClientCertificate = clientCertificateSelector.GetCertificate(null, FiddlerClientCertificateSelectResources.GlobalDefault);
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
        
        /// <summary>
        /// Clears the global default client certificate
        /// </summary>
        private void ClearGlobalDefaultClientCertificate()
        {
            this.defaultClientCertificate = null;
            this.defaultClientCertMenuItem.Text = FiddlerClientCertificateSelectResources.SetDefault;
            this.clearDefaultClientCertMenuItem.Enabled = false;
            this.defaultClientCertMenuItem.Checked = false;
        }

        #region Menu Item Event Handlers

        private void SetDefault_Click(object sender, EventArgs e)
        {
            SetGlobalDefaultClientCertificate();
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

        private void ClearDefault_Click(object sender, EventArgs e)
        {
            ClearGlobalDefaultClientCertificate();
            Properties.Settings.Default.DefaultClientCertificate = null;
            Properties.Settings.Default.Save();
        }

        private void Enable_Click(object sender, EventArgs e)
        {
            if (this.isExtensionEnabled)
            {
                this.Disable();
            }
            else
            {
                this.Enable();
            }
        }

        private void Use_Windows_UI_Click(object sender, EventArgs e)
        {
            if (this.clientCertificateSelector is WindowsDefaultCertificateSelector)
            {
                this.clientCertificateSelector = new CertificateGridViewSelector();
                useDefaultGlobalClientCertificate.Checked = false;
            }
            else
            {
                this.clientCertificateSelector = new WindowsDefaultCertificateSelector();
                useDefaultGlobalClientCertificate.Checked = true;
            }
        }

        #endregion Menu Item Event Handlers

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

            return clientCertificateSelector.GetCertificate(localCertificates, targetHost);
        }
    }
}
