using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble2 : MonoBehaviour {

	bool faded = false;

	// Use this for initialization
	void Start() { 
	}
	
	// Update is called once per frame
	void Update () {
		if (!faded)
		{
			faded = true;
			fade_in();
		}
	}

	public void fade_in()
	{
		Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();

		for (int i = 0; i < graphics.Length; ++i)
		{
			if (graphics[i].GetComponent<Text>() != null || graphics[i].GetComponent<Image>() != null)
				graphics[i].CrossFadeAlpha(1f, 0.25f, false);
		}
	}

	public void fade_out_instant()
	{
		Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();

		for (int i = 0; i < graphics.Length; ++i)
		{
			graphics[i].CrossFadeAlpha(0f, 0f, false);
		}
	}
}
