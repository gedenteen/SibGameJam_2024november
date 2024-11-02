using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mySpriteRenderer;
    [SerializeField] private bool _hideSpriteInGame = true;

    private void Awake()
    {
        if (_hideSpriteInGame)
        {
            _mySpriteRenderer.gameObject.SetActive(false);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        IvanController ivanController;
        bool itIsIvan = other.TryGetComponent<IvanController>(out ivanController);
        Debug.Log($"DeathTrigger: OnTriggerEnter2D: itIsIvan={itIsIvan}");

        if (itIsIvan)
        {
            ivanController.Die();
        }
    }
}
