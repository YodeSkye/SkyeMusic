# Skye Music 🎶

[⬇️ Download Skye Music](https://github.com/YodeSkye/SkyeMusic/releases/tag/v1.4)
![Skye Music screenshot](https://raw.githubusercontent.com/YodeSkye/SkyeMusic/refs/heads/master/share/ScreenshotDirectory3.png)

A modern Windows media player built with **LibVLCSharp** and **WinForms**, featuring:
- Smooth audio & video playback.
- Playlist and queue management.
- Integrated library for browsing and managing media files.
- Integrated directory for browsing online streams and podcasts.
- Metadata tag reading with **TagLib#**.
- A compact mini-player mode.
- Local database for play history & statistics.
- Lightweight installer powered by Inno Setup.
- Upgrade notifications for new versions.
- Open source and free to use!

Support the project: https://github.com/sponsors/YodeSkye

---

# Using from the repo:

### Requirements
- Visual Studio 2022 or 2026
- .NET 10 SDK

## 📥 Installing SkyeLibrary from a Local `.nupkg` File (included in the repo)

SkyeClip depends on SkyeLibrary, which is included as a .nupkg file in this repository.
To install it (After cloning this repo):

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

## 🧩 Required Licensing

### 🔑 Syncfusion License Key Setup

SkyeMusic uses Syncfusion UI components. To run the app without trial limitations or watermarks, you’ll need a free community license key from Syncfusion.

**Steps:**
1. Visit https://www.syncfusion.com/ and register for a free community license.
2. Copy `LicenseKey.Sample.vb` to `LicenseKey.vb` in the project root.
3. Replace `"YOUR_ACTUAL_LICENSE_KEY_HERE"` with your own key.
4. Do **not** commit `LicenseKey.vb`—it’s ignored by default via `.gitignore`.

You can find your license key in the **License & Downloads** section of your Syncfusion account.  
For more information, visit https://help.syncfusion.com/common/essential-studio/licensing/license-key