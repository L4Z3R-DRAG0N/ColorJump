using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform player;
    Vector3 distance;
    public float timeToTarget;
    Vector3 ve;

    private Vector3 deltaPos = Vector3.zero;

    private Vector3 shake_offset = Vector3.zero;
    private int shake_duration = 0;
    private float shake_magnitude = 1;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shake_duration > 0) {
            shake_offset = new Vector3(0, shake_magnitude * Random.Range(-shake_duration / 10, shake_duration / 10), 0);
            shake_duration -= 1;
        }else{
            shake_offset = Vector3.zero;
        }

        transform.position = Vector3.SmoothDamp(transform.position, player.position + distance + shake_offset, ref ve, timeToTarget);
    }

    public void Shake_start(int duration)
    {
        shake_duration = duration;
    }

}
