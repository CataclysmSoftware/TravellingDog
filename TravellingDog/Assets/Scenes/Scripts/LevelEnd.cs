 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelEnd : MonoBehaviour
{
    public int numberLevelUnlocked;
    public string levelToLoad;
    private PlayerControl thePlayer;
    private CameraControl theCamera;
    private LevelManager theLevelManager;
    public float waitToMove;
    public float waitToLoad;
    private bool movePlayer;
    public Sprite flagOpen;
    public Sprite flagClose;

    private int x;

    public bool[] levelUNLOCKED;

    private GameObject levelEndScreen;

    public int coinThisLevel =0;


    void Start()
    {
        thePlayer = FindObjectOfType<PlayerControl>();
        theCamera = FindObjectOfType<CameraControl>();
        theLevelManager = FindObjectOfType<LevelManager>();
        levelEndScreen = GameObject.Find("Starter pack/Canvas/LevelEndScreen");
        theLevelManager.levelUNLOCKED[1] = true;
    }

    void Update()
    {
        if(movePlayer)
        {
            thePlayer.myRigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
        }

        if(PlayerPrefs.HasKey("LevelUNLOCKED_1"))
        {
            levelUNLOCKED = PlayerPrefsX.GetBoolArray("LevelUNLOCKED_1");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            coinThisLevel = theLevelManager.coinThisLevel;
            levelUNLOCKED[numberLevelUnlocked] = true;
            PlayerPrefsX.SetBoolArray("LevelUNLOCKED", levelUNLOCKED);
            PlayerPrefsX.SetBoolArray("LevelUNLOCKED_1", levelUNLOCKED);
            StartCoroutine(LevelEndCo());
        }
    }
    
    public IEnumerator LevelEndCo()
    {

        thePlayer.canMove = false;
        theCamera.followTarget = false;
        theLevelManager.invincible  = true;

        thePlayer.myRigidbody.velocity = Vector3.zero;

        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
   

        yield return new WaitForSecondsRealtime(waitToMove);

        movePlayer = true;

        yield return new WaitForSecondsRealtime(waitToLoad);

        levelEndScreen.SetActive(true);

    }

    public void LevelEndNextLevel()
    {
        int.TryParse(levelToLoad, out x);

        SceneManager.LoadScene(levelToLoad);
    }

}
