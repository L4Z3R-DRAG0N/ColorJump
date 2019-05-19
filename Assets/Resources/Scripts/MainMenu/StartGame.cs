using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public void Start_Game() {
        if (PlayerPrefs.GetInt("GameFirstEnter") == 0)
        {
            // mark game started, so player don't need to watch the main menu start again
            PlayerPrefs.SetInt("GameFirstEnter", 1);

            // set is_menu_start status to true to unlock mainCamera movement
            mainCamera.GetComponent<MainMenuCameraControl>().is_menu_start = true;
        }
        else
        {
            // move mainCamera to current level recorded in player status file
            Vector3 target_level_block_pos = mainCamera.GetComponent<MainMenuCameraControl>().target_level_block_pos;
            mainCamera.GetComponent<MainMenuCameraControl>().move_to = new Vector3(target_level_block_pos.x - 6, 15, target_level_block_pos.z);
        }
    }
}
