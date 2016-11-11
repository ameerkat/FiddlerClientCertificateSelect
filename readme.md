# About Fiddler Client Certificate Select
Client certificates are a commonly used authentication mechanism for server to server calls. Unfortunately setting up client certificates in fiddler require you to export the certificate from the local certificate store. Determining which client certificate is set requires locating the file and inspecting the certificate details. Switching certificates is non trivial and can't be done from server to server easily. Fiddler client certificate select is a fiddler extension designed to allow users to select certificates directly from their certificate store for use with fiddler. If you use don't switch client certificates frequently you may be satisfied with the traditional method of attaching client certificates outlined [here](https://www.fiddlerbook.com/fiddler/help/httpsclientcerts.asp).

# Installing
## Using the Installer
Since v0.2.0 you can download and run InstallFiddlerClientCertificateSelect.exe from the releases. You can only install globally if you run as administrator, the application will not self elevate.

# Building
You will have to modify the project references to refer to your local copy of the fiddler executable. This extension was created against fiddler2 in Visual Studio 2013. More information about extending fiddler with .net can be found [here](http://docs.telerik.com/fiddler/extend-fiddler/extendwithdotnet). If you want to easily debug you will need to modify the debug action if your fiddler is not in C:/Program Files (x86)/Fiddler2/.

# Screenshots
![Menu Items](https://github.com/ameerkat/FiddlerClientCertificateSelect/blob/master/Screenshots/Menu_Items.png)
![Grid View Selector](https://github.com/ameerkat/FiddlerClientCertificateSelect/blob/master/Screenshots/Custom_Selector.png)
![Builtin Selector](https://github.com/ameerkat/FiddlerClientCertificateSelect/blob/master/Screenshots/Builtin_Selector.png)