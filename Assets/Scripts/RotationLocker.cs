using UnityEngine;
using System.Collections;

public class RotationLocker : MonoBehaviour
{
    private Rigidbody rb;
    private Quaternion originalRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("RotationLocker script requires a Rigidbody component.");
            return;
        }

        // Start the coroutine to lock rotation
        
    }

    public void lock_rotation()
    {
        GameObject.Find("furnituresoundobj").GetComponent<AudioSource>().Play();
        StartCoroutine(LockRotationForSeconds(3f)); // Locks rotation for 3 seconds
    }

    IEnumerator LockRotationForSeconds(float seconds)
    {
        originalRotation = transform.rotation; // Store the current rotation

        // Lock the rotation by setting constraints
        rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        yield return new WaitForSeconds(seconds); // Wait for the specified time

        // Unlock the rotation
        rb.constraints &= ~(RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ);

        // Optional: Reset to the original rotation after unlocking
        transform.rotation = originalRotation;
    }
}
