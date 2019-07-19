using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraControl : MonoBehaviour
{
    private float x;
    private float z;
    private float camera_moving_speed;

    private float camera_scroll_delta;

    public Vector3 target_level_block_pos;

    private Vector3 currentVelocity;
    public bool is_menu_start;
    public Vector3 move_to;

    public GameObject[] level_block_list;

    // used to determine which block should be which color
    private int level_progress;
    // used to determine the position of camera when user exit the level
    // the camera should lock on the level that player just quit
    private int current_level;

    void set_camera_moveto_pos(int progress)
    {
        // interate through the level block list and try to find the one that matchs level_progress
        for (int i = 0; i < level_block_list.Length; i++)
        {
            Transform current_level_block_transform = level_block_list[i].transform;
            // if match
            if (int.Parse(current_level_block_transform.name.Substring(5, 3)) == progress)
            {
                target_level_block_pos = current_level_block_transform.position;

                // initialize camera target pos to target level block location
                move_to = new Vector3(target_level_block_pos.x - 6, 15, target_level_block_pos.z);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ensure that the game runs as expected even if the game crashes last time
        // initialize timescale
        Time.timeScale = 1;

        // get level_block list in order to fast travel camera
        level_block_list = GameObject.FindGameObjectsWithTag("level_block");

        x = z = 0;
        camera_moving_speed = 0.5f;

        camera_scroll_delta = 0f;

        // load player progress
        // init level_progress 0
        level_progress = 0;
        if (PlayerPrefs.HasKey("level_progress"))
        {
            // load if has key
            level_progress = PlayerPrefs.GetInt("level_progress");
        }
        else
        {
            // if first start, no record, set progress as init value
            PlayerPrefs.SetInt("level_progress", level_progress);
        }

        current_level = 0;
        if (PlayerPrefs.HasKey("current_level"))
        {
            // load if has key
            current_level = PlayerPrefs.GetInt("current_level");
        }
        else
        {
            // if first start, no record, set progress as init value
            PlayerPrefs.SetInt("current_level", level_progress);
        }

        if (PlayerPrefs.GetInt("GameFirstEnter") == 1)
        {
            // camera movement unlock
            is_menu_start = true;
            // when quit a level, the camera should lock on the current level that player is just playing
            set_camera_moveto_pos(current_level);
            // if quit from level, directly put the camera to the target level block
            transform.position = move_to;
        }
        else
        {
            // camera movement lock
            is_menu_start = false;
            // when enter the game, the camera should lock on the latest progress that the player made
            set_camera_moveto_pos(level_progress);
            // if just entered the game, force the player to watch the start animation
        }
        
        currentVelocity = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        // if camera unlocked
        if (is_menu_start)
        {
            // get x, y input
            x = Input.GetAxis("Vertical");
            z = Input.GetAxis("Horizontal");
            // scroll up: enlarge
            camera_scroll_delta = Input.GetAxis("Mouse ScrollWheel") * -30;
            Vector3 move_to_delta = new Vector3(x - camera_scroll_delta / 2, camera_scroll_delta, -z) * camera_moving_speed;
            if (move_to_delta.y + move_to.y > 10 && move_to_delta.y + move_to.y < 30)
            {
                move_to += move_to_delta;
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                move_to = new Vector3(-6.5f, 15f, 0f);
            }

            // smoothly move to target pos, ref currentVelocity is like a pointer that updates each time when the func is called
            transform.position = Vector3.SmoothDamp(transform.position, move_to, ref currentVelocity, 0.1f);
        }
    }
}
