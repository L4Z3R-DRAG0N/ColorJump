using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class finish_collide : MonoBehaviour
{
    [SerializeField] int this_scene_code;
    private float original_gravity;

    private float end_time;

    private bool is_new_best_time;

    private void Start()
    {
        original_gravity = -Mathf.Abs(Physics.gravity.y);
        is_new_best_time = false;
    }

    void OnCollisionEnter(Collision collide) {
        if (collide.gameObject.name != "Player")
        {
            // do nothing if not player collide
            return;
        }
        end_time = (float)(Mathf.Round(Time.timeSinceLevelLoad * 1000)) / 1000;

        float best_time = PlayerPrefs.GetFloat("level" + this_scene_code + "best_time");

        if (best_time > end_time || best_time == 0)
        {
            PlayerPrefs.SetFloat("level" + this_scene_code + "best_time", end_time);
            is_new_best_time = true;
        }
        else
        {
            is_new_best_time = false;
        }

        if (is_new_best_time)
        {
            collide.gameObject.GetComponent<Controller>().getTimeDisplay().GetComponent<Text>().text = "new best : " + end_time.ToString() + "s";
        }
        else
        {
            collide.gameObject.GetComponent<Controller>().getTimeDisplay().GetComponent<Text>().text = "time used: " + end_time.ToString() + "s";
        }
        if (this_scene_code > PlayerPrefs.GetInt("level_progress"))
        {
            // record level pass status here
            PlayerPrefs.SetInt("level_progress", this_scene_code);
        }

        collide.gameObject.GetComponent<Controller>().display_pass_menu = true;
        // pause the game for passed ui interface
        Time.timeScale = 0;
        // no need to restore physics and time, these are handled by the passed ui interface control
    }
}
