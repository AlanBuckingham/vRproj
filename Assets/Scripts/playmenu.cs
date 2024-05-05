using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class playmenu : MonoBehaviour
{
    bool inputAllowed = true;
    public GameObject resumeButton, lengthButton, currentButton;
    public float debounceTime = 0.5f;
    public string but_map = "js2";
    void Awake()
    {
        resumeButton = GameObject.Find("Play");
        lengthButton = GameObject.Find("Quit");
        SelectDefaultButton();
    }
    public void SelectDefaultButton()
    {
        EventSystem es = GetComponent<EventSystem>();
        if (resumeButton != null)
        {
            currentButton = resumeButton;
            es.SetSelectedGameObject(resumeButton);
        }
    }

    public void selectNextButton()
    {
        EventSystem es = GetComponent<EventSystem>();
        if (currentButton == resumeButton)
        {
            es.SetSelectedGameObject(lengthButton);
            currentButton = lengthButton;
        }
        else if (currentButton == lengthButton)
        {
            es.SetSelectedGameObject(resumeButton);
            currentButton = resumeButton;
        }
    }

    public void selectPreviousButton()
    {
        EventSystem es = GetComponent<EventSystem>();
        if (currentButton == resumeButton)
        {
            es.SetSelectedGameObject(lengthButton);
            currentButton = lengthButton;
        }
        else if (currentButton == lengthButton)
        {
            es.SetSelectedGameObject(resumeButton);
            currentButton = resumeButton;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inputAllowed && Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            if (Input.GetAxis("Vertical") > 0)
                selectNextButton();
            else
                selectPreviousButton();

            resumeButton.GetComponent<AudioSource>().Play();
            if (gameObject.activeSelf)
                StartCoroutine(DebounceInput());
        }

        if (Input.GetButtonDown(but_map))
        {
            Button myButton = currentButton.GetComponent<Button>();
            myButton.onClick.Invoke();
            if (gameObject.activeSelf)
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
