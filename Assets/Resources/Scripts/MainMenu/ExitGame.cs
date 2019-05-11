using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit_Game() {
        #if UNITY_EDITOR
            PlayerPrefs.SetInt("level_progress", 0);
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // PlayerPrefs.SetInt("level_progress", 0);
            Application.Quit();
        #endif
    }
}
