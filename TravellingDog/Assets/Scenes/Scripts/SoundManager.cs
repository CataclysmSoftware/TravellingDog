using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static AudioClip buttonSound, coinSound, hurtSound;
    static AudioSource audioSrc;
    private static float ClipButtonLenght;
    void Start()
    { 
        buttonSound = Resources.Load<AudioClip>("ButtonSound");
        coinSound = Resources.Load<AudioClip>("Coin 3");
        hurtSound = Resources.Load<AudioClip>("Jump 3");
        audioSrc = GetComponent<AudioSource>();
        ClipButtonLenght = buttonSound.length;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    static IEnumerator WaitTime(string levelName)
    {
        yield return new WaitForSecondsRealtime(ClipButtonLenght);
        SceneManager.LoadScene(levelName); 
    }

    public static void PlaySound(string clip)
    {
        switch(clip){
            case "ButtonSound":
            audioSrc.PlayOneShot(buttonSound, 0.07F); 
            break;
            case "Coin 3":
            audioSrc.PlayOneShot(coinSound);
            break;
            case "Jump 3":
            audioSrc.PlayOneShot(hurtSound);
            break;
        }
    }
    
    public static void LoadScene(string levelName)
    {
        StaticCoroutine.Start(WaitTime(levelName));
    }

}
