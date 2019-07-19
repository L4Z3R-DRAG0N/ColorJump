using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_interact : MonoBehaviour
{
    private float original_gravity;
    public GameObject setting_menu;
    // Start is called before the first frame update
    void Start()
    {
        original_gravity = -Mathf.Abs(Physics.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initialize()
    {
        Physics.gravity = new Vector3(0, -10, 0);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    public void restart_level()
    {
        initialize();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit_level_without_save()
    {
        // quit to main menu without recording progress
        Time.timeScale = 1;
        Physics.gravity = new Vector3(0, original_gravity, 0);
        SceneManager.LoadScene("start_menu");
    }

    public void next_level()
    {
        Time.timeScale = 1;
        Physics.gravity = new Vector3(0, original_gravity, 0);
        
        // 001, 002, ... etc
        string next_level_code = "" + (PlayerPrefs.GetInt("current_level") + 1);
        // fill the missing 0s
        for (int i = 0; i < 3 - next_level_code.Length + 1; i++)
        {
            next_level_code = "0" + next_level_code;
        }
        
        
        SceneManager.LoadScene("level" + next_level_code);
    }


    
    public void open_settings()
    {
        GameObject.Find("Player").GetComponent<Controller>().display_setting_menu = true;
    }

    public void apply_settings()
    {

    }

    public void discard_settings()
    {
        GameObject.Find("Player").GetComponent<Controller>().display_setting_menu = false;
    }
}
