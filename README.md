# ReactorAR | RTU-600 Training Simulator

![Platform](https://img.shields.io/badge/Platform-Unity-blue?logo=unity)
![XR](https://img.shields.io/badge/XR-ARFoundation-lightgrey)
![Architecture](https://img.shields.io/badge/Architecture-ServiceLocator%20%2F%20EventBus-green)
![Animation](https://img.shields.io/badge/Animation-DOTween-red)

A premium AR training application for the **RTU-600 Reactor Activation** procedure. Built with a focus on performance, modularity, and high-fidelity UI/UX.

## 🚀 Key Features

*   **AR-Guided Placement**: Robust surface detection using ARFoundation with stable anchor persistence.
*   **Dynamic Step Controller**: Data-driven training scenarios using Unity ScriptableObjects.
*   **Live Telemetry Simulation**: Real-time sensor fluctuation (RPM, Pressure, Temp) for immersive training.
*   **Modular 3D Swapping**: Smooth transition between reactor components and states using DOTween.
*   **Premium HUD Architecture**: Decoupled UI systems (Telemetry, Instructions, Scanning) for a professional cockpit experience.

## 🛠 Tech Stack

*   **Engine**: Unity 6+ (compatible with 2022.3 LTS+)
*   **XR Framework**: ARFoundation 6.x (ARCore / ARKit)
*   **Rendering**: Universal Render Pipeline (URP)
*   **Animation**: DOTween (Pro recommended for advanced sequences)
*   **UI**: TextMeshPro & Unity Canvas System
*   **Patterns**: Service Locator, Event Bus, Dependency Injection

## 📂 Project Structure

```text
Assets/_Project/
├── Documentation/      # Hardware specs and setup guides
├── Plugins/            # Third-party assets (DOTween, etc.)
├── Resources/          # ScriptableObject scenarios
└── Scripts/
    ├── AR/             # Plane detection and anchor handlers
    ├── Animation/      # Reusable tweening components
    ├── Core/           # Bootstrapping and service discovery
    ├── Data/           # Scenario and step definitions
    ├── Training/       # Simulation logic and model management
    └── UI/             # HUD controllers and screen navigation
```

## 📋 Getting Started

### Prerequisites
1. Unity 6+ installed.
2. Android/iOS build support modules.
3. **Packages required**: ARFoundation, ARCore/ARKit XR Plugins, DOTween, TextMeshPro.

### Setup
1. Clone the repository.
2. Follow the detailed [SETUP_GUIDE.md](Assets/_Project/Documentation/SETUP_GUIDE.md) for scene-specific hierarchy instructions.
3. Import DOTween and run its setup utility (`Tools > DOTween Utility Panel`).
4. Build to a supported AR device.

## 📐 Architecture Philosophy

The project follows a **decoupled architecture**:
*   **ServiceLocator**: Provides a central registry for core systems without singleton bloat.
*   **EventBus**: Allows the AR layer to communicate with the UI layer without direct references.
*   **ScriptableObjects**: Separates training content from simulation logic, making it easy to add new scenarios without writing more code.

---

**Developed for technical training environments.** 
*Senior Lead Developer: DUSHIME1212*
