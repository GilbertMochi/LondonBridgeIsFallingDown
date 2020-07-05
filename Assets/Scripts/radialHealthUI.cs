using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class radialHealthUI : MonoBehaviour
{
    Image image;
    float imageFill;
    PlayerHealthSystem pls;

    // Use this for initialization
    void Start()
    {
        pls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
        image = this.GetComponent<Image>();
        imageFill = pls.Health;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        imageFill = pls.Health;
        image.fillAmount = imageFill;

        //change colour according to how much life is left
        if (imageFill > .5)//green
        {
            Color32 half = new Color32(0, 255, 65, 255);
            image.color = half;
        }
        else if (imageFill <= .2)//red
        {
            Color32 half = new Color32(229, 0, 0, 255);
            image.color = half;
        }
        else if (imageFill <= .5)//orange
        {
            Color32 half = new Color32(248, 132, 7, 255);
            image.color = half;
        }
    }
}
