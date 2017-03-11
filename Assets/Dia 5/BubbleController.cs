using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : EventManager {

	public GameObject rightBubble;
	public GameObject leftBubble;
	public GameObject canvas;
	public Shake shake;

	IGameEvent current;
	GameObject currentbubble;

	public override void ReceiveEvent(IGameEvent ev)
	{
		GameObject g = this.gameObject;
		if (ev.Name == "bubble right")
		{
			current = ev;
			currentbubble = g = Instantiate(rightBubble);
			String text = (String)ev.getParameter("text");
			g.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
			g.transform.SetParent(canvas.transform);
			g.transform.localPosition = new Vector3(171.5f, 75.4f, 1);
			shake.DoShake();

		}
		else if (ev.Name == "bubble left")
		{
			current = ev;
			currentbubble = g = Instantiate(leftBubble);
			String text = (String)ev.getParameter("text");
			g.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
			g.transform.SetParent(canvas.transform);
			g.transform.localPosition = new Vector3(-609, 180, 1);
			shake.DoShake();
		}
		else if (ev.Name == "shake")
		{
			current = ev;
			shake.DoShake();
		}
	}

	public override void Tick()
	{
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			DestroyImmediate(currentbubble);
			Game.main.eventFinished(current);
		}
	}
}
