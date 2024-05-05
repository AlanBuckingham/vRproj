using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DataHolder.Char_sel == 0)
        {
            var alan = GameObject.Find("alannormalpose");
            //alan.transform.position = new Vector3(-2.77399993f, 2.5f, 5.94199991f);
            var target = GameObject.Find("Main Camera").transform;
            Vector3 directionToTarget = target.position - alan.transform.position;
            directionToTarget.y = 0;  // This removes any vertical difference in the look direction

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            alan.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
        else
        {
            var shawn = GameObject.Find("shawnhappy");
            //shawn.transform.position = new Vector3(-2.77399993f, 2.5f, 5.94199991f);
            var target = GameObject.Find("Main Camera").transform;
            Vector3 directionToTarget = target.position - shawn.transform.position;
            directionToTarget.y = 0;  // This removes any vertical difference in the look direction

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            shawn.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
    }
}
