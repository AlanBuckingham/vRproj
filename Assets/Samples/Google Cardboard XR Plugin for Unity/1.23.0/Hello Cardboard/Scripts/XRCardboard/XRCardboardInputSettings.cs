using UnityEngine;

[CreateAssetMenu(fileName = "XRCardboardInputSettings", menuName = "Google Cardboard/Cardboard Input Settings")]
public class XRCardboardInputSettings : ScriptableObject
{
    public string ClickInputX => clickInputNameX;
    public string ClickInputY => clickInputNameY;
    public string ClickInputB => clickInputNameB;
    public string ClickInputA => clickInputNameA;
    public bool ClickOnHover => clickOnHover;
    public float GazeTime => gazeTimeInSeconds;


    //[SerializeField]
    //string clickInputName0 = "js2";//X
    //[SerializeField]
    //string clickInputName1 = "js3";//Y
    //[SerializeField]
    //string clickInputName2 = "js5";//B
    //[SerializeField]
    //string clickInputName3 = "js10";//A
    [SerializeField]
    string clickInputNameX = "js2";//X
    [SerializeField]
    string clickInputNameY = "js3";//Y
    [SerializeField]
    string clickInputNameB = "js5";//B
    [SerializeField]
    string clickInputNameA= "js10";//A
    [SerializeField]
    bool clickOnHover = false;
    [SerializeField, Range(.5f, 5)]
    float gazeTimeInSeconds = 2f;
}