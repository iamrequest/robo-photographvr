using HexabodyVR.PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenHexabodyPositioner : MonoBehaviour {
    public HexaBodyPlayer3 hexabodyRig;

    private void FixedUpdate() {
        hexabodyRig.MoveToPosition(transform.position);
    }
}
