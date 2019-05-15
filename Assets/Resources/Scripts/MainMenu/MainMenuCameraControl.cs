using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraControl : MonoBehaviour
{
    private float x;
    private float z;
    private float camera_moving_speed;

    private float camera_scroll_delta;

    private Vector3 currentVelocity;
    public bool is_menu_start;
    public Vector3 move_to;
    // Start is called before the first frame update
    void Start()
    {
        x = z = 0;
        camera_moving_speed = 0.5f;

        camera_scroll_delta = 0f;
        
        if (PlayerPrefs.GetInt("GameFirstEnter") == 1)
        {
            is_menu_start = true;
            // if GameFirstEnter == 1, there must be a level_progress initialized by StartGame
            // so no need for try catch
            int level_progress = PlayerPrefs.GetInt("level_progress");
            int level_z = 30 - level_progress * 10;
            transform.position = new Vector3(27, 15, level_z);
        }
        else
        {
            is_menu_start = false;
        }
        // initial camera pos
        move_to = transform.position;

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
        }
        // smoothly move to target pos, ref currentVelocity is like a pointer that updates each time when the func is called
        transform.position = Vector3.SmoothDamp(transform.position, move_to, ref currentVelocity, 0.1f);
    }
}
