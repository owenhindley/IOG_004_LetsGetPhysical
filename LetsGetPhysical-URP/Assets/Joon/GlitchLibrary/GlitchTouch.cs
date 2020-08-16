using UnityEngine;

public class GlitchTouch
{
    public float deltaTime;
    public int tapCount;
    public TouchPhase phase;
    public Vector2 deltaPosition;
    public int fingerId;
    public Vector2 position;
    public Vector2 rawPosition;

    public GlitchTouch()
    {

    }
    
    public GlitchTouch(Touch touch)
    {
        this.deltaPosition = touch.deltaPosition;
        this.deltaTime = touch.deltaTime;
        this.tapCount = touch.tapCount;
        this.phase = touch.phase;
        this.fingerId = touch.fingerId;
        this.position = touch.position;
        this.rawPosition = touch.rawPosition;
    }

    public GlitchTouch(float deltaTime, int tapCount, TouchPhase phase, Vector2 deltaPosition, int fingerId, Vector2 position, Vector2 rawPosition)
    {
        this.deltaPosition = deltaPosition;
        this.deltaTime = deltaTime;
        this.tapCount = tapCount;
        this.phase = phase;
        this.fingerId = fingerId;
        this.position = position;
        this.rawPosition = rawPosition;
    }

}