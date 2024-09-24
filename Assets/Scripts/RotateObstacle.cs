using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObstacle : MonoBehaviour
{
    [SerializeField] Transform rotateObstacle;
    [SerializeField] GameObject rotateButton;
    public Vector3 rotateAngle = new Vector3(0,90,0);
    private Vector3 closed;

    private void Start()
    {
        closed = new Vector3(0,0,0);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball") && rotateObstacle)
        {
            rotateObstacle.DORotate(rotateAngle, 0.2f);
            rotateButton.transform.DOLocalMove(closed, 0.2f);
        }
    }
}
