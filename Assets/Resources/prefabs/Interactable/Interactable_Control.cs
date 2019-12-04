using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Control : MonoBehaviour
{

    public bool Acceleration_Refuel;
    public bool Destroy_After_Used;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(1, 1, 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.name != "Player")
        {
            return;
        }
        else
        {
            float[] block_mode_cooldown_time_list = obj.GetComponent<Controller>().getBlockModeCooldownTimeList();
            if (Acceleration_Refuel)
            {
                block_mode_cooldown_time_list[0] = 1;
            }
            obj.GetComponent<Controller>().setBlockModeCooldownTimeList(block_mode_cooldown_time_list);

            if (Destroy_After_Used)
            {
                Destroy(gameObject);
            }
        }
    }
}
