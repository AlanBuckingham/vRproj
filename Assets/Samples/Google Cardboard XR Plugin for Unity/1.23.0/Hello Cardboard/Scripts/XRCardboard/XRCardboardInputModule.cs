using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;
#if !UNITY_EDITOR
using UnityEngine.XR;
#endif

public class XRCardboardInputModule : PointerInputModule
{
    [SerializeField]
    XRCardboardInputSettings settings = default;

    GameObject currentTarget;
    Outline outl;
    bool outOfRange = false;
    GameObject raycastObj = null;
    RaycastHit loc;

    bool bool_obj_being_manipulated = false;
    GameObject obj_being_manipulated;
    float prev_score = 0f;

    public override void Process()
    {
        HandleLook();
        HandleSelection();
    }

    void HandleLook()
    {

        GameObject lineR = GameObject.Find("Line");
        GameObject myObject = GameObject.Find("MainMenuEventSystem");
        var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getMainMenu().GetComponent<MainMenuFunctions>();
        loc = lineR.GetComponent<LineRaycastFunctions>().getHitPoint(pobj.getRaycastLength());
        //Debug.Log(loc.point);
        try
        {
            raycastObj = loc.transform.gameObject;
            
        }
        catch (NullReferenceException)
        {
            raycastObj = null;
        }

        if (bool_obj_being_manipulated)
        {
            obj_being_manipulated.transform.position = loc.point;
            obj_being_manipulated.transform.rotation = obj_being_manipulated.transform.rotation;
            //Debug.Log(obj_being_manipulated.transform.position);
        }
        //Disable not used ones.
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("object");

        foreach (GameObject obj in objectsWithTag)
        {
            outl = obj.GetComponent<Outline>();

            if (outl.enabled && (obj != raycastObj || loc.distance > pobj.getRaycastLength()))
            {
                outl.enabled = false;
                if (obj.tag == "button")
                    obj.GetComponent<Renderer>().material.color = Color.white;
            }

        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("button");

        foreach (GameObject obj in objectsWithTag)
        {
            outl = obj.GetComponent<Outline>();

            if (outl.enabled && (obj != raycastObj || loc.distance > pobj.getRaycastLength()))
            {
                outl.enabled = false;
                obj.GetComponent<Renderer>().material.color = Color.white;
            }

        }

        if (raycastObj != null)
        {
            if (loc.distance < pobj.getRaycastLength())
            {
                outOfRange = false;
                outl = raycastObj.GetComponent<Outline>();
                if (outl != null)
                    outl.enabled = true;
                if (raycastObj.tag == "button")
                    raycastObj.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                outOfRange = true;
            }

        }
    }

    async void PlayScore()
    {
        var pobj = GameObject.Find("alannormalpose").GetComponent<evaluate_room>();
        float new_score = pobj.getEval(false);
        var anima = GameObject.Find("Feedback").GetComponent<Animator>();
        if ((int)(new_score - prev_score) >= 0)
        {
            //play animation
            var panel = GameObject.Find("Panel").GetComponent<Image>();
            panel.color = new Color(0, 1, 0, 0);
            var score_txt = GameObject.Find("change_score").GetComponent<TextMeshPro>();
            score_txt.text = "+" + ((int)(new_score - prev_score)).ToString();
            anima.enabled = true;
            anima.Play("win_animation");
            await Task.Delay(1000);
            anima.enabled = false;
            panel.color = new Color(0, 0, 0, 0);
            score_txt.text = "";
        }
        else
        {
            //play red animation
            var panel = GameObject.Find("Panel").GetComponent<Image>();
            panel.color = new Color(1, 0, 0, 0);
            var score_txt = GameObject.Find("change_score").GetComponent<TextMeshPro>();
            score_txt.text = "-" + ((int)(-new_score + prev_score)).ToString();
            anima.enabled = true;
            anima.Play("win_animation");
            await Task.Delay(1000);
            anima.enabled = false;
            panel.color = new Color(0, 0, 0, 0);
            score_txt.text = "";
        }
        prev_score = new_score;
    }
    async void HandleSelection()
    {
        GameObject handler = null;
        try
        {
            if (!outOfRange)
                handler = raycastObj;
            var selectable = handler.GetComponent<Selectable>();

            if ((selectable && selectable.interactable == false))
                throw new NullReferenceException();

        }
        catch (NullReferenceException)
        {
            currentTarget = null;
            return;
        }

        if (currentTarget != handler)
            currentTarget = handler;

        //Debug.Log(currentTarget.name);
        if (bool_obj_being_manipulated && Input.GetButtonDown(settings.ClickInputX))
        {
            var inst_obj = GameObject.Find("Instructions").GetComponent<InstructionsFunctions>();
            if (inst_obj.press_count != 0)
            {
                inst_obj.press_count = 0;
                inst_obj.inst_obj1.SetActive(true);
                inst_obj.inst_obj2.SetActive(false);
            }
            else
                obj_being_manipulated.transform.RotateAround(obj_being_manipulated.transform.position, Vector3.up, 45);

        }


        else if (bool_obj_being_manipulated && Input.GetButtonDown(settings.ClickInputB))
        {
            var inst_obj = GameObject.Find("Instructions").GetComponent<InstructionsFunctions>();
            if (inst_obj.press_count != 0)
            {
                inst_obj.press_count = 0;
                inst_obj.inst_obj1.SetActive(true);
                inst_obj.inst_obj2.SetActive(false);
            }
            else
            {
                if (!obj_being_manipulated.name.EndsWith("_TV"))
                {
                    if (obj_being_manipulated.GetComponent<Renderer>().material.color == new Color(0.50196f, 0, 0, 1))  // Maroon
                        obj_being_manipulated.GetComponent<Renderer>().material.color = new Color(0.76471f, 0.69020f, 0.56863f, 1);  // Khaki Green
                    else if (obj_being_manipulated.GetComponent<Renderer>().material.color == new Color(0.76471f, 0.69020f, 0.56863f, 1))  // Khaki Green
                        obj_being_manipulated.GetComponent<Renderer>().material.color = new Color(0, 0, 0.5451f, 1);  // Dark Blue
                    else if (obj_being_manipulated.GetComponent<Renderer>().material.color == new Color(0, 0, 0.5451f, 1))  // Dark Blue
                        obj_being_manipulated.GetComponent<Renderer>().material.color = new Color(0.50196f, 0.50196f, 0.50196f, 1);  // Grey
                    else
                        obj_being_manipulated.GetComponent<Renderer>().material.color = new Color(0.50196f, 0, 0, 1);  // Maroon

                }
            }
        }

        //Object kept back down.
        else if (bool_obj_being_manipulated && Input.GetButtonDown(settings.ClickInputA))
        {
            bool_obj_being_manipulated = false;
            obj_being_manipulated.GetComponent<MeshCollider>().enabled = true;
            var inst_obj = GameObject.Find("Instructions").GetComponent<InstructionsFunctions>();
            //Debug.Log(inst_obj.press_count);

            inst_obj.inst_obj1.SetActive(false);
            inst_obj.inst_obj2.SetActive(false);
            RotationLocker locker = obj_being_manipulated.GetComponent<RotationLocker>();
            locker.lock_rotation();
            obj_being_manipulated = null;
            inst_obj.press_count = 0;
            inst_obj.useInstructions = false;
            PlayScore();
        }

        else if (bool_obj_being_manipulated && Input.GetButtonDown(settings.ClickInputY))
        {
            var inst_obj = GameObject.Find("Instructions").GetComponent<InstructionsFunctions>();
            //Debug.Log(inst_obj.press_count);
            if (inst_obj.press_count == 1)
            {
                inst_obj.inst_obj1.SetActive(false);
                inst_obj.inst_obj2.SetActive(true);
            }
            else if (inst_obj.press_count > 1)
            {
                inst_obj.inst_obj2.SetActive(false);
                Destroy(obj_being_manipulated);
                bool_obj_being_manipulated = false;
                obj_being_manipulated = null;
                inst_obj.press_count = 0;
                inst_obj.useInstructions = false;
                PlayScore();
            }
        }

        else if ((currentTarget.tag == "object") && Input.GetButtonDown(settings.ClickInputX))
        {
            var obj = currentTarget;
            GameObject myObject = GameObject.Find("MainMenuEventSystem");
            var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getContextMenu().GetComponent<ContextMenuFunctions>();
            pobj.setObject(currentTarget);
            var pos = obj.transform.position;
            pos[0] += 1.5f;
            pobj.SetPosition(pos);

        }
        else if (Input.GetButtonDown(settings.ClickInputB) && (currentTarget.name == "Cut_button" || currentTarget.name == "Copy_button" || currentTarget.name == "Exit_button"))
        {

            GameObject myObject = GameObject.Find("ContextMenu");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            if (currentTarget.name == "Cut_button")
            {
                pobj.setAction(1);
                pobj.getObject().SetActive(false);
            }
            else if (currentTarget.name == "Copy_button")
            {
                pobj.setAction(2);

            }
            else
            {
                pobj.setAction(3);
            }
            pobj.resetMenu();
        }
        else if ((currentTarget.name == "default_alan" || currentTarget.name == "default_shawn") && Input.GetButtonDown(settings.ClickInputA))
        {
            GameObject.Find("menusound").GetComponent<AudioSource>().Play();
            var pobj = GameObject.Find("ContextMenuCust").GetComponent<ContextMenuFunctions>();
            //pobj.setObject(currentTarget);
            var pos = loc.point;
            
            pos[2] += 1f;
            pobj.SetPosition(pos);
            Camera mainCamera = Camera.main;
            // Make the object face the camera
            if (mainCamera != null)
            {
                Vector3 directionToCamera = (mainCamera.transform.position - pobj.GetPosition()).normalized;
                // Set the object to face towards the camera, ignore y axis if you do not want it to tilt upwards/downwards
                directionToCamera.y = pobj.GetPosition().y;
                //directionToCamera.y = 0; // Optional: Remove this line if tilting is desired
                Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
                pobj.SetRotation(targetRotation);
            }
            //pobj.SetPlacementLoc(loc.point);
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Eval_button")
        {
            GameObject.Find("menusound").GetComponent<AudioSource>().Play();
            var pobj = GameObject.Find("alannormalpose").GetComponent<evaluate_room>();
            var score = pobj.getEval(true);
            currentTarget.transform.position = new Vector3(0f,0f,0f);
            DataHolder.Char_sel = Mathf.RoundToInt(score);
            
            //Debug.Log(Mathf.RoundToInt(score));
            await Task.Delay(3000);
            SceneManager.LoadScene(sceneName: "End");
            //display 
        }
        else if (currentTarget.name == "Plane" && Input.GetButtonDown(settings.ClickInputA))
        {
            GameObject.Find("menusound").GetComponent<AudioSource>().Play();
            GameObject myObject = GameObject.Find("Character");
            var obj = currentTarget;
            myObject = GameObject.Find("MainMenuEventSystem");
            var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getContextMenuPlane().GetComponent<ContextMenuFunctions>();
            pobj.setObject(currentTarget);
            var pos = loc.point;
            pos[1] += 1f;
            pobj.SetPosition(pos);
            Camera mainCamera = Camera.main;
            // Make the object face the camera
            if (mainCamera != null)
            {
                Vector3 directionToCamera = (mainCamera.transform.position - pobj.GetPosition()).normalized;
                // Set the object to face towards the camera, ignore y axis if you do not want it to tilt upwards/downwards
                directionToCamera.y = pobj.GetPosition().y;
                //directionToCamera.y = 0; // Optional: Remove this line if tilting is desired
                Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
                pobj.SetRotation(targetRotation);
            }
            pobj.SetPlacementLoc(loc.point);

        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Add_button")
        {
            GameObject.Find("menusound").GetComponent<AudioSource>().Play();
            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();

            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.SetPosition(myObject.transform.position);
            Camera mainCamera = Camera.main;
            // Make the object face the camera
            if (mainCamera != null)
            {
                Vector3 directionToCamera = (mainCamera.transform.position - pobj1.GetPosition()).normalized;
                // Set the object to face towards the camera, ignore y axis if you do not want it to tilt upwards/downwards
                directionToCamera.y = pobj1.GetPosition().y;
                //directionToCamera.y = 0; // Optional: Remove this line if tilting is desired
                Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
                pobj1.SetRotation(targetRotation);
            }
            pobj.resetMenu();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Sofa_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            spawnLoc[1] += 1;
            GameObject clonedObject = Instantiate(GameObject.Find("Couch"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_Sofa";
            clonedObject.tag = "furniture_sitting";
            clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.left, 90);
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }

        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Chair_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            GameObject clonedObject = Instantiate(GameObject.Find("Chair"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_Chair";
            clonedObject.tag = "furniture_sitting";
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "TV_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            spawnLoc[1] += 1f;
            GameObject clonedObject = Instantiate(GameObject.Find("TV"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_TV";
            clonedObject.tag = "furniture_entertainment";
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Book_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            spawnLoc[1] += 0.5f;
            GameObject clonedObject = Instantiate(GameObject.Find("Bookshelf"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_Bookshelf";
            clonedObject.tag = "furniture_entertainment";
            clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.left, 90);
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Table_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            spawnLoc[1] += 0.5f;
            GameObject clonedObject = Instantiate(GameObject.Find("Table"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_Table";
            clonedObject.tag = "furniture_utility";
            clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.left, 90);
            
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Counter_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            GameObject clonedObject = Instantiate(GameObject.Find("Counter"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_Counter";
            clonedObject.tag = "furniture_utility";
            clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.left, 90);
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name == "Vase_button")
        {

            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc = pobj.GetPlacementLoc();
            spawnLoc[1] += 1f;
            GameObject clonedObject = Instantiate(GameObject.Find("Vase"), spawnLoc, Quaternion.identity);
            clonedObject.name = "new_furniture_obj" + (UnityEngine.Random.Range(0, 1001)).ToString() + "_Vase";
            clonedObject.tag = "furniture_utility";
            clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.left, 90);
            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>();
            pobj1.resetMenu();
            PlayScore();
        }
        else if (Input.GetButtonDown(settings.ClickInputA) && currentTarget.name.StartsWith("new_furniture_obj"))
        {
            //attach object to the raycast.
            bool_obj_being_manipulated = true;
            obj_being_manipulated = currentTarget;
            currentTarget.GetComponent<MeshCollider>().enabled = false;
            currentTarget.transform.rotation = Quaternion.identity;
            if(!currentTarget.name.EndsWith("TV") && !currentTarget.name.EndsWith("Chair"))
                currentTarget.transform.RotateAround(currentTarget.transform.position, Vector3.left, 90);
            var inst_obj = GameObject.Find("Instructions").GetComponent<InstructionsFunctions>();
            inst_obj.inst_obj1.SetActive(true);
            inst_obj.useInstructions = true;
        }

        else if (Input.GetButtonDown(settings.ClickInputA))
        {
            GameObject myObject = GameObject.Find("ContextMenu");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var tp_loc = loc.point;
            tp_loc[1] = 0.5f;
            if (pobj.getAction() == 1)
            {
                pobj.getObject().transform.position = tp_loc;
                pobj.getObject().SetActive(true);

            }
            else if (pobj.getAction() == 2)
            {
                GameObject clonedObject = Instantiate(pobj.getObject(), tp_loc, pobj.getObject().transform.rotation);
            }
            else if (pobj.getAction() == 3)
            {
                pobj.setObject(null);
                pobj.resetMenu();
            }
        }


    }

}