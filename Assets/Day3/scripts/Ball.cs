using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float maxSize;
	public float growFactor;
	public float waitTime;

	public bool clickable = false;
	bool clicked = false;

	public BallGenerator generator;

	void Start()
	{
		StartCoroutine(Scale());
		this.GetComponent<SpriteRenderer>().color = Color.gray;
	}

	IEnumerator Scale()
	{
		float timer = 0;

		while (true) // this could also be a condition indicating "alive or dead"
		{
			while (maxSize > transform.localScale.x)
			{
				timer += Time.deltaTime;
				transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
				yield return null;
			}
			// reset the timer

			yield return new WaitForSeconds(waitTime);

			timer = 0;
			while (1 < transform.localScale.x)
			{
				timer += Time.deltaTime;
				transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
				yield return null;
			}

			timer = 0;
			yield return new WaitForSeconds(waitTime);
		}
	}

	public void ApplicateForce(Vector2 force)
	{
		this.gameObject.GetComponent<ConstantForce2D>().force = force;
	}

	private void Update()
	{
		if (!clickable && this.transform.localScale.x > maxSize * 0.3)
		{
			this.GetComponent<SpriteRenderer>().color = Color.white;
			this.clickable = true;
		}
	}

	private void OnMouseUp()
	{
		if (clickable)
		{
			generator.Hit();
			clicked = true;
			Destroy(this.gameObject);
		}
	}

	private void OnBecameInvisible()
	{
		if (!clicked)
		{
			generator.Fail();
		}
		Destroy(this.gameObject);
	}
}
