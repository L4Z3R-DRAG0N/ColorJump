using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearProgress : MonoBehaviour
{
    public void Clean()
    {
        PlayerPrefs.DeleteAll();
    }
}
