using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// A photo camera that doesn't rely on HVR input to call TakePicture().
/// </summary>
public class PhotoCameraNonHVR : PhotoCamera {
    [Tooltip("If non-null, this action will be used to take photos")]
    public SteamVR_Action_Boolean takePhotoAction;

    private void OnEnable() {
        takePhotoAction.AddOnStateDownListener(TakePictureViaSteamVRInput, SteamVR_Input_Sources.Any);
    }
    private void OnDisable() {
        takePhotoAction.RemoveOnStateDownListener(TakePictureViaSteamVRInput, SteamVR_Input_Sources.Any);
    }

    private void TakePictureViaSteamVRInput(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        TakePicture();
    }
}
