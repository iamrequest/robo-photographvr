using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// See also: https://github.com/iamrequest/shattered-skies/blob/main/Assets/Scripts/Cameras/VRPhotoModeManager.cs
public class PhotoModeRig : MonoBehaviour {
    public PhotoModeManager photoModeManager;
    public Camera cam;

    private bool isSprinting;

    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Action_Vector2 turnAction;
    public SteamVR_Action_Boolean sprintAction;

    [Range(0f, 25f)]
    public float defaultSpeed, sprintSpeed;

    private void OnEnable() {
        moveAction.AddOnAxisListener(DoMove, SteamVR_Input_Sources.Any);
        sprintAction.AddOnChangeListener(SetIsSprinting, SteamVR_Input_Sources.Any);
        turnAction.AddOnAxisListener(DoTurn, SteamVR_Input_Sources.Any);
    }
    private void OnDisable() {
        moveAction.RemoveOnAxisListener(DoMove, SteamVR_Input_Sources.Any);
        sprintAction.RemoveOnChangeListener(SetIsSprinting, SteamVR_Input_Sources.Any);
        turnAction.RemoveOnAxisListener(DoTurn, SteamVR_Input_Sources.Any);
    }

    private void SetIsSprinting(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) {
        isSprinting = newState;
    }

    private void DoMove(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) {
        // Calculate motion
        float moveSpeed = isSprinting ? sprintSpeed : defaultSpeed;
        Vector3 motion = axis.normalized * moveSpeed * Time.unscaledDeltaTime;

        // Optional: Convert this from HMD-oriented motion to hand-based. Just swap out cam.transform for a tracked pose driver
        transform.position += cam.transform.forward * motion.y;
        transform.position += cam.transform.right * motion.x;
    }

    private void DoTurn(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) {
        // Match the rotation speed of the hexabody rig, to avoid a weird mismatch
        transform.RotateAround(cam.transform.position,
            Vector3.up,
            photoModeManager.hexabodyRig.SmoothTurnSpeed * axis.x * Time.unscaledDeltaTime);
    }

}
