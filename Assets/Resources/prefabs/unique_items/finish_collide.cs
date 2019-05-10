using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish_collide : MonoBehaviour
{
    [SerializeField] string next_scene;

    void OnCollisionEnter() {
        // TODU: record level pass status here
        PlayerPrefs.SetInt("level_progress", 1);
        // go back to main menu
        SceneManager.LoadScene("start_menu");
    }
}
