using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour {
    [HideInInspector]
    public HVRGrabbable hvrGrabbable;
    public RawImage image;

    [Range(0f, 2f)]
    public float returnToCameraRollDelay;

    public CameraRoll sourceCameraRoll;
    public RenderTexture texture;
    public DateTime timestamp;

    private Coroutine returnToCameraRollCoroutine;


    private void Awake() {
        hvrGrabbable = GetComponent<HVRGrabbable>();
    }
    private void OnEnable() {
        hvrGrabbable.Grabbed.AddListener(OnGrabbed);
        hvrGrabbable.Released.AddListener(ReturnToCameraRollAfterDelay);
    }
    private void OnDisable() {
        hvrGrabbable.Released.RemoveListener(ReturnToCameraRollAfterDelay);
        hvrGrabbable.Grabbed.RemoveListener(OnGrabbed);
    }



    private void OnGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1) {
        // If we're in the middle of returning a photo to the camera roll, stop now.
        if (returnToCameraRollCoroutine != null) {
            StopCoroutine(returnToCameraRollCoroutine);
            returnToCameraRollCoroutine = null;
        }

        // If we're hand grabbing the photo, remove it from the camera roll
        if (hvrGrabbable.IsHandGrabbed) {
            sourceCameraRoll.photos.Remove(this);
        }
    }
    private void ReturnToCameraRollAfterDelay(HVRGrabberBase arg0, HVRGrabbable arg1) {
        returnToCameraRollCoroutine = StartCoroutine(DoReturnToCameraRollAfterDelay());
    }

    private IEnumerator DoReturnToCameraRollAfterDelay() {
        yield return new WaitForSeconds(returnToCameraRollDelay);
        if (!hvrGrabbable.IsBeingHeld) sourceCameraRoll.AddPhoto(this); // TODO: Consider not re-rendering the page
        returnToCameraRollCoroutine = null;
    }

    [Button]
    // https://gist.github.com/krzys-h/76c518be0516fb1e94c7efbdcd028830
    public void SaveToFile() {
        // Prepare the render texture for saving
        RenderTexture.active = texture;
        Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        RenderTexture.active = null;

        // Prepare byte array for writing
        byte[] bytes;
        bytes = tex.EncodeToPNG();

        // Write to file (creating dir if it doesn't already exist)
        string dir = "Photos/";
        string filename = $"{timestamp.Year}-{timestamp.Month}-{timestamp.Day}_{timestamp.Hour}-{timestamp.Minute}-{timestamp.Second}-{timestamp.Millisecond}.png";

        if (!System.IO.Directory.Exists(dir)) {
            Debug.Log($"Photo dir ({dir}) doesn't exist, creating it now.");
            System.IO.Directory.CreateDirectory(dir);

            // TODO: Probably should double check that it worked, and throw an error to the player in-game or something.
            // I don't have that kind of functionality right now, so future works it is
        } 
        System.IO.File.WriteAllBytes(dir + filename, bytes);
    }
}
