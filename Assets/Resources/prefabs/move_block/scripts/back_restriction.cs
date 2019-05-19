using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back_restriction : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0);
    }
}
