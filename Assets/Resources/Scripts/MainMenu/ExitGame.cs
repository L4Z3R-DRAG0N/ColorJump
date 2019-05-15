using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit_Game() {
        // reset the GameFirstEnter value to 0 so watch the start anim on next enter
        PlayerPrefs.SetInt("GameFirstEnter", 0);
        #if UNITY_EDITOR
            PlayerPrefs.SetInt("level_progress", 0);
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            //PlayerPrefs.SetInt("level_progress", 0);
            Application.Quit();
        #endif
    }
}
