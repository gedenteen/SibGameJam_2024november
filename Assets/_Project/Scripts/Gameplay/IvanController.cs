using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvanController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3 _movementDirection;
    public bool IsMoving = false;
    public bool IsGrounded = false;

    [Header("References")]
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _myCollider2D;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayer;

    private void Update()
    {
        // Проверка, на земле ли персонаж
        CheckGrounded();

        // Движение только если персонаж находится на земле
        float multiplyerOfGrounded = IsGrounded ? 1f : 0.25f;
        transform.position += _movementDirection * _movementSpeed * Time.deltaTime * multiplyerOfGrounded;

        // Определение направления и обновление анимации
        if (_movementDirection.x != 0 && IsGrounded)
        {
            _characterSprite.flipX = _movementDirection.x > 0f ? false : true;
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        _animator.SetBool("IsMoving", IsMoving);
    }

    private void CheckGrounded()
    {
        // Проверка соприкосновения CapsuleCollider2D с землей
        IsGrounded = _myCollider2D.IsTouchingLayers(_groundLayer);
    }

    public void Die()
    {
        _animator.SetBool("Death", true);
        _movementSpeed = 0f;
        _movementDirection = Vector3.zero;
        
        GlobalEvents.EventIvanIsDead?.Invoke();
    }

    public void SetMovementDirectionToLeft()
    {
        _movementDirection = new Vector3(-1f, 0f, 0f);
    }

    public void SetMovementDirectionToRight()
    {
        _movementDirection = new Vector3(1f, 0f, 0f);
    }

    public void Stop()
    {
        _movementDirection = Vector3.zero;
    }
}
