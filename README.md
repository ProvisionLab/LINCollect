# LincLocal
LincLocal is an application for passing surveys.

# Features
1. Question types:
  * Text
  * Choice Across (Radiobutton, Checkbox)
  * Choice Down (Radiobutton, Checkbox)
  * Drop Down
  * Matrix
  * Slider
2. Individual questions for choosed companies
3. Ability to add new company

# Requirements
* IDE: [Android Studio](https://developer.android.com/studio/index.html)
* minSdkVersion 16
* targetSdkVersion 25

# Installation and Run
1. Open https://github.com/ProvisionLab/LINCollect
2. Clone project using git bash to your local machine via command git@github.com:ProvisionLab/LINCollect.git
3. In git bash switch to “android” branch
4. Start Android Studio
5. If there is open project go to File->Open, else choose “Open an existing Android Studio project”
6. Open ...\LINCollect\android\LincLocal
7. To build and run project on device go to Run->Run ‘app’
8. APK file you could find in ...\android\app\build\outputs\apk

All libs should be synced via gradle, there is no external jar libs.
