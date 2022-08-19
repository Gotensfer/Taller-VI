using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp_PidgeonTest : MonoBehaviour
{
	// Fuentes: https://gist.github.com/alialacan/1eddcd107f4a48a46dea17695ca151f2
	// Dev note: Cuando haya más tiempo organizo esto es un script dedicado, mientras, esto hace el pare

	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;

	public bool detectSwipeAfterRelease = false;

	public float SWIPE_THRESHOLD = 20f;

	// Update is called once per frame
	void Update()
	{

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeAfterRelease)
				{
					fingerDownPos = touch.position;
					DetectSwipe();
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended)
			{
				fingerDownPos = touch.position;
				DetectSwipe();
			}
		}
	}

	void DetectSwipe()
	{

		if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
		{
			Debug.Log("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0)
			{
				OnSwipeUp();
			}
			else if (fingerDownPos.y - fingerUpPos.y < 0)
			{
				OnSwipeDown();
			}
			fingerUpPos = fingerDownPos;

		}
		else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
		{
			Debug.Log("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (fingerDownPos.x - fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			fingerUpPos = fingerDownPos;

		}
		else
		{
			Debug.Log("No Swipe Detected!");
		}
	}

	float VerticalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
	}

    #region"SwipeMethods"
    void OnSwipeUp()
	{
		GoUp();
	}

	void OnSwipeDown()
	{
		GoDown();
	}

	void OnSwipeLeft()
	{
		//Do something when swiped left
	}

	void OnSwipeRight()
	{
		//Do something when swiped right
	}
	#endregion

	#region"Pidgeon methods"

	public Rigidbody2D rb;
	public float duration;
	public float sensibility;
	public float speed;
	[SerializeField] Vector2 rawDirection;
	[SerializeField] Vector2 direction;

	[SerializeField] float count;

    private void FixedUpdate()
    {
		rb.velocity = direction * speed;
	}

    public void Initialize()
    {
		rawDirection = Vector2.right;
		direction = Vector2.right;

		rb = GetComponent<Rigidbody2D>();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0;
		rb.AddForce(direction * speed, ForceMode2D.Impulse);

		Invoke(nameof(StopPowerUp), duration);

		print("Woke?");
	}

    void GoUp()
    {
		rawDirection.x -= sensibility;
		rawDirection.y += sensibility;
		direction = rawDirection.normalized;

		count += sensibility;
	}

	void GoDown()
    {
		rawDirection.x += sensibility;
		rawDirection.y -= sensibility;
		direction = rawDirection.normalized;

		count -= sensibility;
	}

	// Llame a este método para parar el PowerUp
	public void StopPowerUp()
    {
		Destroy(this);
    }

    #endregion
}
