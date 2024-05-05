using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class get_score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 
    }

    // Update is called once per frame
    void Update()
    {
        var textMesh = GameObject.Find("Score").GetComponent<TextMeshPro>();
        textMesh.text = (DataHolder.Char_sel).ToString(); ;
    }
}
