using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForIvan : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mySpriteRenderer;
    [SerializeField] private bool _hideSpriteInGame = true;
    [SerializeField] private List<UnityEvent> _eventsForInvoke;

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
        //Debug.Log($"TriggerForIvan: OnTriggerEnter2D: itIsIvan={itIsIvan}");

        if (itIsIvan)
        {
            if (_eventsForInvoke == null)
            {
                Debug.Log($"TriggerForIvan: OnTriggerEnter2D: _methodForInvoke is null");
                return;
            }

            for (int i = 0; i < _eventsForInvoke.Count; i++)
            {
                _eventsForInvoke[i].Invoke();
            }
        }
    }
}
