using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using HexabodyVR.PlayerController;
using Sirenix.OdinInspector;
using Valve.VR;

// See also: VRIKCalibrationController
public class HeightCalibrator : MonoBehaviour {
    public PhotoModeManager photoModeManager;
    [Tooltip("Deactivated on Awake, re-activated on first calibration")]
    public List<Renderer> bodyRenderers;

    public VRIK vrik;
    public HexaBodyPlayer3 hexabody;
    public float scaleMultiplier;
    public Transform leftHandAnchor, rightHandAnchor, hmdAnchor;
    public SteamVR_Action_Boolean calibrateHeightAction;
    public bool IsCalibrated { get; private set; }

    private void Awake() {
        foreach (Renderer renderer in bodyRenderers) {
            renderer.enabled = false;
        }
    }

    private void Start() {
        // If we just came from the title screen (which probably already set calibration data), then calibrate the player using that data
        if (TitleScreenHeightCalibrator.Instance != null) {
            if (TitleScreenHeightCalibrator.Instance.calibrationData != null) {
                StartCoroutine(IntialCalibration());
            }
        }
    }

    private IEnumerator IntialCalibration() {
        yield return new WaitForEndOfFrame();
        CalibrateUsingExistingData(TitleScreenHeightCalibrator.Instance.calibrationData);
    }


    private void OnEnable() {
        calibrateHeightAction.AddOnStateDownListener(CalibrateHeight, SteamVR_Input_Sources.Any);
    }
    private void OnDisable() {
        calibrateHeightAction.RemoveOnStateDownListener(CalibrateHeight, SteamVR_Input_Sources.Any);
    }

    private void CalibrateHeight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        // Do not allow the player to calibrate while in photo mode
        //  This results in a weird tpose (which is valid calibration, but it looks stupid)
        if (photoModeManager.IsPhotoModeActive) return;

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

        hexabody.Calibrate();
        OnCalibrate();
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

        hexabody.Calibrate();
        OnCalibrate();
    }

    public void CalibrateUsingExistingData(VRIKCalibrator.CalibrationData data) {
        VRIKCalibrator.Calibrate(vrik, 
            data, 
            hmdAnchor, 
            null, 
            leftHandAnchor,
            rightHandAnchor);

        hexabody.Calibrate();
        OnCalibrate();
    }

    [Button]
    [HideInEditorMode]
    public void CalibrateScale() {
        if (calibrationData != null) {
            VRIKCalibrator.RecalibrateScale(vrik, calibrationData, scaleMultiplier);
        }
    }

    private void OnCalibrate() {
        if (!IsCalibrated) {
            IsCalibrated = true;

            foreach (Renderer renderer in bodyRenderers) {
                renderer.enabled = true;
            }
        }
    }
}
