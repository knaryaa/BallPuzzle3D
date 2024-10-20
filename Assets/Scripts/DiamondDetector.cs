using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDetector : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Diamond"))
        {
            gameManager.diamondTxt.text = gameManager.diamondCount.ToString();
            collider.gameObject.SetActive(false);
        }
    }
}
