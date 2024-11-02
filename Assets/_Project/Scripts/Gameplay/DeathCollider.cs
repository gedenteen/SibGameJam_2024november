using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] Rigidbody2D _myRigidbody2D;

    protected void OnCollisionEnter2D(Collision2D other)
    {
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
        _myRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        _myRigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        
        // Принудительный запуск симуляции
        _myRigidbody2D.velocity = Vector2.zero; // Обнуление скорости
        _myRigidbody2D.WakeUp(); // Пробуждение физического объекта
    }
}
