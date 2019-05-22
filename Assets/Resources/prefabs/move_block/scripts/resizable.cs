using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizable : MonoBehaviour
{
    public bool use_resize;
    public float min_length = 5;
    public float max_length = 5;
    public float resize_speed;
    public bool is_infinite_resize;
    public int total_resize_times;
    // public bool is_random;
    private bool is_enlarging;

    private Vector3 initial_scale;

    // Start is called before the first frame update
    void Start()
    {
        if (use_resize)
        {
            transform.localScale = new Vector3((min_length + max_length) / 2, transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!use_resize)
        {
            return;
        }
        if (transform.localScale.x < min_length)
        {
            is_enlarging = true;
        }
        else if (transform.localScale.x > max_length)
        {
            is_enlarging = false;
            // if resize mode is finite
            if (!is_infinite_resize)
            {
                // decrease total resize time by 1
                total_resize_times -= 1;
                if (total_resize_times <= 0){
                    // disable resize
                    use_resize = false;
                }
            }
        }

        if (is_enlarging)
        {
            transform.localScale += new Vector3(resize_speed / 100, 0, 0);
        }
        else
        {
            transform.localScale -= new Vector3(resize_speed / 100, 0, 0);
        }

    }
}
