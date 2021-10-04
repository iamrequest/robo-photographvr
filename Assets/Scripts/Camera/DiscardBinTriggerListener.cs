using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Discard photos that get dropped in the pin, rather than placed
// This could be refactored. There's no real need for a socket anyways
public class DiscardBinTriggerListener : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Photo photo)) {
            if (!photo.hvrGrabbable.IsBeingHeld) {
                Destroy(photo.gameObject);
            }
        }

        Photo photoInParent = other.GetComponentInParent<Photo>();
        if (photoInParent) {
            if (!photoInParent.hvrGrabbable.IsBeingHeld) {
                Destroy(photoInParent.gameObject);
            }
        }
    }
}