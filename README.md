## Installing SkyeLibrary from a Local `.nupkg` File

After cloning this repo:

1. Open **Visual Studio**
2. Go to **Tools > NuGet Package Manager > Package Manager Console**
3. Run the following command, replacing the path with wherever you saved the `.nupkg` file:

    ```powershell
    Install-Package SkyeLibrary -Source "C:\Path\To\Your\Package"
    ```

> Example: If you saved the `.nupkg` to `Downloads`, use:
> ```powershell
> Install-Package SkyeLibrary -Source "C:\Users\YourName\Downloads"
> ```

Make sure the **Default Project** dropdown (top of the console) is set to the project you want to install into.
