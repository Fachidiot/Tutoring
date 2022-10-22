using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerable : MonoBehaviour
{
    enum State
    {
        None,
        Attack,
        Defend
    }

    enum PlayerAttack
    {
        None,
        Attack1,
        Attack2,
        Attack3
    }
    public float Speed = 5f;
    public GameObject AttackArea;
    public float ComboTimeLimit;

    Animator animator;
    CustomInput input;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    State state;
    PlayerAttack m_attackType;

    bool m_isMove;
    bool m_isAttacking;
    int m_comboCounter = 0;

    // Animation IDs
    int m_animIDvelocity;
    int m_animIDisAttack;
    int m_animIDcombo;

    void Start()
    {
        input = GetComponent<CustomInput>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        AttackArea.SetActive(false);

        m_animIDvelocity = Animator.StringToHash("Velocity");
        m_animIDisAttack = Animator.StringToHash("isAttack");
        m_animIDcombo = Animator.StringToHash("AttackCombo");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (input.mouseL)
            StartAttack();
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

    float m_time = 0f;
    public void StartAttack()
    {
        m_isAttacking = true;
        AttackArea.SetActive(true);
        animator.SetBool(m_animIDisAttack, m_isAttacking);

        if (state != State.Attack)
        {
            state = State.Attack;
            m_attackType = PlayerAttack.Attack1;
            m_time = Time.fixedTime;
        }
        else if (m_attackType <= PlayerAttack.Attack2 && Time.fixedTime - m_time > 0.2f ||
            m_attackType == PlayerAttack.Attack3 && Time.fixedTime - m_time > 0.3f)
        {
            StopCoroutine("CheckCombo");
            AttackArea.GetComponent<Attack>().AddDamage(((int)m_attackType));
            m_attackType = m_attackType + 1;
            m_time = Time.fixedTime;
        }
        animator.SetInteger(m_animIDcombo, ((int)m_attackType));
        StartCoroutine("CheckCombo");
    }

    IEnumerator CheckCombo()
    {
        yield return new WaitForSeconds(0.7f);

        m_time = 0f;
        state = State.None;
        m_isAttacking = false;
        AttackArea.SetActive(false);
        AttackArea.GetComponent<Attack>().ResetDamage();
        m_attackType = PlayerAttack.None;
        animator.SetBool(m_animIDisAttack, m_isAttacking);
        animator.SetInteger(m_animIDcombo, ((int)m_attackType));
    }
}
