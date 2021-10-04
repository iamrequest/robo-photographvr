using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using Valve.VR;

// See also: VRIKCalibrationController, HeightCalibrator
public class TitleScreenHeightCalibrator : MonoBehaviour {
    public static TitleScreenHeightCalibrator Instance { get; private set; }

    public VRIK vrik;
    public Transform leftHandAnchor, rightHandAnchor, hmdAnchor;
    public SteamVR_Action_Boolean calibrateHeightAction;
    public bool IsCalibrated { get; private set; }
    public UnityEvent onCalibrate;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError($"Multiple {GetType()} components detected. This is probably a bug.");
            Destroy(this);
        }
    }

    private void OnEnable() {
        calibrateHeightAction.AddOnStateDownListener(CalibrateHeight, SteamVR_Input_Sources.Any);
    }
    private void OnDisable() {
        calibrateHeightAction.RemoveOnStateDownListener(CalibrateHeight, SteamVR_Input_Sources.Any);
    }

    private void CalibrateHeight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        CalibrateNew();
    }

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

        onCalibrate.Invoke();
    }
}
