using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMatOnPress : MonoBehaviour
{
    [SerializeField] private Material pressedMat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            gameObject.GetComponent<MeshRenderer>().material = pressedMat;
        }
    }
}
