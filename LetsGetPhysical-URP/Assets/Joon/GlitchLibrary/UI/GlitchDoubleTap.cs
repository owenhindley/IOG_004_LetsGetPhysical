using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;

public class GlitchDoubleTap : MonoBehaviour, IPointerClickHandler
{
    public const float MAXALLOWEDTIMEFORDOUBLETAP = 0.4f;
    public int tap;
    public float counter;
    public UnityEvent OnDoubleTap;
    public Action OnDoubleTapAction;

    public void Update()
    {
        if (tap == 1)
        {
            counter += Time.deltaTime;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tap++;
        
        if (tap > 1)
        {
            if (counter < MAXALLOWEDTIMEFORDOUBLETAP)
            {
                if (OnDoubleTap != null) OnDoubleTap.Invoke();
                if (OnDoubleTapAction != null) OnDoubleTapAction.Invoke();
            }
            tap = 0;
            counter = 0;
        }
    }
}