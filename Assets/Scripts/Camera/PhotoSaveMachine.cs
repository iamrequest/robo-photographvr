using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSaveMachine : MonoBehaviour {
    public void TrySavePhoto(HVRGrabberBase hvrGrabberBase, HVRGrabbable hvrGrabbable) {
        if (hvrGrabbable.TryGetComponent(out Photo photo)) {
            // TODO: Play animation, display "saved!" message
            photo.SaveToFile();
        }
    }
}
