using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class quit_level : MonoBehaviour
{
    private float original_gravity;

    private void Start()
    {
        original_gravity = -Mathf.Abs(Physics.gravity.y);
    }

    public void ExitLevelScene()
    {
        // quit to main menu without recording progress
        Time.timeScale = 1;
        Physics.gravity = new Vector3(0, original_gravity, 0);
        SceneManager.LoadScene("start_menu");
    }
}