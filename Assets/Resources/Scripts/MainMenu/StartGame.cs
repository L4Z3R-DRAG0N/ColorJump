using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private int level_progress;
    public void Start_Game() {
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
        // replace with an array of coordinates later
        int level_z = 30 - level_progress * 10;
        mainCamera.GetComponent<MainMenuCameraControl>().move_to = new Vector3(27, 10, level_z);
    }
}
