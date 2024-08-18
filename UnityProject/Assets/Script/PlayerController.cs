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
    private float _JumpForce = 20f;
    private Vector2 _InputDirection;

    private Animator _PlayerAnimation;
    private bool _BoolJump;

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
    
        _PlayerRigidbody.AddForce(Vector2.up * _JumpForce, ForceMode2D.Impulse );
        _BoolJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor") _BoolJump = false;
        if(collision.gameObject.tag == "Enemy")
        {
            
            _HitEnemy(collision.gameObject);
        }
    }

    private void _HitEnemy(GameObject enemy)
    {
        float HalfScaleY = transform.lossyScale.y / 2.0f;
        float EnemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        Debug.Log("第1引数は" + (transform.position.y - (HalfScaleY - 0.1f)));
        Debug.Log("第2引数は" + (enemy.transform.position.y + (EnemyHalfScaleY - 0.1f)));
        // 敵の頭よりもプレイヤーの脚が高い位置で接触した場合
        if(transform.position.y - (HalfScaleY - 0.1f) >= enemy.transform.position.y + (EnemyHalfScaleY - 0.1f))
        {
            Debug.Log("Destroy");
            Destroy(enemy);
        }
        else
        {
            Debug.Log("Bug");
        }

    }

}
