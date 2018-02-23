using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}