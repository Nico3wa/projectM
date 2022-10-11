using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Left : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] Transform _player;
    Vector2 _playerMovement;
    [SerializeField] float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += EndMove;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(_playerMovement.x, 0);

        _player.rotation = Quaternion.Euler(0, 180, 0);
        _player.transform.Translate(direction * Time.fixedDeltaTime * _speed, Space.World);
    }
    public void StartMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();
        //Debug.Log($"Appelé ! {_playerMovement}");
    }

    void UpdateMove(InputAction.CallbackContext obj)
    {
        
        _playerMovement = obj.ReadValue<Vector2>();
        //Debug.Log($"Joystic UPDATE ! {_playerMovement}");
    }
    void EndMove(InputAction.CallbackContext obj)
    {
        _playerMovement = new Vector2(0, 0);
        //Debug.Log($"Joystic Annulé !");
    }
}
