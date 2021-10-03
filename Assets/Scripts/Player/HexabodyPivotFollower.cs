using HexabodyVR.PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transform that follows the hexasphere locosphere, positioning itself directly under the locosphere.
/// This gameobject will function as the parent transform for the finalIK avatar
/// </summary>
public class HexabodyPivotFollower : MonoBehaviour {
    public HexaBodyPlayer3 hexabodyRig;

    private Vector3 locosphereOffset;

    private void Start() {
        locosphereOffset = new Vector3(0f, hexabodyRig.LocoCollider.radius, 0f);
    }
    void FixedUpdate() {
        transform.position = hexabodyRig.LocoSphere.position;
        transform.position -= locosphereOffset;
    }
}
