using UnityEngine;

/*
 * Swipe Input script for Unity by @fonserbc, free to use wherever
 *
 * Attack to a gameObject, check the static booleans to check if a swipe has been detected this frame
 * Eg: if (SwipeInput.swipedRight) ...
 *
 * 
 */

public class InputSwipeDetector : MonoSingleton<InputSwipeDetector> {

	// If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
	public const float MAX_SWIPE_TIME = 0.5f; 
	
	// Factor of the screen width that we consider a swipe
	// 0.17 works well for portrait mode 16:9 phone
	public const float MIN_SWIPE_DISTANCE = 0.07f;

	public bool swipedRight = false;
	public bool swipedLeft = false;
	public bool swipedUp = false;
	public bool swipedDown = false;
	public bool nonSwipeClick = false;
	public bool touchBegan;
	
	
	public bool debugWithArrowKeys = true;

	Vector2 startPos;
	float startTime;

	public void Reset()
	{
		touchBegan = false;
	}

	public void Update()
	{
		swipedRight = false;
		swipedLeft = false;
		swipedUp = false;
		swipedDown = false;
		nonSwipeClick = false;

		if(MouseToGlitchTouchManager.GetTouches().Count > 0)
		{
			var t = MouseToGlitchTouchManager.GetTouches()[0];
			if(t.phase == TouchPhase.Began)
			{
				startPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
				startTime = Time.time;
				touchBegan = true;
			}
			if(t.phase == TouchPhase.Ended && touchBegan)
			{
				touchBegan = false;
				nonSwipeClick = true;
				if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
					return;

				Vector2 endPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);

				Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

				if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
					return;

				if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) { // Horizontal swipe
					if (swipe.x > 0) {
						nonSwipeClick = false;
						swipedRight = true;
					}
					else {
						nonSwipeClick = false;
						swipedLeft = true;
					}
				}
				else { // Vertical swipe
					if (swipe.y > 0) {
						nonSwipeClick = false;
						swipedUp = true;
					}
					else {
						nonSwipeClick = false;
						swipedDown = true;
					}
				}
			}
		}

		if (debugWithArrowKeys) {
			swipedDown = swipedDown || Input.GetKeyDown (KeyCode.DownArrow);
			swipedUp = swipedUp|| Input.GetKeyDown (KeyCode.UpArrow);
			swipedRight = swipedRight || Input.GetKeyDown (KeyCode.RightArrow);
			swipedLeft = swipedLeft || Input.GetKeyDown (KeyCode.LeftArrow);
			nonSwipeClick = nonSwipeClick || Input.GetKey(KeyCode.Space);
		}
	}
}