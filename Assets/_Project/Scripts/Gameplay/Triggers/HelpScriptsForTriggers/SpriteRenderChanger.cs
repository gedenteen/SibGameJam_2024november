using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _targetSpriteRenderer;
    [SerializeField] private Sprite _nextSprite;

    public void SetNextSprite()
    {
        _targetSpriteRenderer.sprite = _nextSprite;
    }
}
