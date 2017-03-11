﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelids : MonoBehaviour {

	public AnimationCurve curve;

	public UnityEngine.UI.Image topEyelid;
	public UnityEngine.UI.Image bottomEyelid;

	private float animationSeconds;

	private Vector2 start1;
	private Vector2 start2;

	private Vector2 goal1;
	private Vector2 goal2;

	private bool animate = false;

	private float delta;

	void Start () {
		
	}
	
	void Update () {
		if (animate)
		{
			topEyelid.rectTransform.anchoredPosition = Vector3.Lerp(start1, goal1, curve.Evaluate(delta / animationSeconds));
			bottomEyelid.rectTransform.anchoredPosition = Vector3.Lerp(start2, goal2, curve.Evaluate(delta / animationSeconds));
			delta += Time.deltaTime;
			if (delta >= animationSeconds)
				animate = false;
		}
	}

	public void wakeUp(float seconds)
	{
		delta = 0;

		start1 = topEyelid.rectTransform.anchoredPosition;
		start2 = bottomEyelid.rectTransform.anchoredPosition;
		goal1 = topEyelid.rectTransform.anchoredPosition + new Vector2(0, topEyelid.rectTransform.sizeDelta.y);
		goal2 = bottomEyelid.rectTransform.anchoredPosition - new Vector2(0, bottomEyelid.rectTransform.sizeDelta.y);

		animate = true;
		animationSeconds = seconds;
	}

	

}
