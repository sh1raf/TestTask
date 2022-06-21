using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Update()
    {
        transform.position += _player.position.normalized;
    }
}
