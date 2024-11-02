using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvanController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3 _movementDirection;
    public bool IsMoving = false;

    [Header("References")]
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider2D _myCollider2D;


    private void Update()
    {
        transform.position += _movementDirection * _movementSpeed * Time.deltaTime;

        if (_movementDirection.x != 0)
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

    public void Die()
    {
        _animator.SetBool("Death", true);
        _movementSpeed = 0f;
        _movementDirection = Vector3.zero;
    }
}
