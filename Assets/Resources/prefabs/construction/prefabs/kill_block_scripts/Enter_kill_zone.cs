using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_kill_zone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collide)
    {
        Kill(collide.gameObject);
    }

    void Kill(GameObject obj)
    {
        if (obj.name != "Player")
        {
            return;
        }
        // TODU: replace this with a death scene in the future
        obj.GetComponent<Controller>().display_dead_menu = true;
        Time.timeScale = 0;
    }
}
