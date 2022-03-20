using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelSelect;
    public string mainMenu;
    private LevelManager theLevelManager;
    public GameObject thePauseScreen;
    private PlayerControl thePlayer;
    public Image musicButton;
    public Image soundButton;
    public Sprite musicOnButtonSprite;
    public Sprite musicOffButtonSprite;
    public Sprite soundOnButtonSprite;
    public Sprite soundOffButtonSprite;
    public bool music;
    public bool sound;

    public GameObject avertismentLevelExitShop;
    public GameObject avertismentLevelExitMainMenu;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        thePlayer = FindObjectOfType<PlayerControl>();

        thePauseScreen.SetActive(false);
        Time.timeScale = 1f;
        thePlayer.canMove = true;

        if (PlayerPrefs.HasKey("Music"))
        {
            music = PlayerPrefsX.GetBool("Music");
        }
        if(PlayerPrefs.HasKey("Sound"))
        {
            sound = PlayerPrefsX.GetBool("Sound");
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerPrefsX.SetBool("Music", music);
        PlayerPrefsX.SetBool("Sound", sound);
        if(music == true)
        {
            musicButton.sprite = musicOnButtonSprite;
        }
        else
        {
            musicButton.sprite = musicOffButtonSprite;
        }
        if(sound == true)
        {
            soundButton.sprite = soundOnButtonSprite;
        }
        else
        {
            soundButton.sprite = soundOffButtonSprite;
        }
    } 
    public void PressButton()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        Time.timeScale = 0;
        thePauseScreen.SetActive(true);
        thePlayer.canMove = false;
    }
    public void ResumeGame()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        thePauseScreen.SetActive(false);
        Time.timeScale = 1f;
        thePlayer.canMove = true;
    }
    public void Shop()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        avertismentLevelExitShop.SetActive(true);
        Time.timeScale = 1f;
        thePlayer.canMove = true;
    }

    public void QuitToMainMenu()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        Time.timeScale = 1f;
        thePlayer.canMove = true;
        avertismentLevelExitMainMenu.SetActive(true);

    }

    public void BackQuitToMainMenu()
	{
        avertismentLevelExitMainMenu.SetActive(false);
	}
    public void BackQuitToShop()
    {
        avertismentLevelExitShop.SetActive(false);
    }

    public void GoQuitToMainMenu()
    {
        thePauseScreen.SetActive(false);
        avertismentLevelExitMainMenu.SetActive(false);
        SoundManager.LoadScene("Main Menu");
    }
    public void GoQuitToShop()
    {
        thePauseScreen.SetActive(false);

        avertismentLevelExitShop.SetActive(false);
        SoundManager.LoadScene("Store");
    }


    public void MusicButton()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        } 
        if(music == true)
        {
            music = false;
        }
        else
        {
            music = true;
        }
        PlayerPrefsX.SetBool("Music", music);
    }
    public void SoundButton()
    {
        if(sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        } 
        if(sound == true)
        {
           sound = false;
        }
        else
        {
           sound = true;
        }
        PlayerPrefsX.SetBool("Sound", sound);
    }

}
