using IsoUnity;
using IsoUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageShower : EventedEventManager {
    
    [SerializeField]
    public List<Sprite> images;

    private bool showing = false;
    private float time;

    public GameObject imageHolder;

    public UnityEngine.UI.Image sr;
    private IGameEvent ge;

    [GameEvent(false, false)]
    public void ShowImage(string name)
    {
        var i = images.Find(s => s.name == name);
        if (i)
        {
            ge = Current;
            time = 0;
            imageHolder.SetActive(true);
            sr.sprite = i;
            showing = true;
        }
    }

    // Use this for initialization
    void Start () {
        sr = imageHolder.GetComponent<UnityEngine.UI.Image>();
    }
    private Vector3 velocity = Vector3.zero, color = Vector3.zero;

    // Update is called once per frame
    void Update () {
        if (showing)
        {
            imageHolder.transform.localScale = Vector3.SmoothDamp(imageHolder.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), ref velocity, 5f);
            color = Vector3.SmoothDamp(color, Vector3.one, ref velocity, 2f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, color.x);
            time += Time.deltaTime;
            if(time > 6f)
            {
                Game.main.eventFinished(ge);
                DestroyImmediate(this.gameObject);
            }
        }
	}
}
