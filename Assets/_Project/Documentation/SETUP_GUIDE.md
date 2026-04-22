# AR Training App | Setup & Hierarchy Guide

This guide explains how to assemble the project in Unity, attach the scripts, and organize your UI (HUDs) for a premium experience.

## 1. Scene Organization

### Scene A: `01_Boot`
*   **[AppBootstrap](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/Core/AppBootstrap.cs)**: Create a `[SYSTEM]` object. Attach `AppBootstrap` and `ServiceLocator`.
*   **[BootScreen](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/BootScreen.cs)**: Attach to a `Canvas/BootOverlay`. Link your logo and background.
*   **[SceneLoader](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/Core/SceneLoader.cs)**: Create a `[SCENE_MANAGER]` object. Attach `SceneLoader` and a `CanvasGroup` for the black fade.

### Scene B: `02_HomeMenu`
*   **[HomeMenuUI](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/HomeMenuUI.cs)**: Attach to the main `Canvas`.
    *   Link `navButtons` (Home, Simulator, Settings).
    *   Link `scenarioCards` (Prefabs of cards in the ScrollRect).
*   **[NavTabController](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/NavTabController.cs)**: Attach to the `NavigationBar` object.

### Scene C: `03_Simulation`
*   **XR Origin (AR Foundation)**:
    *   Attach `ARPlaneManager`, `ARRaycastManager`, `ARAnchorManager`.
    *   Attach **[ARRaycastHandler](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/AR/ARRaycastHandler.cs)** and **[AnchorManager](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/AR/AnchorManager.cs)**.
*   **Training Manager**: Create a `[TRAINING_CONTROLLER]` object.
    *   Attach **[StepController](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/Training/StepController.cs)**.
    *   Attach **[ModelSwapManager](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/Training/ModelSwapManager.cs)**.
    *   Attach **[TelemetrySimulator](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/Training/TelemetrySimulator.cs)**.

---

## 2. UI Layers (The HUDs)

Ensure your Canvas is set to `Screen Space - Overlay` with a `Canvas Scaler` (Match Height = 0.5).

### Top: Instruction HUD
*   **Object**: `Canvas/HUD_Top`
*   **Script**: **[StepInstructionUI](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/StepInstructionUI.cs)**
*   **Design**: A sleek, dark glass container with a progress bar and the current step instruction.

### Side: Telemetry HUD
*   **Object**: `Canvas/HUD_Left`
*   **Script**: **[TelemetryHUDCtrl](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/TelemetryHUDCtrl.cs)**
*   **Content**: A vertical list of "Telemetry Badges". Each badge needs a `TextMeshProUGUI` for Value and Unit.

### Bottom: Scanning HUD
*   **Object**: `Canvas/HUD_Scanning`
*   **Script**: **[ScanningHUD](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/ScanningHUD.cs)**
*   **Content**: The diamond reticle visual and the "Floor detected — tap to place" label.

### Popup: Parameter Panel
*   **Object**: `Canvas/Panel_Parameter`
*   **Script**: **[ParameterPanelCtrl](file:///c:/Users/don360/Desktop/csharp/AR%20training%20app/Assets/_Project/Scripts/UI/ParameterPanelCtrl.cs)**
*   **Content**: A slider for adjustments (RPM, Pressure) and an Apply button.

---

## 3. Required Assets

### 🎨 Icons (Flat, Monochromatic)
| Icon Name | Purpose | Suggested Source |
|---|---|---|
| `ic_power` | Power state | [Google Fonts / Icons](https://fonts.google.com/icons) |
| `ic_temp` | Temperature telemetry | [Lucide Icons](https://lucide.dev) |
| `ic_pressure` | Pressure badges | [Lucide Icons](https://lucide.dev) |
| `ic_rpm` | Stirrer speed | [Lucide Icons](https://lucide.dev) |
| `ic_diamond` | AR Placement reticle | Custom (simple rotated square) |

### 🔊 Sound Effects
| File Name | Trigger |
|---|---|
| `ui_tap.wav` | Button clicks |
| `scanned.wav` | When a plane is found |
| `step_success.mp3` | Advancing to next step |
| `ambient_hum.wav` | Background loop for reactor |

### 📦 3D Models & Shaders
*   **Models**: Search **Sketchfab** or **Unity Asset Store** for:
    *   "Industrial Water Heater"
    *   "RTU Controller Panel"
    *   "Valve Assembly"
*   **Shaders (ShaderGraph)**:
    *   **UI Blur**: Use a UI material with a GrabPass or a simple Gaussian Blur filter.
    *   **Model Dissolve**: Create a Lit ShaderGraph with a `Step` node using a `Noise` texture for the `Alpha Clip Threshold`. This is essential for the **ModelSwapManager** effects.

---

## 5. CI/CD Activation (GitHub Actions)

The project includes an automated pipeline in `.github/workflows/build-android.yml`. Every push to `main` will generate a release APK.

### 🔑 Required GitHub Secrets
You **must** add these to your GitHub Repo Settings (`Settings > Secrets > Actions`):
1.  **`UNITY_EMAIL`**: Your Unity account email.
2.  **`UNITY_PASSWORD`**: Your Unity account password.
3.  **`UNITY_LICENSE`**: The content of your `.ulf` license file.

### 📜 How to get your `UNITY_LICENSE`:
1.  On your local machine, run Unity and ensure you are logged in.
2.  Locate your license file:
    *   **Windows**: `C:\ProgramData\Unity\Unity_lic.ulf`
    *   **Mac**: `/Library/Application Support/Unity/Unity_lic.ulf`
3.  Open the file in a text editor, copy the entire XML content, and paste it into the `UNITY_LICENSE` secret on GitHub.
4.  *Note: If you use a Personal license, you may need to use the [Game-CI Activation Workflow](https://game.ci/docs/github/activation) to generate a request file manually.*

---

## 6. Senior Developer Tips
1.  **Prefab Everything**: Turn every HUD layer and your model anchors into Prefabs.
2.  **Universal Render Pipeline (URP)**: Since this is AR, ensure you are using URP. It allows for better performance and the `MaterialPropertyBlock` tricks used in our scripts.
3.  **DOTween Setup**: After importing DOTween from the Asset Store, go to `Tools > DOTween Utility Panel > Setup` and click **"Generate ASMDEF"** to ensure standard namespaces work correctly.
