using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvanController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3 _movementDirection;

    private void Update()
    {
        transform.position += _movementDirection * _movementSpeed * Time.deltaTime;
    }
}
