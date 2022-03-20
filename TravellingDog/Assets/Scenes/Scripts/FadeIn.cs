using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    public float fadeTime;

     public Image blackScreen;

    void Start()
    {
        blackScreen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        blackScreen.CrossFadeAlpha(0.0f, fadeTime, false);

        if(blackScreen.color.a == 0) 
        {
            blackScreen.gameObject.SetActive(false);
        }
    }
}
