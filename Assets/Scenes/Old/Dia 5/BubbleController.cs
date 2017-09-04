using IsoUnity.Events;
using System.Collections;
using UnityEngine;

public class BubbleController : EventedEventManager {

	public GameObject rightBubble;
	public GameObject leftBubble;
	public GameObject canvas;
	public Shake shake;

	IGameEvent current;
	GameObject currentbubble;

    [GameEvent(true, false)]
    public IEnumerator BubbleRight(string text)
    {
        var g = Instantiate(rightBubble, canvas.transform);
        g.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        //g.transform.SetParent();
        g.transform.localPosition = new Vector3(220f, 44f, 1);
        shake.DoShake();

        yield return WaitForMouseButtonDown(0);
        
        DestroyImmediate(g);
    }

    [GameEvent(true, false)]
    public IEnumerator BubbleLeft(string text)
    {
        var g = Instantiate(leftBubble, canvas.transform);
        g.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        //g.transform.SetParent();
        g.transform.localPosition = new Vector3(-200, 83, 1);
        shake.DoShake();

        yield return WaitForMouseButtonDown(0);

        DestroyImmediate(g);
    }

    [GameEvent(true, false)]
    public IEnumerator Shake()
    {
        shake.DoShake();
        yield return WaitForMouseButtonDown(0);
    }

    public IEnumerator WaitForMouseButtonDown(int mouseButton)
    {
        while (!Input.GetMouseButtonDown(mouseButton))
            yield return null;
    }
}
