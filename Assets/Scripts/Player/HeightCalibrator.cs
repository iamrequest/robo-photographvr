using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;

// See also: VRIKCalibrationController
public class HeightCalibrator : MonoBehaviour {
    public VRIK vrik;
    public float scaleMultiplier;
    public Transform leftHandAnchor, rightHandAnchor, hmdAnchor;

    public VRIKCalibrator.Settings settings;
    public VRIKCalibrator.CalibrationData calibrationData;

    [Button]
    [HideInEditorMode]
    public void CalibrateNew() {
        calibrationData = VRIKCalibrator.Calibrate(vrik, 
            settings,
            hmdAnchor,
            null,
            leftHandAnchor,
            rightHandAnchor);
    }

    [Button]
    [HideInEditorMode]
    public void CalibrateUsingExistingData() {
        VRIKCalibrator.Calibrate(vrik, 
            calibrationData, 
            hmdAnchor, 
            null, 
            leftHandAnchor,
            rightHandAnchor);
    }

    [Button]
    [HideInEditorMode]
    public void CalibrateScale() {
        if (calibrationData != null) {
            VRIKCalibrator.RecalibrateScale(vrik, calibrationData, scaleMultiplier);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            CalibrateNew();
        }
    }
}
