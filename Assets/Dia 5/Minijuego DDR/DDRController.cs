using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRController : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Slider progressbar;

    [SerializeField]
    bool started = false;
    public bool finished = false;

    [SerializeField]
    GameObject spawnPos;
    [SerializeField]
    GameObject top;
    /*[SerializeField]
    ScoreBar bar;*/
    [SerializeField] // TOP RIGHT DOWN LEFT
    DDRPoint[] points;
    [SerializeField]
    DDRArrow[] arrows;
    [SerializeField]
    float gameTime;

    [SerializeField]
    GameObject missToEmit = null;
    [SerializeField]
    GameObject okayToEmit = null;
    [SerializeField]
    GameObject goodToEmit = null;
    [SerializeField]
    GameObject perfectToEmit = null;

    [SerializeField]
    AnimationCurve timeToSpawn;

    float lastSpawn = 0;
    float progress = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (started)
        {
            progress += Time.deltaTime;
            progressbar.value = progress / gameTime;

            if (progress - lastSpawn > timeToSpawn.Evaluate(progress / gameTime))
            {
                SpawnArrow(Random.Range(0, 4));
                lastSpawn = progress;
            }

            if(progress > gameTime)
            {
                finished = true;
                started = false;
                foreach (var a in FindObjectsOfType<DDRArrow>())
                {
                    GameObject.DestroyImmediate(a.gameObject);
                }
            }
        }
	}

    private void SpawnArrow(int arrow)
    {
        var go = arrows[arrow];
        var localPosition = points[arrow].transform.localPosition;
        localPosition = new Vector3(localPosition.x, spawnPos.transform.localPosition.y, localPosition.z);

        var instance = GameObject.Instantiate(go);
        go.GetComponent<DDRArrow>().end = top;
        instance.transform.SetParent(this.transform);
        instance.transform.localPosition = localPosition;

        instance.GetComponent<DDRArrow>().end = top;
        instance.GetComponent<DDRArrow>().targetPoint = points[arrow];
    }

    public void Miss()
    {
        var go = GameObject.Instantiate(missToEmit);
        go.transform.localPosition = Vector3.zero;
    }
    public void Okay()
    {
        var go = GameObject.Instantiate(okayToEmit);
        go.transform.localPosition = Vector3.zero;

    }
    public void Good()
    {
        var go = GameObject.Instantiate(goodToEmit);
        go.transform.localPosition = Vector3.zero;

    }
    public void Perfect()
    {
        var go = GameObject.Instantiate(perfectToEmit);
        go.transform.localPosition = Vector3.zero;

    }
}
