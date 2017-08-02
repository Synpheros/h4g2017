using IsoUnity;
using IsoUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInitializer : EventedEventManager {

    [SerializeField]
    BallGenerator minigameController;

    private IGameEvent start;

    bool started = false;

    [GameEvent(false, false)]
    public void StartFutbol()
    {
        started = true;
        start = Current;
    }
	
	void Update () {
        minigameController.gameObject.SetActive(started);
    }

    public void End()
    {
        started = false;
        Game.main.eventFinished(start);
    }
}
