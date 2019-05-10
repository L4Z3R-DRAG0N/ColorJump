using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initialize()
    {
        Physics.gravity = new Vector3(0, -10, 0);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    public void restart_level()
    {
        initialize();
        Scene scene = SceneManager.GetActiveScene();
        Application.LoadLevel(scene.name);
    }
}
