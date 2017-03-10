using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInitializer : EventManager {

    [SerializeField]
    BallGenerator minigameController;

    bool started = false;
    public override void ReceiveEvent(IGameEvent ev)
    {
        if(ev.Name == "start futbol")
        {
            started = true;
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
        minigameController.gameObject.SetActive(started);

    }
}
