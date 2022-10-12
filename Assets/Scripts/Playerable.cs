using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerable : MonoBehaviour
{
    public float Speed = 5f;

    Animator animator;
    CustomInput input;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    bool m_isMove;
    int m_animIDvelocity;

    void Start()
    {
        input = GetComponent<CustomInput>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        m_animIDvelocity = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        rigidbody2D.velocity = new Vector2(input.move.x * Speed, input.move.y * Speed);
        float velocity = Mathf.Abs(rigidbody2D.velocity.magnitude);
        m_isMove = velocity > 0 ? true : false;
        animator.SetFloat(m_animIDvelocity, velocity);

        if (!m_isMove)
            return;
        if (input.move.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
