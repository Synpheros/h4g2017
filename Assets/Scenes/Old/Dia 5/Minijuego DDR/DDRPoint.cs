using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DDRPoint : MonoBehaviour {

    [SerializeField]
    KeyCode key = KeyCode.A;

    [SerializeField]
    GameObject toEmit = null;

    [SerializeField]
    float maxDistance, goodDistance, perfectDistance;

    private DDRController c;

    // Use this for initialization
    void Start ()
    {
        c = GameObject.FindObjectOfType<DDRController>();

    }

    private void OnMouseUpAsButton()
    {
        Activate();
    }

    void Activate()
    {
        var instance = GameObject.Instantiate(toEmit);
        instance.transform.SetParent(this.transform.parent);
        instance.transform.localPosition = this.transform.localPosition;

        var arrows = FindObjectsOfType<DDRArrow>().ToList().FindAll(a => a.targetPoint == this);
        if (arrows.Count > 0)
        {
            var min = arrows.Min(a => (a.transform.localPosition - this.transform.localPosition).magnitude);
            var minArrow = arrows.Find(a => (a.transform.localPosition - this.transform.localPosition).magnitude == min);
            if (minArrow && min < maxDistance)
            {
                Debug.Log(min);
                if (min < perfectDistance)
                {
                    c.Perfect();
                    Debug.Log("Perfect");
                }
                else if (min < goodDistance)
                {
                    c.Good();
                    Debug.Log("Good");
                }
                else if (min < maxDistance)
                {
                    c.Okay();
                    Debug.Log("Max");
                }
                GameObject.DestroyImmediate(minArrow.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(key) == true)
        {
            Activate();
        }
	}
}
