using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PhotoSaveMachine : MonoBehaviour {
    private Animator animator;
    private int animHashSavePicture;
    public AudioClip saveSFX;

    private void Awake() {
        animator = GetComponent<Animator>();
        animHashSavePicture = Animator.StringToHash("savePicture");
    }

    public void TrySavePhoto(HVRGrabberBase hvrGrabberBase, HVRGrabbable hvrGrabbable) {
        if (hvrGrabbable.TryGetComponent(out Photo photo)) {
            animator.SetTrigger(animHashSavePicture);
            photo.SaveToFile();
            SFXPlayer.Instance.PlaySFX(saveSFX, transform.position);
        }
    }
}
