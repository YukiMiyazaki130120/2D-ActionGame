using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _PlayerRigidbody;

    [SerializeField, Header("移動速度")]
    private float _moveSpeed = 2f;

    [SerializeField, Header("ジャンプ力")]
    private float _JumpForce = 5f;
    private Vector2 _InputDirection;

    private Animator _PlayerAnimation;
    public bool _BoolJump;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerRigidbody = GetComponent<Rigidbody2D>();
        _PlayerAnimation = GetComponent<Animator>();
        _BoolJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
    }

    private void OnMove()
    {
         if(_InputDirection.x < 0)
        {
            this.transform.eulerAngles = new Vector2(0, 180);
        }
        else if(_InputDirection.x > 0)
        {
            this.transform.eulerAngles = new Vector2(0,0);
        }

        _PlayerRigidbody.velocity = new Vector2(_InputDirection.x * _moveSpeed, _PlayerRigidbody.velocity.y);
        _PlayerAnimation.SetBool("isWalk", _InputDirection.x != 0.0f);
    }

        public void OnMoveAction(InputAction.CallbackContext callbackContext)
    {
        _InputDirection = callbackContext.ReadValue<Vector2>();
        // Debug.Log($"position {_InputDirection.x},{_InputDirection.y}");
    }


    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if(!callbackContext.performed || _BoolJump) return;
    
        _PlayerRigidbody.AddForce(Vector2.up * _JumpForce, ForceMode2D.Impulse);
        _BoolJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor") _BoolJump = false;
    }

}
