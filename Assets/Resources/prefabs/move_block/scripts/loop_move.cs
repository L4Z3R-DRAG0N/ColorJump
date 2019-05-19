using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loop_move : MonoBehaviour
{
    public bool use_move;
    public static int pos_amount = 0;
    public Vector3[] path_position_array = new Vector3[pos_amount + 1];
    public float move_speed;
    private float current_path_progress;
    private int current_index = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < path_position_array.Length; i++)
        {
            path_position_array[i] += transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!use_move)
        {
            return;
        }
        if (current_path_progress + move_speed / 100 < 1)
        {
            current_path_progress += move_speed / 100;
        }
        else
        {
            current_path_progress = 0;
            if (current_index >= path_position_array.Length - 1)
            {
                current_index = 0;
            }
            else
            {
                current_index += 1;
            }
        }
        
        if (current_index + 1 < path_position_array.Length)
        {
            transform.position = Vector3.Lerp(path_position_array[current_index], path_position_array[current_index + 1], current_path_progress);
        }
        else
        {
            transform.position = Vector3.Lerp(path_position_array[current_index], path_position_array[0], current_path_progress);
        }
    }
}
