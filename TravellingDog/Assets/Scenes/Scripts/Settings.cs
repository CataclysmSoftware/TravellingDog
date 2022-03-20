using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public string back;
    public Sprite MusicAndSound_Off;
    public Sprite MusicAndSound_On;
    public Image musicButton;
    public Image soundButton;
    public Image backgroundImage;
    public Sprite forestImage;
    public Sprite caveImage;
    public Sprite mountainImage;
    public Sprite cityImage;
    public bool[] levelUNLOCKED;
    public bool music = true;
    public bool sound = true;
    public GameObject questionScreen;
    public GameObject questionScreenHolder;
    public int deathCount;
    public Text deathCountText;

    void Start()
    {
        if (PlayerPrefs.HasKey("DeathCount"))
        {
            deathCount = PlayerPrefs.GetInt("DeathCount");
        }
        if (PlayerPrefs.HasKey("LevelUNLOCKED_1"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED_1");
        }
        if (levelUNLOCKED[11] == true)
        {
            backgroundImage.sprite = caveImage;
        }
        if (levelUNLOCKED[21] == true)
        {
            backgroundImage.sprite = mountainImage;
        }
        if (levelUNLOCKED[31] == true)
        {
            backgroundImage.sprite = cityImage;
        }
        if (PlayerPrefs.HasKey("Music"))
        {
            music = PlayerPrefsX.GetBool("Music");
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            sound = PlayerPrefsX.GetBool("Sound");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefsX.SetBool("Music", music);
        PlayerPrefsX.SetBool("Sound", sound);
        if (music == true)
        {
            musicButton.sprite = MusicAndSound_On;
        }
        else
        {
            musicButton.sprite = MusicAndSound_Off;
        }
        if (sound == true)
        {
            soundButton.sprite = MusicAndSound_On;
        }
        else
        {
            soundButton.sprite = MusicAndSound_Off;
        }
    }
    public void BackTo()
    {
        if (sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        SoundManager.LoadScene("Main Menu");
    }

    public void Music()
    {
        //if (music == true)
        //{
        //    SoundManager.PlaySound("ButtonSound");
        //    music = false;
        //    musicButton.sprite = MusicAndSound_Off;
        //}
        //else
        //{
        //    music = true;
        //    musicButton.sprite = MusicAndSound_On;
        //}
        //PlayerPrefsX.SetBool("Music", music);
        //PlayerPrefsX.SetBool("Sound", sound);
        if (sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        if (music == true)
        {
            music = false;
        }
        else
        {
            music = true;
        }
        PlayerPrefsX.SetBool("Music", music);
    }

    public void Sound()
    {
        //if (sound == true)
        //{
        //    SoundManager.PlaySound("ButtonSound");
        //    sound = false;
        //    //soundButton.sprite = MusicAndSound_Off;
        //}
        //else
        //{
        //    sound = true;
        //    //soundButton.sprite = MusicAndSound_On;
        //}
        //if (PlayerPrefs.HasKey("Music"))
        //{
        //    music = PlayerPrefsX.GetBool("Music");
        //}
        //if (PlayerPrefs.HasKey("Sound"))
        //{
        //    sound = PlayerPrefsX.GetBool("Sound");
        //}
        if (sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        if (sound == true)
        {
            sound = false;
        }
        else
        {
            sound = true;
        }
        PlayerPrefsX.SetBool("Sound", sound);
    }

    public void About()
    {
        if (sound == true)
        {
            SoundManager.PlaySound("ButtonSound");
        }
        questionScreenHolder.SetActive(true);
        questionScreen.SetActive(true);
        deathCountText.text = "Death count: " + deathCount;
    }

    public void StatsExit()
    {
        deathCountText.text = "Death Count: " + deathCount;
        questionScreen.SetActive(false);
        questionScreenHolder.SetActive(false);
    }

}
