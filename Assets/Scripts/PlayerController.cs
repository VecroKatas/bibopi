using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerActions _playerActions;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerActions = new PlayerActions();
        _playerActions.InRooms.Enable();
    }

    private void FixedUpdate()
    {
        Movement(_playerActions.InRooms.Movement.ReadValue<Vector2>());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("BarCounter"))
            SceneLoader.Load(SceneLoader.Scene.BarCounter);
    }

    private void Movement(Vector2 direction)
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        _rigidBody.velocity = dir * 10;
    }
}
