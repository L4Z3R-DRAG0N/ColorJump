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

    private void Start()
    {
        original_gravity = -Mathf.Abs(Physics.gravity.y);
    }

    void OnCollisionEnter(Collision collide) {
        if (collide.gameObject.name != "Player")
        {
            // do nothing if not player collide
            return;
        }

        end_time = Time.deltaTime;
        collide.gameObject.GetComponent<Controller>().getTimeDisplay().GetComponent<Text>().text = (end_time - collide.gameObject.GetComponent<Controller>().start_time).ToString();
        if (this_scene_code > PlayerPrefs.GetInt("level_progress"))
        {
            // record level pass status here
            PlayerPrefs.SetInt("level_progress", this_scene_code);
        }

        collide.gameObject.GetComponent<Controller>().display_pass_menu = true;
        Time.timeScale = 0;
        // reset all physics properties
        //Time.timeScale = 1;
        //Physics.gravity = new Vector3(0, original_gravity, 0);

        // TODU: show level finished scene here

        // go back to main menu
        //SceneManager.LoadScene("start_menu");
    }
}
