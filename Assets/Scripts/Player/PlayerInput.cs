using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _dashKey;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_dashKey) && _playerMovement.DashActivated == false)
        {
            Debug.Log("Key Downed");
            StartCoroutine(_playerMovement.Dash());
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _playerMovement.Move(moveDirection);
    }
}
