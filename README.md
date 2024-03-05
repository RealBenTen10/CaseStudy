## Change to Master Branch to view and download the project

# CaseStudy
Case study for Bachelor's Thesis
Project for Hololens 2 using Unity

Unity Version: 2022.3.1.13f

Visual Studio Version 2022

MRTK Version: 2.8.3

This Project can be opened using Unity Hub.

6 different Versions of the game can be played.

To Upload the Game to Hololens 2:
  - In Unity (Hub):
    - Open the Project
    - Open the Scene (Assets/Scenes/SampleScene)
    - Click on "File" -> "Build Settings"
    - Choose UniversalWindowsPlatform (UWP)
      - Architecture: ARM 64-Bit
      - Build Type: D3D Project
      - Target SDK Version: Latest Installed (10.0.22621.0)
      - Minumim Platform Version: 10.0.10240.0
      - Visual Studio Version: Latest Installed (VS 2022)
      - Build and Run on: Local Machine
      - Build Configuration: Release
    - Add the Scene to Build (click on "Add Open Scenes")
    - Click on "Build" (create a new folder to build your project in)
  - In Visual Studio (2022):
    - Open the Project (e.g. double click on the .sln file in your folder)
    - change the settings to "Release", "ARM64" and "Device" (if you upload via USB)
    - Right-click on the project name (e.g. "Study1") and select "Deploy"

Or follow this detailed guide: http://www.lancelarsen.com/xr-step-by-step-2023-hololens-2-setting-up-your-project-in-unity-2022-mrtk-2-8-3-visual-studio-2022/

To Change Version:
  - Open the C#-Script "Algorithm" in your Assets folder and change "group_name = 1" to whichever group you want to test (group 1 - 6 exist)
  - If the group_name is empty or something else (not "1", "2", "3", "4", "5" or "6") the order will be 1-10 and not shuffled
  - Click on "Edit" -> "Player Settings" and change the name of the application if you want to upload more than 1 Version
