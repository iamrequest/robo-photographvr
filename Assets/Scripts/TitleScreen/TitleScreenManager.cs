using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {
    public string sceneName;

    [Button]
    public void LoadGameScene() {
        SceneManager.LoadScene(sceneName);
    }
}
