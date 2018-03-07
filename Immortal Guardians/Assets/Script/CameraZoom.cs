using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour {
    private CinemachineVirtualCamera _vcam;

	// Use this for initialization
    private void Start () {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        
	}
	
	// Update is called once per frame
    private void Update () {
        var d = Input.GetAxis("Mouse ScrollWheel");

        // Scroll up
        if(d > 0f)
        {
            if(_vcam.m_Lens.OrthographicSize >= 5)
            {
                _vcam.m_Lens.OrthographicSize -= 0.5f;
            }
        }

        // Scroll down
        if (!(d < 0f)) return;
        if (_vcam.m_Lens.OrthographicSize <= 20)
        {
            _vcam.m_Lens.OrthographicSize += 0.5f;
        }
    }
}
