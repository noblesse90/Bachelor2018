using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour {

    CinemachineVirtualCamera vcam;

	// Use this for initialization
	void Start () {
        vcam = GetComponent<CinemachineVirtualCamera>();
        
	}
	
	// Update is called once per frame
	void Update () {
        var d = Input.GetAxis("Mouse ScrollWheel");

        // Scroll up
        if(d > 0f)
        {
            if(vcam.m_Lens.OrthographicSize >= 5)
            {
                vcam.m_Lens.OrthographicSize -= 0.5f;
            }
        }

        // Scroll down
        if(d < 0f)
        {
            if (vcam.m_Lens.OrthographicSize <= 20)
            {
                vcam.m_Lens.OrthographicSize += 0.5f;
            }
        }


	}
}
