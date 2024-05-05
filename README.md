# Help Me Decorate - CS6334 Spring 2024

Welcome to **Help Me Decorate**, a virtual reality home decoration app developed by us for the CS6334 course in Spring 2024. This game offers an immersive home decor experience, allowing players to engage in a home decor experience for clients. The player has to decorate the room according to the requirements of the customers. The customers can be selected through the avatar selection screen. Each customer has their own preferences which would be based on a trained ML model. The player can place different types of furnitures and change their position and color. The customer would grade the final room decor based on the ML model.

## Team Members
- Shangyi Chen
- Pratyush Kware
- Zhouhang Sun
- Jihan Wang
  
## Scenes
Our game comprises three scenes:
- **Start Scene**: `Menu.unity` // Title Screen
- **Avatar Selection Scene**: `selection.unity` // Present in the prototype demo.
- **Decor Scene**: `main_scene.unity` // Present in the prototype demo.
- **End Scene**: `End.unity` //Score and Retry screen

### Building the APK
When building the APK for Android, ensure the following scenes are included in the "Scenes in Build":
1. `[0] SCENES/Menu`
2. `[1] SCENES/selection`
3. `[2] SCENES/main_scene`
4. `[3] SCENES/End`

## Interaction Techniques
- **Travel**: Movement is controlled by the Bluetooth Controller joystick. Of course the player can look around using the phone movement.
- **Selection and Manipulation**: Employing Ray-Casting with pointing metaphors for object interaction. Objects are selected and manipulated using a raycast and Bluetooth Controller buttons. Context menus show up for selecting specific action to be performed.
- **System Control**: Utilize the Bluetooth Controller for system controls and interaction, including menu navigation and object manipulation.

## Avatar Selection
Our VR experience features a customizable avatar selection scene where users can choose the customers virtual representations. We've meticulously crafted these avatars by modeling facial features based on real human photographs using Blender and FaceBuilder. This approach ensures a more immersive and personalized experience for our users.

## Intelligent Grading System
We've integrated an intelligent system that dynamically evaluates users' room decorations. This system considers factors such as space utilization and aesthetic harmony to assign a grade to the user's decoration efforts. With every modification made to the room, the system recalculates the grade and gives feedback value on the screen. The model will be trained on different room configurations correspondingly labelled good and bad. Every customer will have a different model based on their preferences. We created the room configurations dataset ourselves.

### Target Device
Android

## Equipment
- Google Cardboard
- HWD Bluetooth Controller(Buttons to interact using the raycast. Joystick for movement.)

## Demos
- [Preliminary Prototype Demo](https://youtu.be/fvvBiin__44)
- [Final Prototype Demo](https://www.youtube.com/watch?v=wW1ouEXEjbY)


Thank you!
