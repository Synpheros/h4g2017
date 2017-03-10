using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : EventManager
{
    public object GameState { get; private set; }

    public override void ReceiveEvent(IGameEvent ev)
    {
        if(ev.Name == "change scene")
        {
            SceneManager.LoadScene((string)ev.getParameter("name"));
        }

        if(ev.Name == "change slide")
        {
            h4g2.GameState.S.setNext(
                (string)ev.getParameter("title"),
                (string)ev.getParameter("subtitle"),
                (string)ev.getParameter("next")
            );
            SceneManager.LoadScene("transition");
        }
    }

    public override void Tick()
    {
    }
}
