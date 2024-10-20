using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;

public class DiamondController : MonoBehaviour
{
    private readonly float time = 1f;
    private float timer = 0f;
    private bool anim = true;
    private readonly Vector3 updateScale = new Vector3(0.1f, 0.1f, 0.1f);
    private readonly Vector3 updateRotation = new Vector3(0f, 30f, 0f);

    

    private void Start()
    {
        DOTween.SetTweensCapacity(2000, 100);
    }

    private void Update()
    {
        //StartCoroutine(DiamondAnim());
        
        DiamondAnimation();
        BounceTimer();
    }

    private void DiamondAnimation()
    {
        if (anim)
        {
            transform.DOScale(transform.localScale + updateScale, time).SetEase(Ease.Flash);
        }
        else
        {
            transform.DOScale(transform.localScale - updateScale, time).SetEase(Ease.Flash);
        }
        transform.DORotate(transform.localRotation.eulerAngles + updateRotation, time);

    }

    private void BounceTimer()
    {
        timer += Time.deltaTime;
        int count = (int)timer;
        if (count % 2 == 0)
        {
            anim = true;
        }
        else
        {
            anim = false;
        }
    }

    IEnumerator DiamondAnim()
    {
        anim = true;
        yield return new WaitForSecondsRealtime(5f);
        anim = false;
        yield return new WaitForSecondsRealtime(5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            this.enabled = false;
        }
    }
}