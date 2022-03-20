using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public string mainMenu;
   // public bool[] levelUnlocked;
    public Sprite locked;
    public Sprite unlocked;
    public Image[] button;
    public GameObject Chapter1;
    public GameObject Chapter2;
    public GameObject Chapter3;
    public GameObject Chapter4;
    public bool[] levelUNLOCKED;
    public Sprite buttonLevelSelectLocked;
    public Sprite buttonLevelSelectUnlocked;
    public Image buttonMoveToChapter2;
    public Image buttonMoveToChapter3;
    public Image buttonMoveToChapter4;
    public bool sound;

    void Start()
    {
        if(PlayerPrefs.HasKey("Sound"))
        {
            sound = PlayerPrefsX.GetBool("Sound");
        }
        if(PlayerPrefs.HasKey("LevelUNLOCKED_1"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED_1");
        }
        levelUNLOCKED[1] = true;

        for (int i=1; i<levelUNLOCKED.Length; i++)
        {
            if(levelUNLOCKED[i] == true)
            {
                button[i].sprite = unlocked;
            }
            else
            {
                button[i].sprite = locked;
            }
        }
        if(levelUNLOCKED[11] == true)
        {
            buttonMoveToChapter2.sprite = buttonLevelSelectUnlocked;
        }
        else
        {
            buttonMoveToChapter2.sprite = buttonLevelSelectLocked;
        }
        if(levelUNLOCKED[21] == true)
        {
            buttonMoveToChapter3.sprite = buttonLevelSelectUnlocked;
        }
        else
        {
            buttonMoveToChapter3.sprite = buttonLevelSelectLocked;
        }
        if(levelUNLOCKED[31] == true)
        {
            buttonMoveToChapter4.sprite = buttonLevelSelectUnlocked;
        }
        else
        {
            buttonMoveToChapter4.sprite = buttonLevelSelectLocked;
        }

        Chapter1.SetActive(true);
        Chapter2.SetActive(false);
        Chapter3.SetActive(false);
        Chapter4.SetActive(false);
    }
    void Update()
    {
        for(int i=1; i<levelUNLOCKED.Length; i++)
        {
            if(levelUNLOCKED[i] == true)
            {
                button[i].sprite = unlocked;
            }
            else
            {
                button[i].sprite = locked;
            }
        }  
         if(levelUNLOCKED[11] == true)
        {
            buttonMoveToChapter2.sprite = buttonLevelSelectUnlocked;
        }
        else
        {
            buttonMoveToChapter2.sprite = buttonLevelSelectLocked;
        }
        if(levelUNLOCKED[21] == true)
        {
            buttonMoveToChapter3.sprite = buttonLevelSelectUnlocked;
        }
        else
        {
            buttonMoveToChapter3.sprite = buttonLevelSelectLocked;
        }
        if(levelUNLOCKED[31] == true)
        {
            buttonMoveToChapter4.sprite = buttonLevelSelectUnlocked;
        }
        else
        {
            buttonMoveToChapter4.sprite = buttonLevelSelectLocked;
        }
    }
    public void MainMenu()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        SoundManager.LoadScene("Main Menu");
    } 
    public void Status(int x)
    {
        if(levelUNLOCKED[x] == true)
        {
            if(sound == true)
            {
                SoundManager.PlaySound("ButtonSound");
            }
            SoundManager.LoadScene("Level" + x);
        }
    }

    public void MoveToChapter1()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        Chapter1.SetActive(true);
        Chapter2.SetActive(false);
        Chapter3.SetActive(false);
        Chapter4.SetActive(false);
    }
    public void MoveToChapter2()
    {
        if(levelUNLOCKED[11] == true)
        {
            if(sound == true)
            {
                SoundManager.PlaySound("ButtonSound");
            }
            Chapter2.SetActive(true);
            Chapter1.SetActive(false);
            Chapter3.SetActive(false);
            Chapter4.SetActive(false);
        }
    }
    public void MoveToChapter3()
    {
        if(levelUNLOCKED[21] == true)
        {
            if(sound == true)
            {
                SoundManager.PlaySound("ButtonSound");
            }
            Chapter3.SetActive(true);
            Chapter1.SetActive(false);
            Chapter2.SetActive(false);
            Chapter4.SetActive(false);
        }
    }
    public void MoveToChapter4()
    {
        if(levelUNLOCKED[31] == true)
        {
            if(sound == true)
            {
                SoundManager.PlaySound("ButtonSound");
            }
            Chapter4.SetActive(true);
            Chapter1.SetActive(false);
            Chapter2.SetActive(false);
            Chapter3.SetActive(false);
        }
    }
}