# GreatVRCookOff - CS6334 Spring 2023

Welcome to **GreatVRCookOff**, a virtual reality cooking game developed by students for the CS6334 course in Spring 2023. This game offers an immersive cooking experience using VR technology, allowing players to engage in a culinary adventure from the comfort of their homes.

## Team Members
- Tahmid Imran
- Ethan Roceles
- Chethan Birur Nataraja
- Mustafa Poonawala
- Michelle Kelman

## Scenes
Our game comprises several unique scenes, each offering different gameplay mechanics and interactions:
- **Start Scene**: `StartScene.unity`

### Building the APK
When building the APK for Android, ensure the following scripts are included in the "Scenes in Build":
1. `[0] SCRIPTS/StartScene`
2. `[1] SCRIPTS/CreateOrJoinScene`
3. `[2] SCRIPTS/KitchenScene`

### Controller Button Mapping
To modify the controller button mappings in the Unity Editor:
1. Navigate to `Assets > Resources > Character`.
2. In the Inspector, scroll to `Button Mapping (Script)`.
3. Change the button mappings as required before building.

## Interaction Techniques
- **Travel**: Utilize Gaze and Hand Directed Steering with steering metaphors. Movement is controlled by the headset's gaze direction and the hand tracker joystick.
- **Selection and Manipulation**: Employ Ray-Casting with pointing metaphors for object interaction. Objects are selected and manipulated using the reticle pointer and Bluetooth Controller.
- **System Control**: Utilize the Bluetooth Controller for system controls and interaction, including menu navigation and object manipulation.

## Equipment
- Android Phone
- Google Cardboard
- HWD Bluetooth Controller (Joystick for travel and menu selection, OK Button, Power Button, Menu Button for settings menu, X Button for menu navigation, A Button for returning objects, B Button for object manipulation)

## Multiplayer
**Great VR Cook Off** supports multiplayer gameplay:
- **Creating a Multiplayer Game**: Player 1 creates a game with a 6 digit room code.
- **Joining a Multiplayer Game**: Players 2-4 join using the provided room code.
- Starting the game requires all players to be ready in the waiting area.

### Single Player Mode
- Launch the `.apk` and select "SINGLE PLAYER" to play in single-player mode.

## Demos
- [Preliminary Prototype Demo](https://www.youtube.com/watch?v=8IgeLQmDzME)
- [Final Prototype Demo](https://www.youtube.com/watch?v=C5bu_1EK5XU)

Thank you for exploring **GreatVRCookOff**. Dive into the culinary world of VR and unleash your cooking talents!
