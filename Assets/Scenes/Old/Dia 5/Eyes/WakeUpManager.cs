using IsoUnity;
using IsoUnity.Events;
using IsoUnity.Sequences;
using UnityEngine;

public class WakeUpManager : MonoBehaviour {

	public Eyelids eyelidsObject;

	public float wakeUpSeconds = 6;

	private float offset = 1;

	private float dTime = 0;

	[SerializeField]
	public Sequence sequence;

	/// <summary>
	/// state = 0 --> Wake up
	/// </summary>
	private int state;

	// Use this for initialization
	void Start () {
		state = 0;
	}

	// Update is called once per frame
	void Update()
	{
		//Wake up (Move eyelids)
		if (state == 0 && dTime >= offset)
		{
			state = 1;
			eyelidsObject.wakeUp(wakeUpSeconds);
			dTime = 0;
		} else if (state == 1 && dTime > wakeUpSeconds)
		{
			state = 2;
			Launch();
		}

		dTime += Time.deltaTime;
	}

	private void Launch()
	{

		// Remote start
		GameEvent ge = new GameEvent("start sequence", new System.Collections.Generic.Dictionary<string, object>() {
			{ "sequence", sequence }
		});
		Game.main.enqueueEvent(ge);
		
	}
}
