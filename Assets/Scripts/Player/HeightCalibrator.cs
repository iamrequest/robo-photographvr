using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;

// See also: VRIKCalibrationController
public class HeightCalibrator : MonoBehaviour {
    public VRIK vrik;
    public VRIKCalibrator.Settings settings;
    //public VRIKCalibrator.CalibrationData calibrationData;

    [Button]
    public void Calibrate() {
        VRIKCalibrator.Calibrate(vrik, settings,
            vrik.solver.spine.headTarget,
            null,
            vrik.solver.leftArm.target,
            vrik.solver.rightArm.target);
    }

    public void CalibrateScale() {
    }
}
