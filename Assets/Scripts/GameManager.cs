using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int diamondCount;

    public TextMeshProUGUI diamondTxt;
    private void Start()
    {
        DiamondCountReset();
    }

    public void DiamondCountReset()
    {
        diamondCount = 0;
        diamondTxt.text = diamondCount.ToString();
    }
}
