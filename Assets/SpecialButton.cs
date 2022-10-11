using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpecialButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
   
    enum Direction { Left, Right }
    [SerializeField] Mouvement _Player;
    [SerializeField] Direction _direction;
    bool _down;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("coucou");
        _down = true;
        _Player.Animator.SetBool("Mouv", true);
        _Player.PlayerState = Mouvement.StatePlayer.Walk;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _Player.PlayerMovement = new Vector2(0, 0);
        _Player.Animator.SetBool("Mouv", false);
        Debug.Log("aie");
        _down = false;
        _Player.PlayerState = Mouvement.StatePlayer.IDLE;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_down == true)
        {
            //_Player.PlayerMovement = new Vector2(_direction == Direction.Left ? -1 : 1, 0, 0);
            if(_direction == Direction.Left)
            {
               
                _Player.PlayerMovement = new Vector2( -1, 0);
                //_Player.Root.transform.Translate(_Player.PlayerMovement * Time.fixedDeltaTime * _Player.Currentspeed, Space.World);
            }
            else
            {
              
                _Player.PlayerMovement = new Vector2(1, 0);
                //_Player.Root.transform.Translate(_Player.PlayerMovement * Time.fixedDeltaTime * _Player.Currentspeed, Space.World);
            }
        }
        else
        {
           
        }
    }

}
