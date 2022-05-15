using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_SpriteRenderer;

    private bool isMove;
    private float speed;
    private Vector2 direction;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            m_Rigidbody2D.MovePosition(m_Transform.position + m_Transform.up * speed * Time.fixedDeltaTime);
        }
    }

    public void ShootedAtDirection(float speed, Vector2 scale, Vector2 direction, Color color)
    {
        m_Transform.localScale = scale;
        m_Transform.LookAt(m_Rigidbody2D.position + direction);
        m_Transform.rotation = Quaternion.Euler(new Vector3(0, m_Transform.rotation.y, m_Transform.rotation.z));
        m_SpriteRenderer.color = color;
        this.speed = speed;
        this.direction = direction;

        isMove = true;
    }

    public void ShootedAtAngle(float speed, Vector2 scale, float angle, Color color)
    {
        m_Transform.localScale = scale;
        m_Transform.LookAt(m_Rigidbody2D.position + direction);
        m_Transform.rotation = Quaternion.Euler(new Vector3(0, m_Transform.rotation.y, angle));
        m_SpriteRenderer.color = color;
        this.speed = speed;

        isMove = true;
    }
}
