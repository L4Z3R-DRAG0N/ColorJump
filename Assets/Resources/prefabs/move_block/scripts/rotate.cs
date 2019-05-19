using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool use_rotate;
    public float rotate_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!use_rotate)
        {
            return;
        }
        transform.Rotate(new Vector3(0, 0, rotate_speed));
    }
}
