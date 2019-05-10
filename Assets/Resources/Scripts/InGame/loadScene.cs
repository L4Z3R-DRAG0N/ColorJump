using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewBehaviourScript2 : MonoBehaviour 
{
    public     Texture BackImage = null;
    private AsyncOperation async = null;
    void Start () 
    {
        //此物体在下一个场景中不会被销毁
        DontDestroyOnLoad(this);

        //开始加载场景
        StartCoroutine("LoadScene");
    }
    
    //异步加载
    IEnumerator LoadScene()
    {
        async = Application.LoadLevelAsync("Next");
        yield return async;
        Debug.Log("Complete!");
    }

    void OnGUI()
    {
        //切换场景中的背景，可以是图片或者动画，或者～～
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackImage);

        //加载过程中显示进度，也可以是进度条
        if (async != null && async.isDone == false)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 48;
            GUI.Label(new Rect(0, 200, Screen.width, 20), async.progress.ToString("F2"), style);
        }

        //在加载结束后，弹出是否下个场景，模拟手游中"触摸任意位置进入游戏"
        if (async != null && async.isDone == true)
        {
            if (GUI.Button(new Rect(100, 100, 100, 100), new GUIContent("跳起进入下一个场景")))
            {
                Destroy(this);
            }
        }


    }
}