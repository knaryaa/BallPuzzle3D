using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;

    private void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / 880; 
        Vector3 minValue = new Vector3(-1220, 0, 0);
        Vector3 maxValue = new Vector3(540, 0, 0);
        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0)
            {
                newLocation += new Vector3(-880, 0, 0);
                //panelLocation = Vector3.Min(newLocation,minValue);
            }
            else if (percentage < 0)
            {
                newLocation += new Vector3(880, 0, 0);
                //panelLocation = Vector3.Max(newLocation,maxValue);
            }
            
            if (newLocation.x < minValue.x)
            {
                newLocation.x = minValue.x;
            }
            else if (newLocation.x > maxValue.x)
            {
                newLocation.x = maxValue.x;
            }
            else
            {
                panelLocation = newLocation;
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            
            
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }

        IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
        {
            float t = 0;
            while (t <= 1.0)
            {
                t += Time.deltaTime / seconds;
                transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
        }
    }
}
