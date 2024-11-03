using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LiftingDoor : MonoBehaviour
{
    [SerializeField] private Vector3 _displacementVector;
    [SerializeField] private float _timer = 2f;

    public void Lift()
    {
        LiftAsync();
    }

    public async UniTask LiftAsync()
    {
        float time = _timer;

        while (time > 0f)
        {
            transform.position += _displacementVector;
            await UniTask.WaitForFixedUpdate();
            time -= Time.deltaTime;
        }
    }
}
