using HexabodyVR.PlayerController;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PhotoModeManager : MonoBehaviour {
    public PhotoModeRig photoModeRig;
    public HexaBodyPlayer3 hexabodyRig;
    public VRIK vrik;

    private Camera hexabodyCamera;

    public SteamVR_Action_Boolean togglePhotoModeAction;
    public bool IsPhotoModeActive { get; private set; }
    private Coroutine photoModeRestoreTime;
    [Range(0.001f, 2f)]
    [Tooltip("How long it takes for time to restore to full speed after photo mode is exited")]
    public float timeRestoreDuration;


    private void Awake() {
        hexabodyCamera = hexabodyRig.Camera.GetComponent<Camera>();
    }
    private void Start() {
        photoModeRig.gameObject.SetActive(false);
    }

    private void OnEnable() {
        togglePhotoModeAction.AddOnStateDownListener(TogglePhotoMode, SteamVR_Input_Sources.Any);
    }
    private void OnDisable() {
        togglePhotoModeAction.RemoveOnStateDownListener(TogglePhotoMode, SteamVR_Input_Sources.Any);
    }

    private void TogglePhotoMode(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        // Optional: Do not allow the player to spam photo mode
        //  This prevents the player from starting photo mode while we're lerping time back to normal
        // if (photoModeRestoreTime != null) return;

        if (IsPhotoModeActive) {
            StopPhotoMode();
        } else {
            StartPhotoMode();
        }
    }

    [Button]
    public void StartPhotoMode() {
        IsPhotoModeActive = true;

        Time.timeScale = 0f;
        if (photoModeRestoreTime != null) {
            StopCoroutine(photoModeRestoreTime);
            photoModeRestoreTime = null;
        }

        // Disable hexabody cam, start photo mode rig
        hexabodyCamera.enabled = false;
        photoModeRig.gameObject.SetActive(true);

        // Prevent finalIK from moving around while time is stopped
        vrik.enabled = false;

        // Align the photo mode camera with the hexabody rig's camera
        photoModeRig.transform.forward = hexabodyRig.PelvisCapsule.forward;

        Vector3 hmdOffset = photoModeRig.cam.transform.position - photoModeRig.transform.position;
        photoModeRig.transform.position = hexabodyRig.Camera.transform.position - hmdOffset;
    }

    [Button]
    public void StopPhotoMode() {
        IsPhotoModeActive = false;

        // Switch back to the hexabody rig
        photoModeRig.gameObject.SetActive(false);
        hexabodyCamera.enabled = true;
        vrik.enabled = true;

        // Lerp time back to normal over a short duration
        if (photoModeRestoreTime != null) StopCoroutine(photoModeRestoreTime);
        photoModeRestoreTime = StartCoroutine(RestoreTimeScale());
    }

    private IEnumerator RestoreTimeScale() {
        float elapsedTime = 0f;

        while (elapsedTime < timeRestoreDuration) {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(0, 1, elapsedTime / timeRestoreDuration);
            yield return null;
        }

        Time.timeScale = 1f; // Lerp() is clamped, but just being safe here
        photoModeRestoreTime = null;
    }

    private void OnDrawGizmosSelected() {
        if (Application.isPlaying) {
            Gizmos.DrawLine(photoModeRig.cam.transform.position, hexabodyRig.Camera.position);
        }
    }
}
