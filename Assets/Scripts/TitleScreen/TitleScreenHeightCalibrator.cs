using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using Valve.VR;
using HexabodyVR.PlayerController;

// See also: VRIKCalibrationController, HeightCalibrator
public class TitleScreenHeightCalibrator : MonoBehaviour {
    public static TitleScreenHeightCalibrator Instance { get; private set; }

    public VRIK vrik;
    public HexaBodyPlayer3 hexabody;
    public Transform leftHandAnchor, rightHandAnchor, hmdAnchor;
    public SteamVR_Action_Boolean calibrateHeightAction;
    public bool IsCalibrated { get; private set; }
    public UnityEvent onCalibrate;

    [Tooltip("Ignore calibration requests until the titlescreen says you can calibrate")]
    public float startupCalibrationDelay;

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
        if (Time.time > startupCalibrationDelay) {
            CalibrateNew();
        }
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

        hexabody.Calibrate();
        onCalibrate.Invoke();
    }
}
