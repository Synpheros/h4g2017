using IsoUnity;
using IsoUnity.Events;
using IsoUnity.Sequences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortadaController : EventedEventManager {

    public Sequence playSequence;
    public Sequence creditsSequence;

    bool play = false;
	public void Play()
    {
        if (play)
            return;

        play = true;

        Game.main.enqueueEvent(new GameEvent("start sequence", new Dictionary<string, object>()
        {
            {"sequence", playSequence}
        }));
    }

    float buttonsAlpha = 1;
    float speed = 3f;

    [GameEvent(true, false)]
    public IEnumerator HideButtons()
    {

        var cgs = GetComponentsInChildren<CanvasGroup>();

        while(buttonsAlpha > 0)
        {
            buttonsAlpha -= Time.deltaTime * speed;
            foreach(var cg in cgs)
            {
                cg.alpha = buttonsAlpha;
            }
            yield return null;
        }
    }

    public void Credits()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
