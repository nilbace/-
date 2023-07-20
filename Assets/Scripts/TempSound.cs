using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSound : MonoBehaviour
{
    AudioSource AudioSource;
    public static TempSound instance;
    [SerializeField] AudioClip Lobby;
    [SerializeField] AudioClip Stage;
    public enum BGMName { None,Lobby, Stage }
    BGMName _nowBGM = BGMName.None;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        AudioSource.volume = Managers.Data.MySettingData.BGMSound;
    }

    public void TurnONBGM(BGMName name)
    {
        if(_nowBGM != name)
        {
            _nowBGM = name;
            AudioSource.Stop();
            
            if (name == BGMName.Lobby)
            {
                AudioSource.clip = Lobby;
                AudioSource.Play();
            }
            else if(name == BGMName.Stage)
            {
                AudioSource.clip = Stage;
                AudioSource.Play();
            }
        }
    }
}
