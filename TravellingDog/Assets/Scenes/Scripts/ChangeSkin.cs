using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChangeSkin : MonoBehaviour
{
    public AnimatorOverrideController[] dog_skins;
    public int numberSelectedSkin;


    void Start()
    { 
        if (PlayerPrefs.HasKey("NumberSelectedSkinMenu"))
        {
            numberSelectedSkin = PlayerPrefs.GetInt("NumberSelectedSkinMenu");
        }
        GetComponent<Animator>().runtimeAnimatorController = dog_skins[numberSelectedSkin] as RuntimeAnimatorController;
    }

    void Update()
    {
        
    }
}
