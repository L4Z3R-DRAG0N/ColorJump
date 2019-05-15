using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish_collide : MonoBehaviour
{
    [SerializeField] int this_scene_code;
    private float original_gravity;

    private void Start()
    {
        original_gravity = -Mathf.Abs(Physics.gravity.y);
    }

    void OnCollisionEnter(Collision player) {
        if (this_scene_code > PlayerPrefs.GetInt("level_progress"))
        {
            // record level pass status here
            PlayerPrefs.SetInt("level_progress", this_scene_code);
        }
        // reset all physics properties
        Time.timeScale = 1;
        Physics.gravity = new Vector3(0, original_gravity, 0);

        // TODU: show level finished scene here

        // go back to main menu
        SceneManager.LoadScene("start_menu");
    }
}
