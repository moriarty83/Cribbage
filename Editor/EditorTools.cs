using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorTools : EditorWindow
{
    [MenuItem("Tools/Reset First Load")]

        public static void resetFirstPlay()
    {
        PlayerPrefs.SetInt("FirstLoad", 0);

        Debug.Log(PlayerPrefs.GetInt("FistLoad"));
    }

}
