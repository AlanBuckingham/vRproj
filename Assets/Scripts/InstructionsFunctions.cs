using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InstructionsFunctions : MonoBehaviour
{
    public float debounceTime = 0.5f;
    public bool useInstructions = false, inputAllowed = true;
    public GameObject inst_obj1 = null;
    public GameObject inst_obj2 = null;
    public string deleteButton = "js3";
    public int press_count = 0;

    // Start is called before the first frame update
    void Awake()
    {
        inst_obj1 = GameObject.Find("Instruction_txt");
        inst_obj1.SetActive(false);
        inst_obj2 = GameObject.Find("delete_instructions_txt");
        inst_obj2.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {

        if (useInstructions && inputAllowed && Input.GetButtonDown(deleteButton))
        {
            press_count += 1;  
            StartCoroutine(DebounceInput());
        }
        
    }

    IEnumerator DebounceInput()
    {
        inputAllowed = false;
        yield return new WaitForSeconds(debounceTime);
        inputAllowed = true;
    }


}
