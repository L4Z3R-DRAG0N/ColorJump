using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelBlockClick : MonoBehaviour
{
    private Color original_color;
    private GameObject label;
    void Start()
    {
        original_color = transform.GetComponent<MeshRenderer>().material.color;
        label = transform.GetChild(0).gameObject;
        // init hide label
        // label.SetActive(false);
    }
    void OnMouseDown()
    {
        Scene scene = SceneManager.GetActiveScene();
        Application.LoadLevel("lab");
    }

    void OnMouseEnter()
    {
        Color new_color = original_color;
        new_color.b = original_color.b + 10;
        transform.GetComponent<MeshRenderer>().material.color = new_color;
        // show label
        // label.SetActive(true);
    }

    void OnMouseExit()
    {
        transform.GetComponent<MeshRenderer>().material.color = original_color;
        // hide label when mouse leave
        // label.SetActive(false);
    }
}
