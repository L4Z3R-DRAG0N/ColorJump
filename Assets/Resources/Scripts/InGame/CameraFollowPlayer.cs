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
        // var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        // var y = Mathf.Lerp(transform.position.y, player.transform.position.y, positionLerpPct);
        // transform.position = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z) + delta_position;
        transform.position = player.transform.position + delta_position;        
    }
}
