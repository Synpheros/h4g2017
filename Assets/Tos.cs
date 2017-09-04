using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tos : MonoBehaviour {

    public float hitDistance;
    public float speed;
    public Vector3 Direction { get; set; }
    public float fadingSpeed;

    private bool fading;
    private SpriteRenderer sr;

    private float timeAlive = 0;
    public float maxTimeToLive = 5;


	// Use this for initialization
	void Start () {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        timeAlive += Time.deltaTime;
        if(timeAlive > maxTimeToLive)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        transform.LookAt(this.transform.position + Direction);
        transform.position += Direction.normalized * speed * Time.deltaTime;

        if (!fading)
        {
            var pos = transform.position;
            var bubble = GameObject.FindObjectsOfType<BubbleMissile>().ToList().Find(b => (b.transform.position - pos).magnitude < hitDistance);

            if (bubble)
            {
                fading = true;
                bubble.Coughted();
            }
        }
        else
        {
            var c = sr.color;
            c.a -= fadingSpeed * Time.deltaTime;
            sr.color = c;

            if(c.a <= 0)
            {
                DestroyImmediate(this.gameObject);
            }
        }
	}
}
