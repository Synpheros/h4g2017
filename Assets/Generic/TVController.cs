using IsoUnity;
using IsoUnity.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVController : EventedEventManager {

    public DDRController ddr;
    public GameObject whitenoise, futbol;

    private void Update()
    {
        if (ddr.finished)
        {
            StartCoroutine(ChangeChannel());
        }
    }

    IGameEvent ge;
    [GameEvent(false, false)]
    public IEnumerator StartDDR()
    {
        ge = Current;
        yield return new WaitForSeconds(0.5f);
        ddr.GetComponent<Animator>().SetBool("start", true);
    }

    public IEnumerator ChangeChannel()
    {
        whitenoise.SetActive(true);
        yield return new WaitForSeconds(1);
        futbol.SetActive(true);
        yield return new WaitForSeconds(1);

        Game.main.eventFinished(ge);
    }
}
