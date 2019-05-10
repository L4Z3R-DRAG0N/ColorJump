using UnityEngine;
using System.Collections;

public class human_controller : MonoBehaviour
{
    private Animator human_animator;
    private Rigidbody rigBody;
    private Vector3 dir;

    void Start()
    {
        human_animator = this.GetComponent<Animator>();
        rigBody = GetComponent<Rigidbody>();
        dir = Vector3.zero;
    }
    
    void Update()
    {
        // input vertical
        float vertical = Input.GetAxis("Vertical");
        // input horizontal
        float horizontal = Input.GetAxis("Horizontal");
        // direction vector
        if (vertical > 0) {
            dir = new Vector3(horizontal, 0, vertical);
        } else {
            dir = new Vector3(horizontal, 0, 0);
        }

        if (dir != Vector3.zero) {
            // input recieved
            // rotate
            Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up) * transform.rotation;
            // 创建新旋转值 并根据转向速度平滑转至目标旋转值
            //函数参数解释: Lerp(角色刚体当前旋转值, 目标旋转值, 根据旋转速度平滑转向)
            Quaternion newRotation = Quaternion.Lerp(rigBody.rotation, targetRotation, 1 * Time.deltaTime);
            // 更新刚体旋转值为 新旋转值
            rigBody.MoveRotation(newRotation);

            if (Input.GetKey(KeyCode.LeftShift) && human_animator.GetBool("isWalking")) {
                
                // run animation
                transform.Translate(Vector3.forward * 4 * Time.deltaTime, Space.Self);
                human_animator.SetBool("isRunning", true);
            } else {
                // walk animation
                transform.Translate(Vector3.forward * 2 * Time.deltaTime, Space.Self);
                human_animator.SetBool("isWalking", true);
                human_animator.SetBool("isRunning", false);
            }
            
        } else {
            human_animator.SetBool("isWalking", false);
            human_animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            human_animator.SetBool("isWalking", true);
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 200, 0));
        }

    }
}