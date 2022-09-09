using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class CameraZoom : MonoBehaviour
{

    public Camera cam;
    public float zoomMultiplier = 2;
    public float defaultFov = 70;
    public float zoomDuration = 2;
    public float subtractFOV = 20f;
    private bool fovChangeIsActive;
    public float zoomInSensitivity = 50f;
    public float zoomOutSensitivity = 150f;

    bool zoomedInToggle = false;

    public CameraController fpsCharacter;

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
        }
        
        else
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        ZoomCamera(defaultFov - subtractFOV);
        fpsCharacter.cameraSpeed = zoomInSensitivity;
        fovChangeIsActive = true;
    }

    private void ZoomOut()
    {
        ZoomCamera(defaultFov);
        fpsCharacter.cameraSpeed = zoomOutSensitivity;
        fovChangeIsActive = false;
    }

    void ZoomCamera(float target)
    {
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, target, zoomMultiplier * Time.deltaTime);
    }

#region - GUNSHOT FOV TEST -

    // public void GunshotFOVChange(float m_FieldOfView)
    // {

    //     cam.fieldOfView = m_FieldOfView;

    //     if(fovChangeIsActive)
    //     {
    //         cam.fieldOfView = m_FieldOfView + 2;
    //     }

    //     else
    //     {
    //         cam.fieldOfView = defaultFov + 2;
    //     }
    // }

    // public void DisableGunshotFOVChange()
    // {
    //     cam.fieldOfView = defaultFov;
    // }

#endregion

}
