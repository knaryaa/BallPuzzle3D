using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine((FadeRoutineFNC()));
    }

    IEnumerator FadeRoutineFNC()
    {
        yield return new WaitForSeconds(0);

        GetComponent<CanvasGroup>().DOFade(0, 0.5f);
    }
}
