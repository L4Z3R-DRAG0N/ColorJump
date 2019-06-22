using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool use_rotate;
    public Vector3 rotate_around;
    public Vector3 rotate_axis;
    private Vector3 rotate_origin;
    public float rotate_speed;
    public float rotate_speed_around_center;
    // Start is called before the first frame update
    void Start()
    {
        // rotate around use world coordinate, so add self position to get local position
        rotate_origin = transform.position + rotate_around;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!use_rotate)
        {
            return;
        }
        // rotate around rotate origin
        transform.RotateAround(rotate_origin, rotate_axis, rotate_speed);
        // rotate around center of self
        transform.Rotate(new Vector3(0, 0, rotate_speed_around_center));
    }
}
