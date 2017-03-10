using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRArrow : MonoBehaviour {
    
    public GameObject end;
    public DDRPoint targetPoint;

    [SerializeField]
    float speed;

    private DDRController c;

	// Use this for initialization
	void Start () {
        c = GameObject.FindObjectOfType<DDRController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        if(transform.localPosition.y > end.transform.localPosition.y)
        {
            c.Miss();
            DestroyImmediate(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if(other.gameObject == end)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
