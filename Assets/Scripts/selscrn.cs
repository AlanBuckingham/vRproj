using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class selscrn : MonoBehaviour
{
    public float debounceTime = 1.0f;
    bool inputAllowed = false;
    public string next_button = "js2";//X
    // Start is called before the first frame update
    void Start()
    {
        debounceTime = 0.5f;
        inputAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAllowed && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                GameObject.Find("Previous").GetComponent<Button>().onClick.Invoke();
                GameObject.Find("Previous").GetComponent<AudioSource>().Play();
            }
            else
            {
                GameObject.Find("Next").GetComponent<Button>().onClick.Invoke();
                GameObject.Find("Next").GetComponent<AudioSource>().Play();
            }
            StartCoroutine(DebounceInput());
        }

        if (inputAllowed && Input.GetButtonDown(next_button)) 
        {
            GameObject.Find("Start").GetComponent<Button>().onClick.Invoke();
            GameObject.Find("Start").GetComponent<AudioSource>().Play();
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
