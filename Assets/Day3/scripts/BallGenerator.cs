using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour {

	public GameObject ball;

	[SerializeField]
	AnimationCurve timeToSpawn;

	public float gameTime;

	float lastSpawn = 0;
	float progress = 0;

	public UnityEngine.UI.Text hit;
	public UnityEngine.UI.Text fail;

	int failCount = 0;
	int hitCount = 0;

	// Use this for initialization
	void Start () {
		hit.text = ""+0;
		fail.text = ""+0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		progress += Time.deltaTime;

		if (progress - lastSpawn > timeToSpawn.Evaluate(progress / gameTime))
		{
			lastSpawn = progress;
			
			GenerateBall(new Vector2(Random.Range(-20, 20), 2));
		}
	}

	void GenerateBall(Vector2 direction)
	{
		GameObject obj = Instantiate(ball, new Vector2(0,0), Quaternion.identity);
		Ball script = obj.GetComponent<Ball>();
		script.generator = this;
		script.ApplicateForce(direction);
	}

	public void Hit()
	{
		hitCount++;
		hit.text = "" + hitCount;
	}

	public void Fail()
	{
		failCount++;
		fail.text = "" + failCount;
	}
}
