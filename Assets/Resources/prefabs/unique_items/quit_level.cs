using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class quit_level : MonoBehaviour
{

    public void ExitLevelScene()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // quit to main menu without recording progress
        // SceneManager.LoadScene("start_menu");
        SceneManager.LoadScene("start_menu");
        Time.timeScale = 1;
    }
}