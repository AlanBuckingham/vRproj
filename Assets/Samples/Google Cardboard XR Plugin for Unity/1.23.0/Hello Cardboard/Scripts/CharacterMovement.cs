using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController charCntrl;
    [Tooltip("The speed at which the character will move.")]
    public float speed = 5f;
    [Tooltip("The camera representing where the character is looking.")]
    public GameObject cameraObj;
    [Tooltip("Should be checked if using the Bluetooth Controller to move. If using keyboard, leave this unchecked.")]
    public bool joyStickMode;

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentSpeed = GameObject.Find("MainMenuEventSystem").GetComponent<MainMenuEventSystemScript>().getMainMenu().GetComponent<MainMenuFunctions>().getCharacterSpeed() * GameObject.Find("MainMenuEventSystem").GetComponent<MainMenuEventSystemScript>().getMainMenu().GetComponent<MainMenuFunctions>().stopMoving; ;

        ////Get horizontal and Vertical movements
        //float horComp = Input.GetAxis("Horizontal");
        //float vertComp = Input.GetAxis("Vertical");

        float inputVertical = joyStickMode ? Input.GetAxis("Vertical") : Input.GetAxis("Horizontal");
        float inputHorizontal = joyStickMode ? Input.GetAxis("Horizontal") * -1 : Input.GetAxis("Vertical");

        // Get the camera's current forward and right vectors
        Vector3 cameraForward = cameraObj.transform.forward;
        Vector3 cameraRight = cameraObj.transform.right;

        // Optionally, remove the influence of pitch (up/down tilt)
        cameraForward.y = 0;  // Uncomment this line to restrict vertical movement
        cameraRight.y = 0;    // Uncomment this line to restrict vertical movement

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement vector based on camera orientation and input
        Vector3 movement = (cameraForward * inputVertical + cameraRight * inputHorizontal) * currentSpeed;

        // Apply the movement
        charCntrl.SimpleMove(movement);


    }

    public void Teleport(Vector3 teleportDestination)
    {
        // Ensure any active movement or forces are stopped before teleporting
        charCntrl.enabled = false;

        // Set the character's position to the teleport destination
        charCntrl.transform.position = teleportDestination;

        // Re-enable the character controller
        charCntrl.enabled = true;
    }
}
