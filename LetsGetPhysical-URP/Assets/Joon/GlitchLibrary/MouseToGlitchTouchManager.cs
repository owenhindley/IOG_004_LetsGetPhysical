using UnityEngine;
using System.Collections.Generic;

// Mobile touches show up as Input.GetMouseButtonDown(0) but not vice versa. A mouse does not show up in Input.GetTouches.
// use InputHelper.GetTouches if you've implemented touch, and want the mouse to act like a touch...
[ScriptOrder(-1000)] //TODO: figure out better number...
[MonoSingleton(false)]
public class MouseToGlitchTouchManager : MonoSingleton<MouseToGlitchTouchManager>
{
    private static GlitchTouch lastFakeTouch;
    private static float lastFakeTouchTime;
    private static List<GlitchTouch> touches;
    private bool queueRelease;
    public static Vector2[] touchStartPositions;
    public static Vector2[] touchLastPositions;
    public static Vector2[] touchLastLastPositions;

    void Update()
    {
        if(touchStartPositions == null)
            InitVectors();

        touches = new List<GlitchTouch>();

#if !UNITY_STANDALONE
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            touches.Add(new GlitchTouch(touch));
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPositions[touch.fingerId] = touch.position;
                touchLastLastPositions[touch.fingerId] = touch.position;
                touchLastPositions[touch.fingerId] = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
            {
                touchLastLastPositions[touch.fingerId] = touchLastPositions[touch.fingerId];
                touchLastPositions[touch.fingerId] = touch.position;
            }
        }
#endif        

#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL)
        if (lastFakeTouch == null)
            lastFakeTouch = new GlitchTouch();

        if ((Input.GetMouseButtonDown(0)) || (Input.GetMouseButtonUp(0)) || queueRelease)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastFakeTouch.phase = TouchPhase.Began;
                lastFakeTouch.deltaPosition = new Vector2(0, 0);
                lastFakeTouch.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                lastFakeTouch.fingerId = 0;
            }
            
            if (Input.GetMouseButtonUp(0) || queueRelease)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    queueRelease = true;
                }
                else
                {
                    lastFakeTouch.phase = TouchPhase.Ended;
                    Vector2 newPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    lastFakeTouch.deltaPosition = newPosition - lastFakeTouch.position;
                    lastFakeTouch.position = newPosition;
                    lastFakeTouch.fingerId = 0;
                    queueRelease = false;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            lastFakeTouch.phase = TouchPhase.Moved;
            Vector2 newPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            lastFakeTouch.deltaPosition = newPosition - lastFakeTouch.position;
            lastFakeTouch.position = newPosition;
            lastFakeTouch.fingerId = 0;
        }
        else
        {
            lastFakeTouch = null;
        }

        if (lastFakeTouch != null)
        {
            touches.Add(lastFakeTouch);

            if (lastFakeTouch.phase == TouchPhase.Began)
            {
                touchStartPositions[0] = lastFakeTouch.position;
                touchLastLastPositions[lastFakeTouch.fingerId] = lastFakeTouch.position;
                touchLastPositions[lastFakeTouch.fingerId] = lastFakeTouch.position;
            }
            else if (lastFakeTouch.phase == TouchPhase.Moved || lastFakeTouch.phase == TouchPhase.Stationary)
            {
                touchLastLastPositions[lastFakeTouch.fingerId] = touchLastPositions[lastFakeTouch.fingerId];
                touchLastPositions[lastFakeTouch.fingerId] = lastFakeTouch.position;
            }
        }
#endif
    }

    public static List<GlitchTouch> GetTouches()
    {
        if (instance == null)
            instance.Update(); // it's possible that only this static method is touched, and thus the singleton is never initialized. 

        if (touches == null) touches = new List<GlitchTouch>(); // for some reason it returns null on the first run through...

        return touches;      
    }

    public static GlitchTouch GetTouch(int id)
    {
        var touches = GetTouches();
        if(id >= touches.Count) return null;
        return touches[id];      
    }

    static void InitVectors()
    {
        if (touchStartPositions == null) touchStartPositions = new Vector2[11];
        if (touchLastPositions == null) touchLastPositions = new Vector2[11];
        if (touchLastLastPositions == null) touchLastLastPositions = new Vector2[11];
        for (int i = 0; i < 11; i++)
        {
            touchStartPositions[i] = Vector2.zero;
            touchLastPositions[i] = Vector2.zero;
            touchLastLastPositions[i] = Vector2.zero;
        }
    }

    public static Vector2 GetStartPosition(int fingerID)
    {
        if (touchStartPositions == null) InitVectors();
        if (fingerID < 0) fingerID = 0;
        return touchStartPositions[fingerID];
    }

    public static Vector2 GetLastPosition(int fingerID)
    {
        if (touchLastPositions == null) InitVectors();
        if (fingerID < 0) fingerID = 0;
        return touchLastPositions[fingerID];
    }

    public static Vector2 GetLastLastPosition(int fingerID)
    {
        if (touchLastLastPositions == null) InitVectors();
        if (fingerID < 0) fingerID = 0;
        return touchLastLastPositions[fingerID];
    }

    public static Vector2 GetSafeDeltaPosition(int fingerID)
    {
        return Vector2.Scale((GetLastLastPosition(fingerID) - GetLastPosition(fingerID)), new Vector2(1f/Screen.width, 1f / Screen.height));
    }

    public static Vector2 GetLocalDelta()
    {
        var localValue = Vector2.zero;
        if (MouseToGlitchTouchManager.GetTouches().Count > 0)
        {
            GlitchTouch t = MouseToGlitchTouchManager.GetTouches()[0];
            localValue = MouseToGlitchTouchManager.GetSafeDeltaPosition(t.fingerId);

        }
        return localValue;
    }
}
