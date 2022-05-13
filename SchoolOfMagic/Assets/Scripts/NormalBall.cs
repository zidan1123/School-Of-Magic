using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody2D;

    private bool isMove;
    private float speed;
    private Vector2 direction;

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    public void Shooted(float speed, Vector2 direction)
    {
        isMove = true;
        //Vector3 targerDirection = m_Rigidbody2D.position + direction;
        //targerDirection.z = m_Transform.position.z;
        m_Transform.LookAt(m_Rigidbody2D.position + direction);
        m_Transform.rotation = Quaternion.Euler(new Vector3(0, m_Transform.rotation.y, m_Transform.rotation.z));
        this.speed = speed;
        this.direction = direction;
    }
}
