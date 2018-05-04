using UnityEngine;
using System.Collections;

// Make the healthbar of enemy always stay on top

public class HealtbarScript : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}