using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    Rigidbody player_rigid_body;

    float original_gravity_y;

    Material normal_mat;
    Material accelerate_mat;
    Material decelerate_mat;
    Material jump_mat;
    Material antigravity_mat;
    Material restoregravity_mat;
    Material zerogravity_mat;
    Material timeslow_mat;

    private int        block_mode_index;
    private string[]   block_mode_list;
    private Material[] block_mode_material_list;
    private float[]    block_mode_cooldown_time_list;
    private float[]    block_mode_cooldown_deplete_delta;
    private float[]    block_mode_cooldown_restore_delta;
    

    [SerializeField] GameObject UI;

    public float init_velocity;

    private GameObject exit_menu;
    public bool display_exit_menu;

    private GameObject setting_menu;
    public bool display_setting_menu;

    private GameObject dead_menu;
    public bool display_dead_menu;

    private GameObject pass_menu;
    public bool display_pass_menu;


    private GameObject ingame_UI;
    private GameObject color_panels;
    private GameObject color_labels;
    private GameObject highlighter;


    private bool is_colliding;

    private Quaternion rotateTo;
    private float rotateSpeed;

    private float camera_fov_target;
    private float camera_fov_delta;

    // Start is called before the first frame update
    void Start()
    {
        player_rigid_body = GetComponent<Rigidbody>();

        original_gravity_y = -Mathf.Abs(Physics.gravity.y);

        normal_mat         = (Material) Resources.Load("Materials/CubeMat/normal",         typeof(Material));
        accelerate_mat     = (Material) Resources.Load("Materials/CubeMat/accelerate",     typeof(Material));
        decelerate_mat     = (Material) Resources.Load("Materials/CubeMat/decelerate",     typeof(Material));
        jump_mat           = (Material) Resources.Load("Materials/CubeMat/jump",           typeof(Material));
        antigravity_mat    = (Material) Resources.Load("Materials/CubeMat/antigravity",    typeof(Material));
        restoregravity_mat = (Material) Resources.Load("Materials/CubeMat/restoregravity", typeof(Material));
        zerogravity_mat    = (Material) Resources.Load("Materials/CubeMat/zerogravity",    typeof(Material));
        timeslow_mat       = (Material) Resources.Load("Materials/CubeMat/timeslow",       typeof(Material));


        block_mode_list = new string[]{"accelerate", "decelerate", "jump", "antigravity", "normal"};
        block_mode_material_list = new Material[] { accelerate_mat, decelerate_mat, jump_mat, antigravity_mat, normal_mat };
        block_mode_cooldown_time_list = new float[] {1, 1, 1, 1, 1};
        block_mode_cooldown_deplete_delta = new float[] { 0.01f, 0.01f, 0.5f, 1f, 0f };
        block_mode_cooldown_restore_delta = new float[] { 0.005f, 0.005f, 0.00f, 0.005f, 1f };
        block_mode_index = 0;


        exit_menu = UI.transform.Find("exit_menu").gameObject;
        display_exit_menu = false;

        setting_menu = UI.transform.Find("setting_menu").gameObject;
        display_setting_menu = false;

        dead_menu = UI.transform.Find("dead_menu").gameObject;
        display_dead_menu = false;

        pass_menu = UI.transform.Find("pass_menu").gameObject;
        display_pass_menu = false;

        ingame_UI = UI.transform.Find("ingame_UI").gameObject;
        color_panels = ingame_UI.transform.Find("color_panels").gameObject;
        color_labels = ingame_UI.transform.Find("color_labels").gameObject;
        highlighter = ingame_UI.transform.Find("highlighter").gameObject;


        is_colliding = false;

        rotateTo = new Quaternion();
        rotateSpeed = 0.2f;

        camera_fov_target = 60;
        camera_fov_delta = 0.1f;

        // init velocity
        player_rigid_body.velocity = new Vector3(init_velocity, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        // exit_menu
        exit_menu.SetActive(display_exit_menu);
        // setting_menu
        setting_menu.SetActive(display_setting_menu);
        // dead_menu
        dead_menu.SetActive(display_dead_menu);
        // pass_menu
        pass_menu.SetActive(display_pass_menu);
        // DO NOT PUT THIS INTO FIXED UPDATE!!!
        // since pressing esc will pause the game (Time.timeScale = 0;) and FixedUpdte won't execute anymore
        EscapeDetect();
    }

    void FixedUpdate()
    {
        InteractWithKeys();
        // do not go back
        //if (player_rigid_body.velocity.x < 0) {
        //    player_rigid_body.velocity = new Vector3(0, 0, 0);
        //}

        // smooth rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, rotateSpeed);
        ingame_UI.transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, rotateSpeed);

        // acceleration fov effect
        camera_fov_target = 60 + Mathf.Abs(GetComponent<Rigidbody>().velocity.x) / 3;
        Camera.main.GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.GetComponent<Camera>().fieldOfView, camera_fov_target, camera_fov_delta);

        RestoreCooldown();
    }

    void OnCollisionEnter()
    {
        is_colliding = true;
        // jump_count = 0;
        // reset jump cooldown (1 is the max fuel)
        block_mode_cooldown_time_list[2] = 1;
    }

    void OnCollisionStay()
    {
        is_colliding = true;
        // reset jump cooldown (1 is the max fuel)
        block_mode_cooldown_time_list[2] = 1;
    }

    void OnCollisionExit()
    {
        is_colliding = false;
    }

    void EscapeDetect()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if ... don't react to ESC operation
            if (display_dead_menu || display_setting_menu || display_pass_menu)
            {
                return;
            }
            if (!display_exit_menu)
            {
                //pause time
                Time.timeScale = 0;
                //display menu
                display_exit_menu = true;
            }
            else
            {
                //resume time
                Time.timeScale = 1;
                //hide menu
                display_exit_menu = false;
            }

        }

    }

    void InteractWithKeys() {
        if (display_dead_menu || display_setting_menu || display_exit_menu || display_pass_menu)
        {
            // do not react to input if any menu is shown
            return;
        }
        // if (is_colliding) {
        // Jump (allow double jump)
        if (Input.GetButtonDown("Jump"))
        {
            block_mode_index = 2;
            if (block_mode_cooldown_time_list[block_mode_index] >= block_mode_cooldown_deplete_delta[block_mode_index])
            {
                GetComponent<Renderer>().material = block_mode_material_list[block_mode_index];
                block_mode_cooldown_time_list[block_mode_index] -= block_mode_cooldown_deplete_delta[block_mode_index];
                Jump();
            }
            Debug.Log("jump");
        }

        // accelerate
        if (Input.GetAxis("Horizontal") > 0)
        {
            block_mode_index = 0;
            if (block_mode_cooldown_time_list[block_mode_index] >= block_mode_cooldown_deplete_delta[block_mode_index])
            {
                GetComponent<Renderer>().material = block_mode_material_list[block_mode_index];
                block_mode_cooldown_time_list[block_mode_index] -= block_mode_cooldown_deplete_delta[block_mode_index];
                Accelerate();
            }
        }
        // decelerate
        if (Input.GetAxis("Horizontal") < 0)
        {
            block_mode_index = 1;
            if (block_mode_cooldown_time_list[block_mode_index] >= block_mode_cooldown_deplete_delta[block_mode_index])
            {
                GetComponent<Renderer>().material = block_mode_material_list[block_mode_index];
                block_mode_cooldown_time_list[block_mode_index] -= block_mode_cooldown_deplete_delta[block_mode_index];
                Decelerate();
            }
        }

        // antigravity
        if (Input.GetAxis("Vertical") < 0)
        {
            block_mode_index = 3;
            if (block_mode_cooldown_time_list[block_mode_index] >= block_mode_cooldown_deplete_delta[block_mode_index])
            {
                GetComponent<Renderer>().material = block_mode_material_list[block_mode_index];
                block_mode_cooldown_time_list[block_mode_index] -= block_mode_cooldown_deplete_delta[block_mode_index];
                Antigravity();
            }
        }

            

        RenderBlockModeUI(block_mode_index);
        // restore normal mode, since normal mode is the last mode, index is length -1
        block_mode_index = block_mode_list.Length - 1;
    }

    void RenderBlockModeUI(int block_mode_index) {
        // HighLightChoosenColor and render percentage
        // -1: igore normal render, so length -1 (normal is the last mode in mode list)
        for (int i = 0; i < block_mode_list.Length - 1; i ++) {
            if (i == block_mode_index) {
                // highlight
                color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                color_labels.transform.Find(block_mode_list[i] + "_label").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                highlighter.transform.localPosition = color_labels.transform.localPosition + color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.transform.localPosition;
            } else {
                // dim
                color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                color_labels.transform.Find(block_mode_list[i] + "_label").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
            }
            
        }
    }

    void RestoreCooldown() {
        // status cooldown bars
        // -1: igore normal render, so length -1 (normal is the last mode in mode list)
        for (int i = 0; i < block_mode_list.Length - 1; i ++) {
            // render percentage
            color_panels.transform.Find(block_mode_list[i] + "_color").gameObject.GetComponent<Image>().fillAmount = block_mode_cooldown_time_list[i];
            // cooldown
            if (block_mode_cooldown_time_list[i] >= 1){
                block_mode_cooldown_time_list[i] = 1;
            } else {
                block_mode_cooldown_time_list[i] += block_mode_cooldown_restore_delta[i];
            }
        }
    }

    void Accelerate() {
        player_rigid_body.AddForce(new Vector3(30, 0, 0));
    }
    void Decelerate() {
        player_rigid_body.AddForce(new Vector3(-30, 0, 0));
    }
    void Jump() {
        // clear falling velocity
        player_rigid_body.velocity = new Vector3(player_rigid_body.velocity.x, 0, player_rigid_body.velocity.z);
        player_rigid_body.AddForce(40 * -Physics.gravity);
    }
    void Antigravity() {
        if (Physics.gravity.y > 0)
        {
            // restore normal gravity if gravity is reversed (y > 0)
            rotateTo = Quaternion.Euler(new Vector3(0, 0, 0));
            Physics.gravity = new Vector3(0, original_gravity_y, 0);
        }
        else if (Physics.gravity.y < 0)
        {
            // set anti gravity if gravity is normal (y < 0)
            rotateTo = Quaternion.Euler(new Vector3(180, 0, 0));
            Physics.gravity = new Vector3(0, -original_gravity_y, 0);
        }
        // do nothing if in zero gravity mode
    }

    void Timeslow() {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    public GameObject getTimeDisplay()
    {
        return pass_menu.transform.Find("time").gameObject;
    }
}
