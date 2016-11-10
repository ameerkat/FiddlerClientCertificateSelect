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

        public void OnBeforeUnload()
        {
            Fiddler.FiddlerApplication.ClientCertificateProvider = previousClientCertificateProvider;
        }

        private IList<X509Certificate2> GetCientCertificates()
        {
            X509Store certificateStore = new X509Store(); // current user personal store
            certificateStore.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            List<X509Certificate2> clientCertifiates = new List<X509Certificate2>();
            foreach (var certificate in certificateStore.Certificates)
            {
                clientCertifiates.Add(certificate);
            }

            return clientCertifiates;
        }

        public void OnLoad()
        {
            previousClientCertificateProvider = Fiddler.FiddlerApplication.ClientCertificateProvider;
            Fiddler.FiddlerApplication.ClientCertificateProvider = this.LocalCertificateSelectionCallback;
            certificateSelectorForm = new CertificateSelector(GetCientCertificates());
        }

        public X509Certificate LocalCertificateSelectionCallback(object sender, 
            string targetHost, 
            X509CertificateCollection localCertificates, 
            X509Certificate remoteCertificate, 
            string[] acceptableIssuers)
        {
            DialogResult dialogResult = certificateSelectorForm.ShowDialog();
            return null;
        }
    }
}
