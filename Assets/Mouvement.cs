using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouvement : MonoBehaviour
{
  public   enum StatePlayer { IDLE, Run, Walk, Crounch }
   public  enum Attack { Base, Special , UpBase , SpecialBase,none}
    [SerializeField] InputActionReference _MoveInput;
    [SerializeField] InputActionReference _AttackInput;

    [SerializeField] Transform _raycastRoot;
    [SerializeField] Vector3 raycastDirection;
    public bool _isGrounded;
    [SerializeField] Transform _root;
    [SerializeField] float _speed;
    [SerializeField] float _MovingThreshold;
    Vector2 _playerMovement;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _MoveAttack;
    [SerializeField] float _currentspeed;

    public Vector2 PlayerMovement { get => _playerMovement; set => _playerMovement = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public Attack AttackState { get => _Attack; set => _Attack = value; }
    public StatePlayer PlayerState { get => _playerState; set => _playerState = value; }

    StatePlayer _playerState;
    Attack _Attack;
    // Start is called before the first frame update
    void Start()
    {
        _playerState = StatePlayer.IDLE;
        _Attack = Attack.none;
        _currentspeed = _speed;

         _AttackInput.action.started += AttackStart;
       // _AttackInput.action.performed += updateAttack;
        _AttackInput.action.started += AttackEnd; 

        _MoveInput.action.started += _MoveStart;
        _MoveInput.action.performed += updateMove;
        _MoveInput.action.canceled += endMove;
    }


    private void AttackEnd(InputAction.CallbackContext obj)
    {
        _Attack = Attack.none;
    }
    /*private void updateAttack(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("attack");
        _Attack = Attack.Base;
    }*/

    private void AttackStart(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("attack");
        _Attack = Attack.Base;
    }
    

    private void endMove(InputAction.CallbackContext obj)
    {
        _playerMovement = new Vector2(0, 0);
        _animator.SetBool("Mouv", false) ;
        _playerState = StatePlayer.IDLE;
    }

    private void updateMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();
    }

    private void _MoveStart(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();
        _animator.SetBool("Mouv", true);
        _playerState = StatePlayer.Walk;
    }

    // Update is called once per frame
    void Update()
    {
     
       if (_playerState == StatePlayer.Walk & _Attack == Attack.Base)
        {
            _currentspeed = _MoveAttack;
            
        }
        else if (_playerState == StatePlayer.Walk & _Attack == Attack.none)
        {
            _currentspeed = _speed;
        }

        var hit = Physics2D.Raycast(_raycastRoot.position, raycastDirection,raycastDirection.magnitude,LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            Debug.DrawLine(_raycastRoot.position, _raycastRoot.position + raycastDirection, Color.magenta);
            _isGrounded = true;
        }
        else
        {
            Debug.DrawLine(_raycastRoot.position, _raycastRoot.position + raycastDirection, Color.red);
            _isGrounded = false;
       
        }

    }
    private void FixedUpdate()
    {
         
        // _rb.MovePosition(_rb.transform.position + (moveInput * _speed * Time.fixedDeltaTime));
        if (_playerState == StatePlayer.Walk)
        {
            
            _root.transform.Translate(_playerMovement * Time.fixedDeltaTime * _currentspeed, Space.World);
            if (_playerMovement.x > 0)
            {
                _root.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_playerMovement.x < 0)   //left
            {
                _root.rotation = Quaternion.Euler(0, 180, 0);

            }
        }
        else
        {

        }
    }


}
