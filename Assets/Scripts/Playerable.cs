using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerable : MonoBehaviour
{
    public float Speed = 5f;
    public GameObject AttackArea;
    public float ComboTimeLimit;

    Animator animator;
    CustomInput input;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    bool m_isMove;
    int m_comboCounter = 0;

    // Animation IDs
    int m_animIDvelocity;
    int m_animIDcombo;

    void Start()
    {
        input = GetComponent<CustomInput>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        AttackArea.SetActive(false);

        m_animIDvelocity = Animator.StringToHash("Velocity");
        m_animIDcombo = Animator.StringToHash("AttackCombo");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
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

    float ComboTimer;
    void Attack()
    {
        if (input.mouseL)
        {
            ComboTimer += Time.deltaTime;
            AttackArea.SetActive(true);
            //if (ComboTimer > ComboTimeLimit)
            //    m_comboCounter = 0;
            //else
            m_comboCounter++;
            animator.SetInteger(m_animIDcombo, m_comboCounter);
            m_comboCounter = 0;
            AttackArea.SetActive(false);
        }
    }
}
