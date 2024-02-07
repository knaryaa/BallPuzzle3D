using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDetector : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
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
