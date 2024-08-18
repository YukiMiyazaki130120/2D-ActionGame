using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _MoveSpeed;
    private Rigidbody2D EnemyRigid;
    // Start is called before the first frame update
    void Start()
    {
        EnemyRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        EnemyRigid.velocity = new Vector2(Vector2.left.x * _MoveSpeed, EnemyRigid.velocity.y);
    }
}
