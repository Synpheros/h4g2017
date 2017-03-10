using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterSpriteManager : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    List<CharacterSprite> expressions;

    [System.Serializable]
    public class CharacterSprite
    {
        public string expression;
        public Sprite sprite;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void ChangeSprite(string expression)
    {
        spriteRenderer.sprite = expressions.Find(e => e.expression == expression).sprite;
    }
}
