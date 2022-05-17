using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform m_Transform;
    private BoxCollider2D m_BoxCollider2D;
    private Rigidbody2D m_Rigidbody2D;

    private Transform leftArm_ShootPoint;
    private Transform rightArm_ShootPoint;

    private Vector2 movement;
    public float speed = 5f;

    private float shootTime;
    public float shootCD = 0.1f;

    public Color shootColor = new Color(255 / 255f, 78 / 255f, 255 / 255f);
    private GameObject projectile_Normal_Ball;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>(); 
        m_BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        leftArm_ShootPoint = m_Transform.Find("Player_BodyComponents/Player_LeftArm/LeftArm_ShootPoint").GetComponent<Transform>();
        rightArm_ShootPoint = m_Transform.Find("Player_BodyComponents/Player_RightArm/RightArm_ShootPoint").GetComponent<Transform>();

        projectile_Normal_Ball = Resources.Load<GameObject>("Prefabs/Projectile/Normal_Ball");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time > shootTime + shootCD)
        {
            shootTime = Time.time;

            GameObject go_0 = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, leftArm_ShootPoint.position, Quaternion.identity);
            GameObject go_1 = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, rightArm_ShootPoint.position, Quaternion.identity);

            go_0.GetComponent<NormalBall>().ShootedAtDirection(10f, new Vector2(0.125f, 0.125f), Vector2.up, shootColor);
            go_1.GetComponent<NormalBall>().ShootedAtDirection(10f, new Vector2(0.125f, 0.125f), Vector2.up, shootColor);
        }
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + movement * speed * Time.fixedDeltaTime); 
    }
}
