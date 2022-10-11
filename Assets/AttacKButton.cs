using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class AttacKButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{




    [SerializeField] Mouvement _Player;
    bool _down;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("coucou");
        _Player.Animator.SetTrigger("attack");
        _Player.AttackState = Mouvement.Attack.Base;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("aie");
        _Player.AttackState = Mouvement.Attack.none;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
