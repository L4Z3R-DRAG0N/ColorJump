using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1250000, 0, 1250000));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 8)
        {
            Destroy(this.gameObject);
        }
    }
}
