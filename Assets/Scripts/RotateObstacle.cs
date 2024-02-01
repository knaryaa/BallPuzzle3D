using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObstacle : MonoBehaviour
{
    public GameObject rotateObstacle;
    public Vector3 rotateAngle = new Vector3(0,90,0);
    private Vector3 closed;

    private void Start()
    {
        rotateObstacle = GameObject.Find("RotateObstacle");
        closed = new Vector3(0,0,0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball") && rotateObstacle)
        {
            rotateObstacle.transform.DORotate(rotateAngle, 0.2f);
            gameObject.transform.DOLocalMove(closed, 0.2f);
        }
    }
}
