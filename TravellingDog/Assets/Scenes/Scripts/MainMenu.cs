using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string continueGame;
    public string levelSelect;
    public string settings;
    public string store;
    public string[] levelNames;
    public int startingLives;
    public Text coinText;
    private bool[] status;
    private int numberSelectedSkin;
    public Text newGameText;
    public bool[] levelUNLOCKED;
    private int j = 1;
    public Image backgroundImage;
    public Sprite forestImage;
    public Sprite caveImage;
    public Sprite mountainImage;
    public Sprite cityImage;
    public bool sound;
    void Start()
    {
        AudioListener.volume = 0.25f;
        if (PlayerPrefs.HasKey("Sound"))
        {
            sound = PlayerPrefsX.GetBool("Sound");
        }

         if(PlayerPrefs.HasKey("LevelUNLOCKED_1"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED_1");
        }

        if(PlayerPrefs.HasKey("Status"))
        {
            status = PlayerPrefsX.GetBoolArray("Status");
            PlayerPrefsX.SetBoolArray("StatusMenu",status);
        }   

        if(PlayerPrefs.HasKey("NumberSelectedSkin"))
        {
            numberSelectedSkin = PlayerPrefs.GetInt("NumberSelectedSkin");
            PlayerPrefs.SetInt("NumberSelectedSkinMenu", numberSelectedSkin);
        }

        if(levelUNLOCKED[2] == true)
        {
            newGameText.text = "Continue";
        }

        for(int i=1;i<levelUNLOCKED.Length;i++)
        {
            if(levelUNLOCKED[i] == true)
            {
                j=i;
            }
        }

        if(levelUNLOCKED[11] == true)
        {
            backgroundImage.sprite = caveImage;
        }

        if(levelUNLOCKED[21] == true)
        {
            backgroundImage.sprite = mountainImage;
        }

        if(levelUNLOCKED[31] == true)
        {
            backgroundImage.sprite = cityImage;
        }
        PlayerPrefs.SetInt("isLoaded", 0);

    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = 0.25f;
    }

    public void NewGame()
    {
        if (PlayerPrefs.HasKey("LevelUNLOCKED_1"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED_1");
        }

        for (int i = 1; i < levelUNLOCKED.Length; i++)
        {
            if (levelUNLOCKED[i] == true)
            {
                j = i;
            }
        }

        if (sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        SoundManager.LoadScene("Level"+j);
        for(int i=0; i< levelNames.Length;i++)
        {
            PlayerPrefs.SetInt(levelNames[i],0);
        }
        PlayerPrefs.SetInt("PlayerLives", startingLives);
    }
    public void Store()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        SoundManager.LoadScene("Store");
    }
    public void Settings()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        SoundManager.LoadScene("Settings");
    }
    public void LevelSelect()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        SoundManager.LoadScene("LevelSelect");
    }
    public void QuitGame()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        Application.Quit();
    }

}
