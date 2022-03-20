using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollStore : MonoBehaviour
{
    static int buttonSize = 20;
    static int numberOfButtons = 6;
    private int mValue = buttonSize * numberOfButtons;
    public RectTransform cont;

    void Update()
    {
        if (cont.offsetMax.x < 0)
        {
            cont.offsetMax = new Vector2();
            cont.offsetMin = new Vector2();
        }
        if (cont.offsetMax.y > (numberOfButtons * buttonSize) - buttonSize)
        {
            cont.offsetMax = new Vector2((numberOfButtons * buttonSize) - buttonSize,0);
            cont.offsetMin = new Vector2();
        }
    }

}
