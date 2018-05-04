using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Zooms the camera in and out with the mousewheel

public class CameraZoom : Singleton<CameraZoom> {
    private CinemachineVirtualCamera _vcam;
    private bool _zoom = false;

    public bool Zoom
    {
        get { return _zoom; }
        set { _zoom = value; }
    }

    // Use this for initialization
    private void Start () {
        _vcam = GetComponent<CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
    private void Update () {

        if (_zoom)
        {
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
}
