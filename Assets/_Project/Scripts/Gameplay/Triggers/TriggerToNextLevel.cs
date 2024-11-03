using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToNextLevel : MonoBehaviour
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
        Debug.Log($"TriggerToNextLevel: OnTriggerEnter2D: itIsIvan={itIsIvan}");

        if (itIsIvan)
        {
            if (SceneTransition.instance != null)
            {
                SceneTransition.instance.SwitchToNextScene();
            }
            else
            {
                Debug.LogError($"TriggerToNextLevel: OnTriggerEnter2D: SceneTransition.instance is null");
            }
        }
    }
}
