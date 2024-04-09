# Home Decoration - CS6334 Spring 2024

Welcome to **Home Decoration**, a virtual reality home decoration app developed by students for the CS6334 course in Spring 2024. This game offers an immersive home decoration experience, allowing players to engage in a culinary adventure from the comfort of their homes.

## Team Members
- Shangyi Chen
- Pratyush Kware
- Zhouhang Sun
- Jihan Wang
  
## Scenes
Our game comprises several unique scenes, each offering different gameplay mechanics and interactions:
- **Start Scene**: `StartScene.unity`
- **Avatar Selection Scene**: `StartScene.unity`

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

Avatar Selection
Our VR experience features a customizable avatar selection scene where users can choose their virtual representations. We've meticulously crafted these avatars by modeling full-body and facial features based on real human photographs using Blender. This approach ensures a more immersive and personalized experience for our users.

Intelligent Grading System
We've integrated an intelligent system that dynamically evaluates users' room decorations. This system considers factors such as space utilization and aesthetic harmony to assign a grade to the user's decoration efforts. With every modification made to the room, the system recalculates the grade, and our Non-Player Characters (NPCs) respond with appropriate emojis to reflect the user's design prowess. This feature adds a layer of interactivity and feedback, encouraging users to experiment with their virtual spaces.




User



## Equipment
- Android Phone
- Google Cardboard
- HWD Bluetooth Controller (Joystick for travel and menu selection, OK Button, Power Button, Menu Button for settings menu, X Button for menu navigation, A Button for returning objects, B Button for object manipulation)

### Single Player Mode
- Launch the `.apk` and select "SINGLE PLAYER" to play in single-player mode.

## Demos
- [Preliminary Prototype Demo](https://www.youtube.com/watch?v=8IgeLQmDzME)
- [Final Prototype Demo](https://www.youtube.com/watch?v=C5bu_1EK5XU)

Thank you for exploring **Home Decoration**. Dive into the culinary world of VR and unleash your cooking talents!
