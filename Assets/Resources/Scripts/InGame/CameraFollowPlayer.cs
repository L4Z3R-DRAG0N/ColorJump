using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 delta_position;
    public float positionLerpTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        delta_position = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = player.transform.position + delta_position;
    }
}
