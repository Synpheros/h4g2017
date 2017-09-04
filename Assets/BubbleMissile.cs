using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMissile : MonoBehaviour {

    public float maxDistance = 1;
    public float speed = 5;

    public int Hits { get; internal set; }
    public CharacterSpriteManager Target { get; internal set; }
    public float SpeedMod { get; internal set; }
    public float Damage { get; internal set; }


    public float fadingSpeed;
    private bool fading;
    private CanvasGroup cg;

    public void Coughted()
    {
        cg.alpha *= ((Hits-1f)/((float)Hits));
        Hits = Mathf.Max(0, Hits - 1);
        if (Hits == 0)
        {
            fading = true;
        }
    }

    private void Start()
    {
        cg = GetComponentInChildren<CanvasGroup>();
    }

    // Update is called once per frame
    void Update () {
        
        if (Target)
        {
            var vector = (this.Target.transform.position + new Vector3(0, 4, 0)) - this.transform.position;
            this.transform.position += vector.normalized * speed * Time.deltaTime * Mathf.Sqrt(Mathf.Sqrt(SpeedMod));
            if (fading)
            {
                cg.alpha -= fadingSpeed * Time.deltaTime;

                if (cg.alpha <= 0)
                {
                    DestroyImmediate(this.gameObject);
                }
            }
            else if (vector.magnitude < maxDistance)
            {
                FindObjectOfType<ExamMinigame>().Hit(Damage);
                fading = true;
            }
        }
		
	}
}
