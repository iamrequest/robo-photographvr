using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {
    public string sceneName;
    public Animator titleScreenAnimator, fadeCamAnimator;
    public TitleScreenHeightCalibrator titleScreenHeightCalibrator;

    public float fadeDuration;
    [Tooltip("Load the next scene after x seconds, following a proper calibration")]
    public float loadSceneDelay;

    private void OnEnable() {
        titleScreenHeightCalibrator.onCalibrate.AddListener(OnCalibrate);
    }

    private void OnDisable() {
        titleScreenHeightCalibrator.onCalibrate.AddListener(OnCalibrate);
    }

    [Button]
    public void LoadGameScene() {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadGameSceneAfterDelay() {
        yield return new WaitForSeconds(loadSceneDelay);
        fadeCamAnimator.SetTrigger("fadeOut");
        titleScreenHeightCalibrator.enabled = false;
        yield return new WaitForSeconds(fadeDuration);
        LoadGameScene();
    }

    private void OnCalibrate() {
        titleScreenAnimator.SetTrigger("isCalibrationComplete");
        StartCoroutine(LoadGameSceneAfterDelay());
    }
}
