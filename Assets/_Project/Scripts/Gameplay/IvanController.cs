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
}
