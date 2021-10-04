using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoDiscardBin : MonoBehaviour {
    public void TryDiscardPhoto(HVRGrabberBase hvrGrabberBase, HVRGrabbable hvrGrabbable) {
        if (hvrGrabbable.TryGetComponent(out Photo photo)) {
            //hvrGrabbable.ForceRelease();
            Destroy(photo.gameObject);
        }
    }

    public void OnPhotoEnteredEntered(Collider other) {
        if (other.TryGetComponent(out Photo photo)) {
            if (!photo.hvrGrabbable.IsBeingHeld) {
                Destroy(photo.gameObject);
            }
        }
    }
}
