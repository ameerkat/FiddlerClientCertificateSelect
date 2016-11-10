# About Fiddler Client Certificate Select
Fiddler client certificate select is a fiddler extension designed to allow users to select certificates directly from their certificate store for use with fiddler. If you use don't switch client certificates frequently you may be satisfied with the traditional method of attaching client certificates outlined [here](https://www.fiddlerbook.com/fiddler/help/httpsclientcerts.asp).

# Building
You will have to modify the project references to refer to your local copy of fiddler. This extension was created against fiddler2 in Visual Studio 2013. More information about extending fiddler with .net can be found [here](http://docs.telerik.com/fiddler/extend-fiddler/extendwithdotnet). You will need to modify the debug action if your fiddler is not in C:/Program Files (x86)/Fiddler2/, if you'd like to run it easily in debug mode.

# Usage
Client certificate select adds 3 menu options to the "Rules" menu
## Client Certificate Select
This is whether or not to use the client certificate select functionality at all. When disable the menu item will appear without a check mark next to it. To enable click on the menu item "Client Certificate Select", and a check mark will appear. To disable click again. Enablement status will be maintained when reopening fiddler. When this option is disabled, the fiddler extension will be disabled and the client certificate selection mechanism will default to the fiddler default behavior.
## Set Global Client Certificate
Please note this only works when "Client Certificate Select" is enabled. When there is no client certificate selected this menu item will display "Set Global Client Certificate", when set it will display the thumbprint of the selected certificate. Clicking on the menu item regardless of the state will bring up the client certificate selection grid. Setting a global client certificate will cause Fiddler Client Certificate Select to not prompt you for a certificate when requested, rather it will always pass the certificate you have selected as the client certificate.
## Clear Global Client Certificate
This is only enabled when a client certificate is set. This clears the default client certificate.

# References
* [System.Net.Security.LocalCertificateSelectionCallback](https://msdn.microsoft.com/en-us/library/system.net.security.localcertificateselectioncallback(v=vs.110).aspx)
