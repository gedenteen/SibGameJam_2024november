using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] Rigidbody2D _myRigidbody2D;
    [SerializeField] Collider2D _myCollider2D;
    [SerializeField] private bool _canKillIvan = true;
    [SerializeField] private float _delayBeforeChangeLayer = 3f;

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (!_canKillIvan)
        {
            return;
        }

        IvanController ivanController;
        bool itIsIvan = other.gameObject.TryGetComponent<IvanController>(out ivanController);
        Debug.Log($"DeathCollider: OnCollisionEnter2D: itIsIvan={itIsIvan}");

        if (itIsIvan)
        {
            ivanController.Die();
        }
    }

    public void Unfreeze()
    {
        _myRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        _myRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        _myRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        
        // Принудительный запуск симуляции
        _myRigidbody2D.velocity = Vector2.zero; // Обнуление скорости
        _myRigidbody2D.WakeUp(); // Пробуждение физического объекта

        SetCanNotKillIvan();
    }

    private async UniTask SetCanNotKillIvan()
    {
        await UniTask.WaitForSeconds(_delayBeforeChangeLayer);
        _canKillIvan = false;

        int newLayer = LayerMask.NameToLayer("ColliderNotForChars");
        gameObject.layer = newLayer;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = newLayer;
        }

        // Временное отключение и включение коллайдера для обновления коллизий
        if (_myCollider2D != null)
        {
            _myCollider2D.enabled = false;
            _myCollider2D.enabled = true;
        }
    }
}
