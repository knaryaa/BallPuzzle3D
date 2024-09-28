using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconOnOffManager : MonoBehaviour
{
    [SerializeField] private Sprite openIcon;
    [SerializeField] private Sprite closedIcon;

    private Image iconImg;

    [SerializeField] private bool defaultIconState;
    private bool iconState;

    private void Start()
    {
        iconImg = GetComponent<Image>();

        iconImg.sprite = (defaultIconState) ? openIcon : closedIcon;
    }

    public void IconOnOffFNC()
    {
        if (!iconImg || !openIcon || !closedIcon)
        {
            return;
        }

        if (iconState)
        {
            iconImg.sprite = openIcon;
            iconState = false;
        }
        else
        {
            iconImg.sprite = closedIcon;
            iconState = true;
        }
        //iconImg.sprite = (iconState) ? openIcon : closedIcon;
    }
}
