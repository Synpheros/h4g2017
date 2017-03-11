using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : EventManager {

    public Animator animator;

    private IGameEvent toFinish;

    public override void ReceiveEvent(IGameEvent ev)
    {
        if(ev.Name == "animate")
        {
            toFinish = ev;
            animator.Play((string)ev.getParameter("animation"));
        }
    }

    public override void Tick()
    {
    }
    

    public void Finish()
    {
        Game.main.eventFinished(toFinish);
    }
}
