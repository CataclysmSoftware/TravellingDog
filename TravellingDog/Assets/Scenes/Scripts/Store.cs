using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public string back;
    public int coinCount;
    public Sprite tick;
    public Text coinText;
    public Text[] unlockedText;
    public Image[] unlockedImage;
    public Image[] selected;
    public Sprite unlocked;
    public Sprite coin;
    public Button[] button;
    public bool[] status = { false };
    public GameObject questionScreen;
    public GameObject questionScreenHolder;
    private int number;
    private int x;
    public Image[] tickImage;
    public bool[] statusBuy;
    public int numberSelectedSkin = 0;
    public Image backgroundImage;
    public Sprite forestImage;
    public Sprite caveImage;
    public Sprite mountainImage;
    public Sprite cityImage;
    public bool[] levelUNLOCKED;
    public bool sound;

    void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            sound = PlayerPrefsX.GetBool("Sound");
        }

        questionScreen.SetActive(false);
        if (PlayerPrefs.HasKey("LevelUNLOCKED_1"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED_1");
        }

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }

        coinText.text = " " + coinCount;

        if (PlayerPrefs.HasKey("StatusMenu"))
        {
            statusBuy = PlayerPrefsX.GetBoolArray("StatusMenu");
        }
        if (PlayerPrefs.HasKey("NumberSelectedSkinMenu"))
        {
            numberSelectedSkin = PlayerPrefs.GetInt("NumberSelectedSkinMenu");
        }

        statusBuy[0] = true;
        for (int i = 0; i < statusBuy.Length; i++)
        {
            if (statusBuy[i] == true)
            {
                unlockedImage[i].sprite = unlocked;
                unlockedText[i].text = "UNLOCKED";
            }
        }
        tickImage[numberSelectedSkin].sprite = tick;
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
        for (int i = 1; i < status.Length; i++)
        {
            status[i] = false;
            //unlockedText[i].text = "Coming Soon";
        }
    }
    void Update()
    {

        for (int i = 0; i <status.Length; i++)
        {
            if (status[i] == true && unlockedText[i].text != "UNLOCKED" && unlockedText[i].text != "Coming Soon")
            {
           
                int.TryParse(unlockedText[i].text, out x);
                //Debug.Log(x);
                if (x <= coinCount)
                {
                    questionScreenHolder.SetActive(true);
                    questionScreen.SetActive(true);
                    number = i;
                }
            }
            else if (status[i] == true && unlockedText[i].text == "UNLOCKED")
            {
                status[i] = false;
                tickImage[i].sprite = tick;
                numberSelectedSkin = i;
                for (int j = 0; j < status.Length; j++)
                {
                    if (tickImage[j].sprite == tick && j != i)
                    {
                        tickImage[j].sprite = unlocked;
                    }
                }
            }
        }
        PlayerPrefsX.SetBoolArray("Status", statusBuy);
        PlayerPrefs.SetInt("NumberSelectedSkin", numberSelectedSkin);
        PlayerPrefs.SetInt("CoinCount", coinCount);

    }

    public void BackTo()
    {
        //if (sound == true)
        //{
        //    SoundManager.PlaySound("ButtonSound");
        //}
        SceneManager.LoadScene(back);
    }

    public void ButtonStatus_0()
    {
        status[0] = true;

    }
    public void ButtonStatus_1()
    {
        status[1] = true;
    }
    public void ButtonStatus_2()
    {
        status[2] = true;
    }
    public void ButtonStatus_3()
    {
        status[3] = true;
    }
    public void ButtonStatus_4()
    {
        status[4] = true;
    }
    public void ButtonStatus_5()
    {
        status[5] = true;
    }

    public void ButtonStatus_6()
    {
        status[6] = true;
    }

    public void QuestionYes()
    {
        statusBuy[number] = true;
        unlockedImage[number].sprite = unlocked;
        unlockedText[number].text = "UNLOCKED";
        coinCount -= x;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        coinText.text = " " + coinCount;
        status[number] = false;
        questionScreen.SetActive(false);
        questionScreenHolder.SetActive(false);
    }
    public void QuestionNo()
    {
        status[number] = false;
        questionScreen.SetActive(false);
        questionScreenHolder.SetActive(false);
    }



}
