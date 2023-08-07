using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySelect : MonoBehaviour
{
    [SerializeField] StoryType storyType = StoryType.Story;
    [SerializeField] GameObject[] storyCutImage;
    int openIndex = 0;

    TempSound.BGMName lastBGM;

    private bool StoryRead;


    public enum StoryType
    {
        Story,
        Prologue
    }

    // Start is called before the first frame update
    void Start()
    {
        lastBGM = TempSound.instance._nowBGM;
        if(storyType == StoryType.Story)
            TempSound.instance.TurnONBGM(TempSound.BGMName.Story);
        else if(storyType == StoryType.Prologue)
        {
            TempSound.instance.TurnONBGM(TempSound.BGMName.Prologue);
            StoryRead = PlayerPrefs.GetInt("StoryRead", 0) == 1;
            if (StoryRead) UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoryOpen(int index)
    {
        Debug.Log(index + " StoryOpen");

        storyCutImage[index].SetActive(true);
        Debug.Log(storyCutImage[index].name + " Open");
        openIndex = index;
    }

    public void StoryExit()
    {
        Debug.Log("StoryExit");
        storyCutImage[openIndex].SetActive(false);
        //이 오브젝트를 아예 죽여야되나...
        TempSound.instance.TurnONBGM(lastBGM);
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    public void GoToLobby()
    {
        StoryRead = true;
        PlayerPrefs.SetInt("StoryRead", StoryRead ? 1 : 0);
        PlayerPrefs.Save(); // 변경 사항 저장

        Debug.Log("GoToLobby");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");

    }
}
