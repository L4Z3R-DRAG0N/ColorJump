using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private int level_progress;
    public void Start_Game() {
        // mark game started, so player don't need to watch the main menu start again
        PlayerPrefs.SetInt("GameFirstEnter", 1);
        // load player progress
        // init level_progress 0
        level_progress = 0;
        try
        {
            if (PlayerPrefs.HasKey("level_progress")) {
                // load if has key
                level_progress = PlayerPrefs.GetInt("level_progress", level_progress);
            } else {
                // if first start, no record, set progress as init value
                PlayerPrefs.SetInt("level_progress", level_progress);
                Debug.Log(level_progress);
            }
        }
        catch
        {
            PlayerPrefs.SetInt("level_progress", level_progress);
        }
        // set is_menu_start status to true to unlock mainCamera movement
        mainCamera.GetComponent<MainMenuCameraControl>().is_menu_start = true;
        // move mainCamera to current level recorded in player status file
        //mainCamera.GetComponent<MainMenuCameraControl>().move_to = new Vector3(27, 15, level_z);
        Vector3 target_level_block_pos = mainCamera.GetComponent<MainMenuCameraControl>().level_block_list[level_progress].transform.position;
        mainCamera.GetComponent<MainMenuCameraControl>().move_to = new Vector3(target_level_block_pos.x - 6, 15, target_level_block_pos.z);
    }
}
