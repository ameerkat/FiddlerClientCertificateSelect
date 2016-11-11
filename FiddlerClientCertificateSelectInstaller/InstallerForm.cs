using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiddlerClientCertificateSelectInstaller
{
    public partial class InstallerForm : Form
    {
        private const string dllName = "FiddlerClientCertificateSelect.dll";
        private const string globalPath = @"%PROGRAMFILES(X86)%\Fiddler2\Scripts";
        private bool IsInstalledGlobally() 
        {
            var fullPath = Path.Combine(globalPath, dllName);
            var expandedPath = Environment.ExpandEnvironmentVariables(fullPath);
            return File.Exists(expandedPath);
        }

        private const string localPath = @"%USERPROFILE%\My Documents\Fiddler2\Scripts";
        private bool IsInstalledLocally() 
        {
            var fullPath = Path.Combine(localPath, dllName);
            var expandedPath = Environment.ExpandEnvironmentVariables(fullPath);
            return File.Exists(expandedPath);
        }

        private void UpdateButtonState()
        {
            var isInstalledGlobally = this.IsInstalledGlobally();
            var isInstalledLocally = this.IsInstalledLocally();

            if (!isInstalledGlobally && !isInstalledLocally)
            {
                InstallCurrentUserButton.Enabled = true;
                InstallForAllUsers.Enabled = true;
                UninstallButton.Enabled = false;
            }
            else if (!isInstalledGlobally && isInstalledLocally)
            {
                UninstallButton.Enabled = true;
                InstallCurrentUserButton.Enabled = false;
                InstallForAllUsers.Enabled = true;
            }
            else if (isInstalledGlobally && isInstalledLocally)
            {
                UninstallButton.Enabled = true;
                InstallCurrentUserButton.Enabled = false;
                InstallForAllUsers.Enabled = false;
            }
            else if (isInstalledGlobally && !isInstalledLocally)
            {
                UninstallButton.Enabled = true;
                InstallCurrentUserButton.Enabled = true;
                InstallForAllUsers.Enabled = false;
            }

            if (!IsAdmin)
            {
                InstallForAllUsers.Enabled = false;
                if (!isInstalledLocally)
                {
                    UninstallButton.Enabled = false;
                }
            }
        }

        public InstallerForm()
        {
            InitializeComponent();
            UpdateButtonState();
        }

        private void CopyResourceToFile(Assembly assembly, string resourceName, string filePath)
        {
            using (FileStream outStream = File.Open(filePath, FileMode.Create))
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    bool exitCondition = false;
                    while(!exitCondition) {
                        int result = stream.ReadByte();
                        if (result == -1) 
                        {
                            exitCondition = true;
                            break;
                        }

                        Byte readByte = Convert.ToByte(result);
                        outStream.WriteByte(readByte);
                    }
                }
            }
            
        }

        private void InstallLocal()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var primaryDll = "FiddlerClientCertificateSelectInstaller.Embedded.FiddlerClientCertificateSelect.dll";
            var fullTargetPath = Environment.ExpandEnvironmentVariables(Path.Combine(localPath, dllName));
            CopyResourceToFile(assembly, primaryDll, fullTargetPath);
        }

        private void InstallGlobal()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var primaryDll = "FiddlerClientCertificateSelectInstaller.Embedded.FiddlerClientCertificateSelect.dll";
            var fullTargetPath = Environment.ExpandEnvironmentVariables(Path.Combine(globalPath, dllName));
            CopyResourceToFile(assembly, primaryDll, fullTargetPath);
        }

        private void Uninstall()
        {
            if (IsAdmin)
            {
                var globalPathFull = Environment.ExpandEnvironmentVariables(Path.Combine(globalPath, dllName));
                if (File.Exists(globalPathFull))
                {
                    File.Delete(globalPathFull);
                }
            }

            var localPathFull = Environment.ExpandEnvironmentVariables(Path.Combine(localPath, dllName));
            if (File.Exists(localPathFull))
            {
                File.Delete(localPathFull);
            }
        }

        private bool IsAdmin { 
            get 
            { 
                return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                       .IsInRole(WindowsBuiltInRole.Administrator) ? true : false; 
            } 
        }

        private void InstallCurrentUserButton_Click(object sender, EventArgs e)
        {
            InstallLocal();
            UpdateButtonState();
        }

        private void InstallForAllUsers_Click(object sender, EventArgs e)
        {
            InstallGlobal();
            UpdateButtonState();
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            Uninstall();
            UpdateButtonState();
        }
    }
}
