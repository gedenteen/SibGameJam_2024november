using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        Vector3 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += input * _movementSpeed * Time.deltaTime;
    }
}
