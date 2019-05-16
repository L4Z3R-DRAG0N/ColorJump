using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_interact : MonoBehaviour
{
    private float original_gravity;
    // 001, 002, ... etc
    public string next_level_code;
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
        SceneManager.LoadScene("level" + next_level_code);
    }
}
