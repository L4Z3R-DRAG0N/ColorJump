using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish_collide : MonoBehaviour
{
    [SerializeField] int this_scene_code;

    void OnCollisionEnter(Collision player) {
        // TODU: record level pass status here
        PlayerPrefs.SetInt("level_progress", this_scene_code);
        // reset all physics properties
        float original_gravity = -Mathf.Abs(Physics.gravity.y);
        Physics.gravity = new Vector3(0, original_gravity, 0);
        // go back to main menu
        SceneManager.LoadScene("start_menu");
    }
}
