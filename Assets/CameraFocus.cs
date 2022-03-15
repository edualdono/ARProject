using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraFocus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += StartVuforiaFocus;
    }

    public void StartVuforiaFocus()
    {
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        var targetFps = VuforiaBehaviour.Instance.CameraDevice.GetRecommendedFPS();

       if (UnityEngine.Application.targetFrameRate != targetFps)
       {
           // Debug.Log("Setting frame rate to " + targetFps + "fps");
            UnityEngine.Application.targetFrameRate = targetFps;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
