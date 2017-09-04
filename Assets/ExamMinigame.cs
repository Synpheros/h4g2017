using IsoUnity.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoUnity.Sequences;
using IsoUnity;

public class ExamMinigame : EventedEventManager {

    public Sequence midGameSequence;
    public Sequence endingSequence;

    public GameObject tosPrefab;

    [System.Serializable]
    public class Phrase
    {
        public enum Origin { Left, Right }

        public int hits;
        public float increase;
        public string phrase;
        public Origin origin;
        public GameObject bubble;
    }

    private float hitAmount;
    public List<Phrase> phrases;
    public List<CharacterPhases> phases;

    public CharacterSpriteManager character;

    [System.Serializable]
    public class CharacterPhases
    {
        public string expression;
        public float value;
    }

    public AnimationCurve colorCurve;
    public Color characterColor;
    public AnimationCurve vibrationCurve;
    public float maxVibration;

    public AnimationCurve spawnCurve;
    public float duration;
    
    public bool Started { get; set; }
    private float elapsed = 0;
    private int launched = 0;

    private bool midSequence = false;
    
    [GameEvent(true, false)]
	public void StartExam()
    {
        Started = true;
    }

	void Update () {
        if (Started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var go = GameObject.Instantiate(tosPrefab, transform);
                go.transform.localPosition = new Vector3(0, -10, 0);
                var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse.z = 0;
                go.GetComponent<Tos>().Direction = mouse - go.transform.position;
            }

            elapsed += Time.deltaTime;

            var percent = elapsed / duration;

            var phrasesPercent = spawnCurve.Evaluate(percent);
            int shouldBeAt = Mathf.FloorToInt(phrasesPercent * phrases.Count);

            while (launched < shouldBeAt)
            {
                CreateBubble(phrases[launched]);
                launched++;
            }

            character.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, characterColor, colorCurve.Evaluate(hitAmount));
            var vibration = vibrationCurve.Evaluate(hitAmount);
            character.transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-vibration, vibration));
            character.transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-vibration, vibration));

            var phase = phases.Find(p => p.value < hitAmount);
            character.ChangeSprite(phase.expression);


            if (percent >= 0.5f && ! midSequence)
            {
                midSequence = true;
                StartCoroutine(DoDialog(midGameSequence, true));
            }
            else if(percent >= 1.05f)
            {
                StartCoroutine(DoDialog(endingSequence, false));
            }
        }
	}

    void CreateBubble(Phrase phrase)
    {
        var go = GameObject.Instantiate(phrase.bubble, transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.Translate(new Vector3((phrase.origin == Phrase.Origin.Left) ? -20 : 20, UnityEngine.Random.Range(-2f, 4f), 0));
        var missile = go.GetComponent<BubbleMissile>();
        missile.Hits = phrase.hits;
        missile.Target = character;
        missile.SpeedMod = spawnCurve.Evaluate(elapsed/duration);
        missile.Damage = phrase.increase;

        missile.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "\n"+phrase.phrase+"\n";
    }

    public void Hit(float value)
    {
        hitAmount = Mathf.Clamp01(hitAmount + value);
    }

    private IEnumerator DoDialog(Sequence sequence, bool restartAfter)
    {
        Started = false;

        var ge = new GameEvent("start sequence");
        ge.setParameter("sequence", sequence);
        ge.setParameter("synchronous", true);
        Game.main.enqueueEvent(ge);

        yield return new WaitForEventFinished(ge);

        if(restartAfter)
            Started = true;
    }
}
