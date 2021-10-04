using HurricaneVR.Framework.Core.Utils;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PhotoCamera : MonoBehaviour {
    public CameraRoll cameraRoll;
    public Camera cam;
    public AudioClip pictureTakenSFX;
    [Range(0f, 1f)]
    public float pitchRange;

    [Button]
    public void TakePicture() {
        cam.Render();

        RenderTexture targetTexture = new RenderTexture(cam.targetTexture);
        Graphics.CopyTexture(cam.targetTexture, targetTexture);
        cameraRoll.AddPhoto(targetTexture);

        SFXPlayer.Instance.PlaySFXRandomPitch(pictureTakenSFX, transform.position, 1 - pitchRange, 1 + pitchRange);
    }
}
