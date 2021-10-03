using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PhotoSaveMachine : MonoBehaviour {
    private Animator animator;
    private int animHashSavePicture;
    private void Awake() {
        animator = GetComponent<Animator>();
        animHashSavePicture = Animator.StringToHash("savePicture");
    }

    public void TrySavePhoto(HVRGrabberBase hvrGrabberBase, HVRGrabbable hvrGrabbable) {
        if (hvrGrabbable.TryGetComponent(out Photo photo)) {
            animator.SetTrigger(animHashSavePicture);
            photo.SaveToFile();
        }
    }
}
