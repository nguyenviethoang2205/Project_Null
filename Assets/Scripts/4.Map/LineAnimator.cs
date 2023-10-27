using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LineAnimator : MonoBehaviour
{
    [SerializeField] private float animationDuration = 5f;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(AnimeLine());
    }

    private IEnumerator AnimeLine()
    {
        float startTime = Time.time;
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        Vector3 pos = startPoint;
        while (pos != endPoint)
        {
            float t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPoint, endPoint, t);
            lineRenderer.SetPosition(1, pos);
        }
        yield return null;
    }
}
