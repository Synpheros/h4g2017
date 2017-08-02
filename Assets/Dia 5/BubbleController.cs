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
    public IEnumerable BubbleRight(string text)
    {
        var g = Instantiate(rightBubble);
        g.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        g.transform.SetParent(canvas.transform);
        g.transform.localPosition = new Vector3(171.5f, 75.4f, 1);
        shake.DoShake();

        yield return WaitForMouseButtonDown(0);

        DestroyImmediate(currentbubble);
    }

    [GameEvent(true, false)]
    public IEnumerable BubbleLeft(string text)
    {
        var g = Instantiate(rightBubble);
        g.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        g.transform.SetParent(canvas.transform);
        g.transform.localPosition = new Vector3(-609, 180, 1);
        shake.DoShake();

        yield return WaitForMouseButtonDown(0);

        DestroyImmediate(currentbubble);
    }

    [GameEvent(true, false)]
    public IEnumerable Shake()
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
