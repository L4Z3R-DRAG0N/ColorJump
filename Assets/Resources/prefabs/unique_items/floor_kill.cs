using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_kill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dead");
        Destroy(other.gameObject);
    }
}
