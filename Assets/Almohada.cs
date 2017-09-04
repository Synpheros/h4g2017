using IsoUnity;
using IsoUnity.Events;
using IsoUnity.Sequences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almohada : EventedEventManager {

    private Animator animator;
    public Sequence sequence;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    bool clicked = false;
    private void OnMouseUpAsButton()
    {
        clicked = true;
    }


    [GameEvent(true, false)]
    public IEnumerator DoAlmohada()
    {
        animator.SetBool("Bounce", true);

        yield return new WaitUntil(() => clicked);

        if (sequence)
        {
            var ge = new GameEvent("start sequence", new Dictionary<string, object>()
            {
                { "sequence", sequence }
            });
            Game.main.enqueueEvent(ge);
            yield return new WaitForEventFinished(ge);
        }
        else
        {
            animator.SetTrigger("In");

            yield return new WaitForSeconds(2);
        }

    }

}
