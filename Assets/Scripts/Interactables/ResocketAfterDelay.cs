using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HVRGrabbable))]
public class ResocketAfterDelay : MonoBehaviour {
    public float returnToSocketDelay;

    private HVRGrabbable hvrGrabbable;
    public HVRSocket homeSocket;

    private Coroutine returnToSocketCoroutine;

    private void Awake() {
        hvrGrabbable = GetComponent<HVRGrabbable>();
    }

    private void OnEnable() {
        hvrGrabbable.Grabbed.AddListener(OnGrabbed);
        hvrGrabbable.Released.AddListener(ReturnToSocketAfterDelay);
    }

    private void OnDisable() {
        hvrGrabbable.Released.RemoveListener(ReturnToSocketAfterDelay);
        hvrGrabbable.Grabbed.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1) {
        if (returnToSocketCoroutine != null) {
            StopCoroutine(returnToSocketCoroutine);
            returnToSocketCoroutine = null;
        }
    }

    private void ReturnToSocketAfterDelay(HVRGrabberBase arg0, HVRGrabbable arg1) {
        returnToSocketCoroutine = StartCoroutine(DoReturnToCameraRollAfterDelay());
    }


    private IEnumerator DoReturnToCameraRollAfterDelay() {
        yield return new WaitForSeconds(returnToSocketDelay);

        if (!hvrGrabbable.IsBeingHeld) {
            homeSocket.TryGrab(hvrGrabbable, true);
        }
        returnToSocketCoroutine = null;
    }
}
