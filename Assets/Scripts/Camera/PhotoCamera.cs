﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour {
    public CameraRoll cameraRoll;
    public Camera cam;

    [Button]
    public void TakePicture() {
        cam.Render();

        RenderTexture targetTexture = new RenderTexture(cam.targetTexture);
        Graphics.CopyTexture(cam.targetTexture, targetTexture);
        cameraRoll.AddPhoto(targetTexture);
    }
}
