using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mySpriteRenderer;

    private void Awake()
    {
        //_mySpriteRenderer.gameObject.SetActive(false);
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

    // protected void OnTriggerExit2D(Collider2D other)
    // {
    // }
}
