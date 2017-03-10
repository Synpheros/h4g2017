using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : EventManager
{
    public override void ReceiveEvent(IGameEvent ev)
    {
        if(ev.Name == "change scene")
        {
            SceneManager.LoadScene((string)ev.getParameter("name"));
        }
    }

    public override void Tick()
    {
    }
}
