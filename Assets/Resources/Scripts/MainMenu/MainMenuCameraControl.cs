using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraControl : MonoBehaviour
{
    private float x;
    private float z;
    private float camera_moving_speed;
    private Vector3 currentVelocity;
    public bool is_menu_start;
    public Vector3 move_to;
    // Start is called before the first frame update
    void Start()
    {
        x = z = 0;
        camera_moving_speed = 0.5f;
        is_menu_start = false;
        currentVelocity = Vector3.zero;
        // initial camera pos
        move_to = new Vector3(34, 10, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get x, y input
        x = Input.GetAxis("Vertical");
        z = Input.GetAxis("Horizontal");
        // if camera unlocked
        if (is_menu_start) {
            move_to = move_to + new Vector3(x, 0, -z) * camera_moving_speed;
        }
        // smoothly move to target pos, ref currentVelocity is like a pointer that updates each time when the func is called
        transform.position = Vector3.SmoothDamp(transform.position, move_to, ref currentVelocity, 0.1f);
    }
}
