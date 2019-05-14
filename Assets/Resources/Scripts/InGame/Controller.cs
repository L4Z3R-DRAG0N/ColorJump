using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Controller : MonoBehaviour
{
    Rigidbody player_rigid_body;

    float original_gravity;

    Material normal_mat;
    Material accelerate_mat;
    Material decelerate_mat;
    Material jump_mat;
    Material antigravity_mat;
    Material restoregravity_mat;
    Material zerogravity_mat;
    Material timeslow_mat;

    private int      block_mode_index;
    private string[] block_mode_list;
    private float[] block_mode_cooldown_time_list;
    public bool      display_menu;
    [SerializeField] GameObject UI;
    private GameObject exit_menu;
    private GameObject ingame_UI;
    private GameObject color_panels;

    private bool is_colliding;
    private int  jump_count;

    // Start is called before the first frame update
    void Start()
    {
        player_rigid_body = GetComponent<Rigidbody>();

        original_gravity = -Mathf.Abs(Physics.gravity.y);

        normal_mat         = (Material) Resources.Load("Materials/CubeMat/normal",         typeof(Material));
        accelerate_mat     = (Material) Resources.Load("Materials/CubeMat/accelerate",     typeof(Material));
        decelerate_mat     = (Material) Resources.Load("Materials/CubeMat/decelerate",     typeof(Material));
        jump_mat           = (Material) Resources.Load("Materials/CubeMat/jump",           typeof(Material));
        antigravity_mat    = (Material) Resources.Load("Materials/CubeMat/antigravity",    typeof(Material));
        restoregravity_mat = (Material) Resources.Load("Materials/CubeMat/restoregravity", typeof(Material));
        zerogravity_mat    = (Material) Resources.Load("Materials/CubeMat/zerogravity",    typeof(Material));
        timeslow_mat       = (Material) Resources.Load("Materials/CubeMat/timeslow",       typeof(Material));

        block_mode_list = new string[]{"normal", "accelerate", "decelerate", "jump", "antigravity", "restoregravity", "zerogravity"};
        block_mode_cooldown_time_list = new float[]{1, 1, 1, 1, 1, 1, 1};
        block_mode_index = 0;

        display_menu = false;
        exit_menu = UI.transform.Find("exit_menu").gameObject;
        ingame_UI = UI.transform.Find("ingame_UI").gameObject;
        color_panels = ingame_UI.transform.Find("color_panels").gameObject;

        is_colliding = false;
        jump_count = 0;

        // init velocity
        player_rigid_body.velocity = new Vector3(10, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        if (display_menu) {
            // exit_menu
            exit_menu.SetActive(true);
        }else{
            exit_menu.SetActive(false);
        }
        // DO NOT PUT THIS INTO FIXED UPDATE!!!
        // since pressing esc will pause the game (Time.timeScale = 0;) and FixedUpdte won't execute anymore
        InteractWithKeys();
    }

    void FixedUpdate()
    {
        // do not go back
        if (player_rigid_body.velocity.x < 0) {
            player_rigid_body.velocity = new Vector3(0, 0, 0);
        }
        RestoreCooldown();
    }

    void OnCollisionEnter()
    {
        is_colliding = true;
        // jump_count = 0;
        // reset jump cooldown (1 is the max fuel)
        block_mode_cooldown_time_list[3] = 1;
    }

    void OnCollisionStay()
    {
        is_colliding = true;
    }

    void OnCollisionExit()
    {
        is_colliding = false;
    }

    void InteractWithKeys() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (Time.timeScale != 0){
                //pause time
                Time.timeScale = 0;
                //display menu
                display_menu = true;
            }else{
                //resume time
                Time.timeScale = 1;
                //hide menu
                display_menu = false;
            }
            
        }

        // if (is_colliding) {

        if (Input.GetKey(KeyCode.LeftShift)){
            block_mode_index = 1;
            if (block_mode_cooldown_time_list[block_mode_index] > 0) {
                GetComponent<Renderer>().material = accelerate_mat;
                block_mode_cooldown_time_list[block_mode_index] -= 0.01f;
                Accelerate();
            }
        }
        if (Input.GetKey(KeyCode.LeftControl)){
            block_mode_index = 2;
            if (block_mode_cooldown_time_list[block_mode_index] > 0 && player_rigid_body.velocity.x > 10) {
                GetComponent<Renderer>().material = decelerate_mat;            
                block_mode_cooldown_time_list[block_mode_index] -= 0.01f;
                Decelerate();
            }
        }
        // Jump (allow double jump)
        if (Input.GetKeyDown(KeyCode.Space)){
            block_mode_index = 3;
            if (block_mode_cooldown_time_list[block_mode_index] >= 0.5f) {
                GetComponent<Renderer>().material = jump_mat;            
                block_mode_cooldown_time_list[block_mode_index] -= 0.5f;
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.W)){
            block_mode_index = 4;
            if (block_mode_cooldown_time_list[block_mode_index] >= 1.0f) {
                GetComponent<Renderer>().material = antigravity_mat;            
                block_mode_cooldown_time_list[block_mode_index] -= 1f;
                Antigravity();
            }
        }else if (Input.GetKeyDown(KeyCode.S)){
            block_mode_index = 5;
            if (block_mode_cooldown_time_list[block_mode_index] >= 1.0f) {
                GetComponent<Renderer>().material = restoregravity_mat;            
                block_mode_cooldown_time_list[block_mode_index] -= 1f;
                Restoregravity();
            }
        }else if (Input.GetKeyDown(KeyCode.Q)){
            block_mode_index = 6;
            if (block_mode_cooldown_time_list[block_mode_index] >= 1.0f) {
                GetComponent<Renderer>().material = zerogravity_mat;            
                block_mode_cooldown_time_list[block_mode_index] -= 1f;
                Zerogravity();
            }
        }

            // if (Input.GetKey(KeyCode.None)) {
            //     // original
            //     GetComponent<Renderer>().material = normal_mat;
            //     block_mode_index = 0;
            // }
        // } else {
        //     // original
        //     GetComponent<Renderer>().material = normal_mat;
        //     block_mode_index = 0;
        // }

        
        RenderBlockModeUI(block_mode_index);
    }

    void RenderBlockModeUI(int block_mode_index) {
        // HighLightChoosenColor and render percentage
        for (int i = 0; i < block_mode_list.Length; i ++) {
            if (i == block_mode_index) {
                // highlight
                color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.GetComponent<Image>().color = new Color(255,255,255,255);
            } else {
                // dim
                color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.196f);
            }
            
        }
    }

    void RestoreCooldown() {
        // status cooldown bars
        for (int i = 0; i < block_mode_list.Length; i ++) {
            // render percentage
            color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.GetComponent<Image>().fillAmount = block_mode_cooldown_time_list[i];
            // cooldown
            if (block_mode_cooldown_time_list[i] >= 1){
                block_mode_cooldown_time_list[i] = 1;
            } else {
                block_mode_cooldown_time_list[i] += 0.001f;
            }
        }
    }

    void Accelerate() {
        player_rigid_body.AddForce(new Vector3(20, 0, 0));
    }
    void Decelerate() {
        player_rigid_body.AddForce(new Vector3(-20, 0, 0));
    }
    void Jump() {
        player_rigid_body.AddForce(40 * -Physics.gravity);
        jump_count += 1;
    }
    void Antigravity() {
        Physics.gravity = new Vector3(0, -original_gravity, 0);
        transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        ingame_UI.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
    }
    void Restoregravity() {
        Physics.gravity = new Vector3(0, original_gravity, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        ingame_UI.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    void Zerogravity() {
        Physics.gravity = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        ingame_UI.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    void Timeslow() {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }
}
