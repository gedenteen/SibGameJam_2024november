using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsDisabler : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectForDeactivate;
    [SerializeField] private GameObject _gameObjectForActivate;

    public void ChangeObjects()
    {
        _gameObjectForDeactivate.SetActive(false);
        _gameObjectForActivate.SetActive(true);
    }
}
