# Skye Music 🎶

[⬇️ Download Skye Music](https://github.com/YodeSkye/SkyeMusic/releases/tag/v1.0)
![Skye Music screenshot](My%20Project/ScreenShot%2020251103.png)

A modern Windows media player built with **LibVLCSharp** and **WinForms**, featuring:
- Smooth audio & video playback.
- Playlist and queue management.
- Integrated library for browsing and managing media files.
- Metadata tag reading with **TagLib#**.
- Local database for play history & statistics.
- Lightweight installer powered by Inno Setup.

Support the project: https://github.com/sponsors/YodeSkye

---

# Using from the repo:

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

### 1. LibVLCSharp.WinForms
Provides bindings for the VLC media player in WinForms applications.

```powershell
Install-Package LibVLCSharp.WinForms
```

---

### 2. VideoLAN.LibVLC.Windows

Provides the Windows-specific implementation of the LibVLC library.

```powershell
Install-Package VideoLAN.LibVLC.Windows
```

---

### 3. NAudio

A powerful audio library for .NET, used for audio playback and processing.

```powershell
Install-Package NAudio
```

**Example:**
```vbnet
Dim waveOut As New NAudio.Wave.WaveOutEvent()
Dim audioFile As New NAudio.Wave.AudioFileReader("song.mp3")
waveOut.Init(audioFile)
waveOut.Play()

Dim MeterAudioCapture As New WasapiLoopbackCapture()
AddHandler MeterAudioCapture.DataAvailable, AddressOf OnDataAvailable
MeterAudioCapture.StartRecording()
```

---

### 4. TagLibSharp

A cross-platform library for reading and writing metadata in media files.

```powershell
Install-Package TagLibSharp
```

**Example:**
```vbnet
Dim file = TagLib.File.Create("song.mp3")
Console.WriteLine($"Title: {file.Tag.Title}")
file.Tag.Title = "New Title"
file.Save()
```

---

### 5. System.Data.SQLite.Core

Provides a lightweight, embedded SQL database engine for local storage of play history and metadata cache.

```powershell
Install-Package System.Data.SQLite.Core
```

**Usage Tip:**
```vbnet
'Example: open a connection to the local database file
Using conn As New SQLiteConnection("Data Source=skyeMusic.db;Version=3;")
    conn.Open()
    ' Execute commands here
End Using
```

---


### 6. Syncfusion.Tools.Windows

Provides advanced WinForms UI components like Ribbon, Docking Manager, and TreeView. It 's free for individual developers and small businesses. To get a license, visit their website [Syncfusion Community License](https://www.syncfusion.com/products/communitylicense).

```powershell
Install-Package Syncfusion.Tools.Windows
```

---

### 7. WinForms.DataVisualization

Provides the **System.Windows.Forms.DataVisualization.Charting** namespace, which includes chart controls (Pie, Bar, Radar, Pareto, etc.) used in the **History & Statistics** page.

```powershell
Install-Package WinForms.DataVisualization
```

**Example:**
```vbnet
Dim chart As New DataVisualization.Charting.Chart()
Dim area As New DataVisualization.Charting.ChartArea("Main")
chart.ChartAreas.Add(area)
Dim series As New DataVisualization.Charting.Series("Genres") With {
    .ChartType = DataVisualization.Charting.SeriesChartType.Pie
}
series.Points.AddXY("Rock", 10)
series.Points.AddXY("Pop", 5)
chart.Series.Add(series)
```

---

### 8. WordCloudSharp

Provides word cloud generation for visualizing artist frequency in SkyeMusic. Used in the **Artist Word Cloud** chart view.

```powershell
Install-Package WordCloudSharp
```

**Example:**
```vbnet
Dim words = {"Avril Lavigne", "Belinda Carlisle", "Lily Allen"}
Dim frequencies = {5, 3, 2}
Dim wc As New WordCloudSharp.WordCloud(
    width:=800,
    height:=600,
    useRank:=False,
    fontColor:=Color.Black,
    maxFontSize:=60,
    allowVerical:=False,
    fontname:="Segoe UI"
)
Dim bmp As Bitmap = wc.Draw(words, frequencies)
PictureBox1.Image = bmp
```

---

### 9. MetaBrainz.MusicBrainz

A .NET client for the MusicBrainz API, used for fetching additional metadata about music tracks and artists.

```powershell
Install-Package MetaBrainz.MusicBrainz
```

---

### 10. MetaBrainz.MusicBrainz.CoverArt

A .NET client for fetching cover art from the MusicBrainz Cover Art Archive.

```powershell
Install-Package MetaBrainz.MusicBrainz.CoverArt
```

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