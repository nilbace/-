using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSound : MonoBehaviour
{
    AudioSource AudioSource;
    public AudioSource EffectSound;
    public static TempSound instance;
    [SerializeField] AudioClip Lobby;
    [SerializeField] AudioClip Stage;
    public enum BGMName { None,Lobby, Stage }
    public enum EffectSoundName {
        button1,  
        weaponSound4,
        item,
        getCoin,
        powerUp,
        charDie2,
        monSound1,
        result,
        wea2, wea3, wea4
    }

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

    public void SFX(EffectSoundName ESN)
    {
        AudioClip temp = Resources.Load<AudioClip>($"SoundEffect/{ESN.ToString()}");
        EffectSound.volume = Managers.Data.MySettingData.SFXSound;
        EffectSound.PlayOneShot(temp);
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
