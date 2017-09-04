using UnityEngine;

public class CustomParticle : MonoBehaviour {

    [SerializeField]
    float timeToLive;
    float progress;

    [SerializeField]
    AnimationCurve scaleOverTime;

    Vector3 originalScale;

    Sprite sprite;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        originalScale = this.transform.localScale;
        spriteRenderer = GetComponent< SpriteRenderer > ();
    }

    void Update()
    {
        progress += Time.deltaTime;
        var s = scaleOverTime.Evaluate(progress/ timeToLive);
        this.transform.localScale = new Vector3(originalScale.x*s, originalScale.y*s, originalScale.z*s);

        Color color = spriteRenderer.material.color;
        color.a = 1 - progress / timeToLive;
        spriteRenderer.material.color = color;

        if (progress > timeToLive)
            DestroyImmediate(this.gameObject);
    }
}
