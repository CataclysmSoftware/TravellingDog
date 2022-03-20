using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// PlayerControl -> thePlayer = Playerul
    /// </summary>
    public PlayerControl thePlayer;

    /// <summary>
    /// float -> waitToRespawn = Timpul pe care trebuie sa il astepte playerul dupa ce moare sa se respawneze
    /// </summary>
    public float waitToRespawn;

    /// <summary>
    /// int -> coinCount = Numarul de coins pe care il are playerul
    /// </summary>
    public int coinCount = 0;

    /// <summary>
    /// Text -> coinText = numarul de coins a playeruli
    /// </summary>
    public Text coinText;

    /// <summary>
    /// Image -> heart1 = heart2 = heart3 = imaginea inimilor
    /// </summary>
    public Image heart1, heart2, heart3;

    /// <summary>
    /// Sprite -> heartFull = hearthHalf = heartEmpty = statusul inimilor (plin, jumate, gol)
    /// </summary>
    public Sprite heartFull, hearthHalf, heartEmpty; 

    /// <summary>
    /// int -> maxHealth = viata maxima pe care o poate avea playerul
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// int -> heatlhCount = viata pe care o are playerul intr-un moment
    /// </summary>
    public int heatlhCount;

    /// <summary>
    /// bool -> respawning = statusul de respawn
    /// </summary>
    private bool respawning;

    /// <summary>
    /// bool -> invincible = statusul de invincibilitate (true = invincibil, false = nu e invincibil)
    /// </summary>
    public bool invincible;

    /// <summary>
    /// ResetOnRespawn ARRAY -> objectToReset = obiectele ce vor fi respawnate la inceputul nivelului
    /// </summary>
    public ResetOnRespawn[] objectToReset;

    /// <summary>
    /// GameObject -> gameOverScreen = ecranul de game over
    /// </summary>
    public GameObject gameOverScreen;

    /// <summary>
    /// string -> mainMenu = numele scenei de Meniu
    /// </summary>
    public string mainMenu;

    /// <summary>
    /// string -> levelSelect = numele scenei de Level Select
    /// </summary>
    public string levelSelect;

    /// <summary>
    /// bool ARRAY -> levelUNLOCKED = nivelele ce sunt deblocate
    /// </summary>
    public bool[] levelUNLOCKED;

    /// <summary>
    /// AudioSource -> levelMusic = coinSound = jumpSound = buttonSound = sunete
    /// </summary>
    public AudioSource levelMusic, coinSound, jumpSound, buttonSound;

    /// <summary>
    /// bool -> music = sound = on/off muzica (true = muzica/sonorul este deschis, false = nu este)
    /// </summary>
    public bool music=true, sound = true;

    /// <summary>
    /// int -> deathCount = numarul total de cate ori a murit playerul
    /// </summary>
    public int deathCount;


    public Text levelText;

    public float startTime;

    public Text invincibleTime;

    //revive
    public GameObject reviveScreen;
    public bool reviveStatus = false;

    public float rewardStartTime;

    private bool statusWatched;     // true -> reward ad-ul a fost vizionat pana la final, false -> in caz contrar

    public int coinThisLevel;

    private int typeRewardLevelManager;


    private LevelEndCanvas theLevelEndCanvas;

    public Text coinThisLevelText;


    void Start()
    {
        InitializeRewardStart();
        LoadStart();
        MusicSoundStart();

        thePlayer = FindObjectOfType<PlayerControl>();

        levelUNLOCKED[1] = true;

        heatlhCount = maxHealth;

        objectToReset = FindObjectsOfType<ResetOnRespawn>();

        coinText.text = " " + coinCount;

        NumberOfLevelStart();

        startTime = Time.time;
        invincibleTime.gameObject.SetActive(false);

        coinThisLevel = 0;

        theLevelEndCanvas = FindObjectOfType<LevelEndCanvas>();
    }

    void Update()
    {
        MusicSoundUpdate();
        //PlayerPrefs.SetInt("CoinCount", coinCount);

        if (heatlhCount <= 0 && !respawning)
        {
            thePlayer.gameObject.SetActive(false);
            reviveScreen.SetActive(true);
            respawning = true;
            levelMusic.Stop();
        }

        if (invincible == true)
        {
            float t = Time.time - startTime;
            string second = (6-(int)t%60).ToString();
            invincibleTime.text = "Invincible 0:" + second;
            if(second=="0")
            {
                invincible = false;
                invincibleTime.gameObject.SetActive(false);
            }
        }

        coinThisLevelText.text = "Coins: " + coinThisLevel;
    }

    /// <summary>
    /// Functie Update ce se ocupa de muzica-sunete
    /// </summary>
    private void MusicSoundUpdate()
    {
        levelMusic.volume = 0.21f;
        coinSound.volume = 0.3f;
        jumpSound.volume = 0.3f;
        buttonSound.volume = 0.3f;
        AudioListener.volume = 0.17f;

        if (PlayerPrefs.HasKey("Music"))
        {
            music = PlayerPrefsX.GetBool("Music");
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            sound = PlayerPrefsX.GetBool("Sound");
        }

        if (music == false)
        {
            levelMusic.Pause();
        }
        else
        {
            levelMusic.UnPause();
        }
    }

    /// <summary>
    /// Functie ce atribuie textului levelText numarul nivelului actual
    /// </summary>
    private void NumberOfLevelStart()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        if (int.TryParse(m_Scene.name.Substring(m_Scene.name.Length - 2), out _))
        {
            levelText.text = "Level " + m_Scene.name.Substring(m_Scene.name.Length - 2);
        }
        else
        {
            levelText.text = "Level " + m_Scene.name.Substring(m_Scene.name.Length - 1);
        }
    }

    /// <summary>
    /// Functie Start ce initializeaza sunetul
    /// </summary>
    private void MusicSoundStart()
    {
        levelMusic.Play(0);
        levelMusic.volume = 0.21f;
        coinSound.volume = 0.3f;
        jumpSound.volume = 0.3f;
        buttonSound.volume = 0.3f;

        AudioListener.volume = 0.17f;

        if (music == false)
        {
            levelMusic.Pause();
        }
        else
        {
            levelMusic.UnPause();
        }
    }

    /// <summary>
    /// Functie Start ce incarca(load) diferite varabile
    /// </summary>
    private void LoadStart()
    {
        if (PlayerPrefs.HasKey("DeathCount"))
        {
            deathCount = PlayerPrefs.GetInt("DeathCount");
        }

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = coinCount + PlayerPrefs.GetInt("CoinCount");
        }

        if (PlayerPrefs.HasKey("LevelUNLOCKED"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED");
            PlayerPrefsX.SetBoolArray("LevelUNLOCKED_1", levelUNLOCKED);
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

    /// <summary>
    /// Functie ce se ocupa de procesul de respawnare a player-ului
    /// <br>rewardStatus = true -> playerul va fi respawnat la ultimul checkpoint</br>
    /// <br>rewardStatus = false -> playerul va fi respawnat la inceputul nivelului</br>
    /// </summary>
    public IEnumerator RespawnCo(bool rewardStatus)
    {
        //Debug.Log("DEBUG: RespawnCo: " + rewardStatus);
        thePlayer.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(waitToRespawn);

        heatlhCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

        //coinCount = PlayerPrefs.GetInt("CoinCount");
        //coinText.text = " " + coinCount;

        if (rewardStatus == false)
        {
            thePlayer.transform.position = thePlayer.respawnPosition;
        }
        else
        {
            reviveScreen.SetActive(false);
            thePlayer.transform.position = thePlayer.respawnPositionCheckpoint;
        }
        thePlayer.gameObject.SetActive(true);

        coinThisLevel = 0;
        for (int i=0; i< objectToReset.Length; i++)
        {
            objectToReset[i].gameObject.SetActive(true);
            objectToReset[i].ResetObject();
        }
    }

    /// <summary>
    /// Functie ce se ocupa de momentul in care player-ul moare
    /// </summary>
    public void Died()
    {
        deathCount += 1;
        PlayerPrefs.SetInt("DeathCount", deathCount);
        respawning = true;
        levelMusic.Stop();
    }

    /// <summary>
    /// Functie ce se ocupa de coins
    /// <br>coinToAdd -> cati coins va primi playerul</br>
    /// </summary>
    public void AddCoins(int coinToAdd)
    {
        coinThisLevel += coinToAdd;
        coinCount += coinToAdd;
        coinText.text = " " + coinCount;
        if(sound == true)
            SoundManager.PlaySound("Coin 3");
    }

    /// <summary>
    /// Functie ce se ocupa de damage-ul pe care il primeste player-ul
    /// <br>damgeToTake -> cat damage va primi player-ul</br>
    /// </summary>
    public void HurtPlayer(int damgeToTake)
    {
        if(!invincible)
        {
            heatlhCount -= damgeToTake;
            UpdateHeartMeter();
            if(heatlhCount >=1)
            {
                thePlayer.Knockback();
            }
            if (sound == true)
                jumpSound.Play();
        }
        if(damgeToTake==100)
        {
            invincible = false;
            invincibleTime.gameObject.SetActive(false);
            heatlhCount -= damgeToTake;
            UpdateHeartMeter();
            if (heatlhCount >= 1)
            {
                thePlayer.Knockback();
            }
            if (sound == true)
                jumpSound.Play();
        }
    }

    /// <summary>
    /// Functie ce face player-ul invincibil
    /// </summary>
    public void Invincible()
    {
        startTime = Time.time;
        invincible = true;
        invincibleTime.gameObject.SetActive(true);
    }

    /// <summary>
    /// Functie ce adauga viata player-ului
    /// <br>healthToGive -> cata viata va primi player-ul</br>
    /// </summary>
    public void GiveHealth(int healthToGive)
    {
        heatlhCount += healthToGive;
        if(heatlhCount > maxHealth)
        {
            heatlhCount = maxHealth;
        }
        if (sound == true)
            coinSound.Play();
        UpdateHeartMeter();
    }

    /// <summary>
    /// Functie ce se ocupa de viata player-ului
    /// </summary>
    public void UpdateHeartMeter()
    {
        switch(heatlhCount)
        {
            case 6: 
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
            return;
            case 5:
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = hearthHalf;
            return;
            case 4:
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartEmpty;
            return;
            case 3:
            heart1.sprite = heartFull;
            heart2.sprite = hearthHalf;
            heart3.sprite = heartEmpty;
            return;
            case 2:
            heart1.sprite = heartFull;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            return;
            case 1:
            heart1.sprite = hearthHalf;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            return;
            case 0:
            heart1.sprite = heartEmpty;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            return;
            default:
            heart1.sprite = heartEmpty;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            return;
        }
    }

    /// <summary>
    /// Functie atribuita unui buton, daca acesta este apasat utilizatorul va incepe de la inceput nivelul
    /// </summary>
    public void Retry()
    {
        if (sound == true)
            SoundManager.PlaySound("ButtonSound");
        StartCoroutine("RespawnCo",false);
        gameOverScreen.SetActive(false);
        if (music == true)
            levelMusic.Play();
        
    }

    /// <summary>
    /// Functie atribuita unui buton, daca acesta este apasat va incarca scena "levelSelect"
    /// </summary>
    public void LevelSelect()
    {
        if (sound == true)
            buttonSound.Play();
        SceneManager.LoadScene(levelSelect);
    }

    /// <summary>
    /// Functie atribuita unui buton, daca acesta este apasat va incarca scena "mainMenu"
    /// </summary>
    public void MainMenu()
    {
        if (sound == true)
            buttonSound.Play();
        SceneManager.LoadScene(mainMenu);
    }

    /// <summary>
    /// Functie atribuita unui buton, daca acesta este apasat atunci utilizatorul a ales sa nu se uite la reward video
    /// </summary>
    public void QuestionNo()
    {
        deathCount += 1;
        PlayerPrefs.SetInt("DeathCount", deathCount);
        reviveScreen.SetActive(false);
        if (deathCount % 3 == 0)
        {
            //adsInterstitial.Display_Interstitial();
        }
        gameOverScreen.SetActive(true);
        respawning = true;
    }

    /// <summary>
    /// Functie ce verifica daca utilizatorul s-a uitat la reward video 
    /// <br>statusWatchedRespawn = true -> utilizatorul va fi respawnat la ultimul checkpoint</br>
    /// <br>statusWatchedRespawn = false ->  utilizatorul va fi respawnat la inceputul nivelului</br>
    /// </summary>
    public void RewardRespawn(bool statusWatchedRespawn)
    {
        if (statusWatchedRespawn == true)
        {
            //Debug.Log("DEBUG: LevelManager: true");
            reviveScreen.SetActive(false);
            StartCoroutine("RespawnCo", true);
        }
        else
        {
            //Debug.Log("DEBUG: LevelManager: false");
            deathCount += 1;
            PlayerPrefs.SetInt("DeathCount", deathCount);
            reviveScreen.SetActive(false);
            StartCoroutine("RespawnCo", false);
        }
        if (music == true)
            levelMusic.Play();
    }

    /// <summary>
    /// Functie atribuita unui buton, daca acesta este apasat atunci va afisa reward videoul
    /// </summary>
    public void WatchReward()
    {
        reviveScreen.SetActive(false);
        statusWatched = false;
      
        if(SystemInfo.deviceType == DeviceType.Desktop || Debug.isDebugBuild)
        {
            //Debug.Log("DEBUG: WatchReward: Aplicatia ruleaza pe desktop sau este Debug Build");
            RewardRespawn(true);
        }

        //Debug.Log("DEBUG: WatchReward: Exista conexiune la internet");
        DisplayRewardBasedVideo(1);
    }

    /// <summary>
    /// Functie Start ce initializeaza reward video-ul
    /// </summary>
    public void InitializeRewardStart()
    {
        //MobileAds.Initialize(initStatus => { });
        //rewardBasedVideo = RewardBasedVideoAd.Instance;

        //rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        //rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        //rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        //rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        //rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        //rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        //rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        RequestRewardBasedVideo();
    }

    /// <summary>
    /// Functie ce face request pentru reward video
    /// </summary>
    public void RequestRewardBasedVideo()
    {
        string rewardBasedVideo_ID;
        if (Debug.isDebugBuild)
        {
            //Debug.Log("DEBUG: Debug Build - Reward");
            rewardBasedVideo_ID = "ca-app-pub-3940256099942544/5224354917";
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                //Debug.Log("DEBUG: Relase Build - Reward Android");
                rewardBasedVideo_ID = "ca-app-pub-2336744267870247/7463681229";
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //Debug.Log("DEBUG: Relase Build - Reward Iphone");
                rewardBasedVideo_ID = "ca-app-pub-2336744267870247/5991635695";
            }
            else
            {
                rewardBasedVideo_ID = "";
            }

        }
        //AdRequest request = new AdRequest.Builder().Build();
        //this.rewardBasedVideo.LoadAd(request, rewardBasedVideo_ID);
    }

    private LevelEndCanvas test;

    /// <summary>
    /// Functie ce verifica daca reward video-ul este incarcat, daca este il afiseaza
    /// </summary>
    public void DisplayRewardBasedVideo(int typeReward)
    {

        typeRewardLevelManager = typeReward;
        if (typeReward == 1)
        {
            //Debug.Log("DEBUG: DisplayRewardBasedVideo: " + typeReward);
            ////Debug.Log("DEBUG: DisplayRewardBasedVideo: S-a apelat functia de afisare a reward-ului.");
           // if (rewardBasedVideo.IsLoaded())
            //{
            //    ////Debug.Log("DEBUG: DisplayRewardBasedVideo: Reward-ul e incarcat si v-a fi afisat.");
            //    rewardBasedVideo.Show();
           // }
            RequestRewardBasedVideo();
        }
        else if(typeReward == 2)
        {
            //Debug.Log("DEBUG: DisplayRewardBasedVideo: " + typeReward);
            //if (rewardBasedVideo.IsLoaded())
            //{
                ////Debug.Log("DEBUG: DisplayRewardBasedVideo: Reward-ul e incarcat si v-a fi afisat.");
                //rewardBasedVideo.Show();
            //}
            RequestRewardBasedVideo();
        }
    }

    /// <summary>
    /// Called when an ad request has successfully loaded.
    /// </summary>
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //Debug.Log("DEBUG: Ads_Reward: 1. Video Reward-ul a fost incarcat.");
    }

    /// <summary>
    /// Called when an ad is shown.
    /// </summary>
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        //Debug.Log("DEBUG: Ads_Reward: 3. Video Reward-ul a fost deschis.");
    }

    /// <summary>
    /// Called when the ad starts to play.
    /// </summary>
    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        //Debug.Log("DEBUG: Ads_Reward: 4. Video Reward-ul a inceput.");
    }

    /// <summary>
    /// Called when the ad is closed.
    /// </summary>
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //Debug.Log("DEBUG: Ads_Reward: 5. Video Reward-ul a fost inchis.");
        if(typeRewardLevelManager == 1)
        {
            //Debug.Log("DEBUG: Ads_Reward : 5.1. Respawn.");
            RewardRespawn(statusWatched);
        }
        else if(typeRewardLevelManager == 2) 
        {
            if(statusWatched == true)
			{
                //Debug.Log("DEBUG: Ads_Reward : 5.1. S-au dublat banutii.");
                coinCount += coinThisLevel;
                coinThisLevel *= 2;
            }
			else
			{
                //Debug.Log("DEBUG: Ads_Reward : 5.1. NU s-au dublat banutii.");
                coinCount += 0;
                coinThisLevel *= 1;
            }
        }
    }

    /// <summary>
    /// Called when the ad click caused the user to leave the application.
    /// </summary>
    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        statusWatched = false;
        //Debug.Log("DEBUG: Ads_Reward: 7. Utilizatorul a parasit aplicatia.");
    }

    public void OnDestroy()
    {
        //Debug.Log("DEBUG: Ads_Reward: 8. Se distruge.");
        //rewardBasedVideo.OnAdLoaded -= HandleRewardBasedVideoLoaded;
        //rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        //rewardBasedVideo.OnAdOpening -= HandleRewardBasedVideoOpened;
        //rewardBasedVideo.OnAdStarted -= HandleRewardBasedVideoStarted;
        //rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        //rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
        //rewardBasedVideo.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
    }
}
