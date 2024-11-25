# VizVid Simple Lock

An unofficial add-on for the VRChat VizVid video player system that adds a simple lock button. It interacts with the `Lock` and `OnUnlock` events exposed by VizVid's `Playlist Queue Handler`.

## Setup

### Dependencies

- Unity 2022.3.22f1 (or latest supported version).
- VRC SDK 3.x.x (Tested against 3.7.3 but should work in any version compatible with VizVid).
- [VizVid](https://github.com/JLChnToZ/VVMW) (Tested Against 1.3.3 but should work back to 1.2.0)

These should be installed automatically by the VCC when you add this package to your project.

### Install

1. Go to the [VPM Listing](https://lackofbindings.github.io/VizVidSimpleLock/) for this repo and hit "Add to VCC".
   
   - If the "Add to VCC" button does not work you can manually enter the following url into the Packages settings page of the VCC `https://lackofbindings.github.io/VizVidSimpleLock/index.json` 

   - If you do not have access to the VCC, there are also unitypackage versions available in the [Releases](https://github.com/lackofbindings/VizVidSimpleLock/releases/latest).

2. Once you have the repo added to your VCC, you can add VizVid Simple Lock to your project from the Mange Project screen.

### Setup

1. Ensure that [VizVid](https://github.com/JLChnToZ/VVMW) is set up and working in your project before proceeding.

2. Drag the `VizVid Simple Lock` prefab into your scene.

   - It is designed to fit in the empty space in the upper left of the narrow separated controls, but you can put it anywhere you want.

3. Fill out the `Playlist Queue Handler` field on the Lock manager script by dragging in the one from your VizVid instance or by clicking the `Auto Find` button.

#### Multiple Buttons

   - The `VizVid Simple Lock` prefab contains both a Lock Manager and Lock button for easy setup, but they also exist separately via the individual [Prefabs](./Packages/com.lackofbindings.vizvidsimplelock/Prefabs/).

   - Only one instance of the Lock Manager can be in your scene at a time, but you can have as many Lock Buttons as you want. 

   - To add additional buttons, drag a Lock Button prefab into your scene and Fill out the `Lock Manager` field by dragging in the one from your scene or by clicking the `Auto Find` button.
     - Lock Buttons should automatically register themselves with their Lock Manager, but you may also wish to assign them manually in the array on the Lock Manager for performance reasons.


## Usage

- While unlocked, any player can click the lock to lock the controls to them.
- While locked, the lock displays the name of the user that has control (the owner).
- The Instance Owner can always toggle the lock, even if it is currently locked to someone else.
