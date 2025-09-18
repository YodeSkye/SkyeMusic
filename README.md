
## 📥 Installing SkyeLibrary from a Local `.nupkg` File

After cloning this repo:

1. Open **Visual Studio**
2. Go to **Tools > NuGet Package Manager > Package Manager Console**
3. Run the following command, replacing the path with wherever you saved the `.nupkg` file:

    ```powershell
    Install-Package SkyeLibrary -Source "C:\Path\To\Your\Package"
    ```

    > 💡 Example: If you saved the `.nupkg` to `Downloads`, use:
    > ```powershell
    > Install-Package SkyeLibrary -Source "C:\Users\YourName\Downloads"
    > ```

Make sure the **Default Project** dropdown (top of the console) is set to the project you want to install into.

---

## 📦 Required NuGet Packages

To ensure full functionality of **Skye Music**, install the following NuGet packages:

### 1. Syncfusion.Tools.Windows

Provides advanced WinForms UI components like Ribbon, Docking Manager, and TreeView. It 's free for individual developers and small businesses. To get a license, visit their website [Syncfusion Community License](https://www.syncfusion.com/products/communitylicense).

```powershell
Install-Package Syncfusion.Tools.Windows
```

---

### 2. System.Text.Encoding.CodePages

Adds support for legacy code page encodings (e.g., Windows-1252, Shift-JIS).

```powershell
Install-Package System.Text.Encoding.CodePages
```

**Usage Tip:**
```csharp
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
```

---

### 3. TagLibSharp

A cross-platform library for reading and writing metadata in media files.

```powershell
Install-Package TagLibSharp
```

**Example:**
```csharp
var file = TagLib.File.Create("song.mp3");
Console.WriteLine($"Title: {file.Tag.Title}");
file.Tag.Title = "New Title";
file.Save();
```

---

## 🧩 Required COM References

### 1. Windows Media Player COM Component

To add the Windows Media Player COM component:

1. Right-click on your project in **Solution Explorer**.
2. Select **Add > Reference**.
3. In the Reference Manager, go to **COM**.
4. Find and check **Windows Media Player**.
5. Click **OK** to add the reference.

---

### 2. CoreAudio

To add the CoreAudio component:

1. Right-click on your project in **Solution Explorer**.
2. Select **Add > COM Reference**.
3. In the Reference Manager, click **Browse**.
4. Navigate to the path where you saved the `CoreAudio.dll` file from the repository.
5. Select `CoreAudio.dll` and click **OK** to add the reference.
