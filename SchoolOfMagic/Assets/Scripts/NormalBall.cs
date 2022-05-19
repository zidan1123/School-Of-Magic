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
    public int attack = 10;
    private Vector2 direction;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (m_Transform.position.y <= -5.375f)
        {
            DestroyItself();
        }
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

        //if(direction.x - )
        m_Transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Vector2.Angle(Vector2.up, direction)));
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

    private void DestroyItself()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "PlayerProjectile" && collision.gameObject.tag == "Enemy")
        {
            collision.SendMessage("Damaged", attack);
            DestroyItself();
        }

        if (gameObject.tag == "EnemiesProjectile" && collision.gameObject.tag == "Player")
        {
            collision.SendMessageUpwards("Dead");
        }
    }
}
