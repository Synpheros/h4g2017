using IsoUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : EventedEventManager
{
    public object GameState { get; private set; }

    [GameEvent(true, false)]
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    [GameEvent(true, false)]
    public void ChangeSlide(string title, string subtitle, string next)
    {
        h4g2.GameState.S.setNext(title, subtitle, next);
        SceneManager.LoadScene("transition");
    }
}
