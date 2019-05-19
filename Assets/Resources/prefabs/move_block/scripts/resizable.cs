using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizable : MonoBehaviour
{
    public bool use_resize;
    public float min_length = 5;
    public float max_length = 5;
    public float resize_speed;
    // public bool is_random;
    private bool is_enlarging;

    // Start is called before the first frame update
    void Start()
    {
        if (use_resize)
        {
            transform.localScale = new Vector3((min_length + max_length) / 2, 1, 1);
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
