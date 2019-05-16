using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizable : MonoBehaviour
{
    public float random_min_length;
    public float random_max_length;
    public float resize_speed;
    public bool is_random;

    private bool is_enlarging;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (random_min_length > transform.localScale.x)
        {
            is_enlarging = true;
        }
        else if (random_max_length < transform.localScale.x)
        {
            is_enlarging = false;
        }

        if (is_enlarging)
        {
            transform.localScale += new Vector3(resize_speed, 0, 0);
        }
        else
        {
            transform.localScale -= new Vector3(resize_speed, 0, 0);
        }
    }
}
