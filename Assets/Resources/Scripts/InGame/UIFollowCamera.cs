using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    [SerializeField] GameObject target_camera;
    Vector3 delta_position;
    // Start is called before the first frame update
    void Start()
    {
        delta_position = transform.position - target_camera.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target_camera.transform.position + delta_position;
    }

}
