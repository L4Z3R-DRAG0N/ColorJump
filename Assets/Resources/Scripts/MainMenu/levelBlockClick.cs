using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelBlockClick : MonoBehaviour
{
    private Color original_color;
    private GameObject label;
    private string scene_index;

    private Material level_future;
    private Material level_now;
    private Material level_passed;

    private bool clickable;
    void Start()
    {
        label = transform.GetChild(0).gameObject;

        scene_index = transform.name.Substring(5, 3);

        level_future = (Material)Resources.Load("Materials/CubeMat/mainMenu/level_future", typeof(Material));
        level_now = (Material)Resources.Load("Materials/CubeMat/mainMenu/level_now", typeof(Material));
        level_passed = (Material)Resources.Load("Materials/CubeMat/mainMenu/level_passed", typeof(Material));

        clickable = false;
        // the next level of current finished level should also be clickable
        int largest_clickable = PlayerPrefs.GetInt("level_progress");
        // render block as passed if
        if (largest_clickable > int.Parse(scene_index))
        {
            gameObject.GetComponent<Renderer>().material = level_passed;
            clickable = true;
        }
        // render block as not passed if
        else if (largest_clickable < int.Parse(scene_index))
        {
            gameObject.GetComponent<Renderer>().material = level_future;
        }
        // render block as currect
        else
        {
            gameObject.GetComponent<Renderer>().material = level_now;
            clickable = true;
        }
        original_color = gameObject.GetComponent<MeshRenderer>().material.color;
        // init hide label
        // label.SetActive(false);
    }
    void OnMouseDown()
    {
        if (!clickable)
        {
            return;
        }
        try
        {
            SceneManager.LoadScene("level" + scene_index);
        } catch
        {

        }
    }

    void OnMouseEnter()
    {
        if (!clickable)
        {
            return;
        }
        Color new_color = new Color(1, 1, 0);
        gameObject.GetComponent<MeshRenderer>().material.color = new_color;
        // show label
        // label.SetActive(true);
    }

    void OnMouseExit()
    {
        if (!clickable)
        {
            return;
        }
        gameObject.GetComponent<MeshRenderer>().material.color = original_color;
        // hide label when mouse leave
        // label.SetActive(false);
    }
}
