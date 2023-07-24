using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers instance {get{Init(); return s_instance;}}

    DataManager _data = new DataManager();
    SceneMan _scene = new SceneMan();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return instance._data; } }
    public static SceneMan Scene { get { return instance._scene; } }
    public static UIManager UI { get { return instance._ui;  } }
    
    void Awake()
    {
        Init();
    }


    static void Init(){
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go==null)
            {
                go = new GameObject{name = "@Managers"};
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
        }
    }

    public static void Clear()
    {
        //Sound.Clear();
        //Input.Clear();
        //Scene.Clear();
        //UI_Manager.Clear();
        
        //Pool.Clear();
    }
}
