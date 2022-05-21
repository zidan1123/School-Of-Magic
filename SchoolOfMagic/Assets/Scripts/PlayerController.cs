using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform m_Transform;
    private BoxCollider2D m_BoxCollider2D;
    private Rigidbody2D m_Rigidbody2D;

    private GameObject player_BodyComponents;  //no include heart
    private GameObject player_Heart;  //no include heart
    private SpriteRenderer heart_SpriteRenderer;

    private Transform leftArm_ShootPoint;
    private Transform rightArm_ShootPoint;

    private bool isLife = true;
    private float respawnTime = 1;
    private Vector3 respawnPosition = new Vector3(-1.85f, -3.72f, 0);

    private Vector2 movement;
    public float speed = 5f;
    private float slowSpeedFactor = 1.85f;
    private float nowSlowSpeedFactor = 1;

    private float shootTime;
    public float shootCD = 0.1f;

    public Color shootColor = new Color(255 / 255f, 78 / 255f, 255 / 255f);
    private GameObject projectile_Normal_Ball;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>(); 
        m_BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        player_BodyComponents = m_Transform.Find("Player_BodyComponents").gameObject;
        player_Heart = m_Transform.Find("Player_Heart").gameObject;
        heart_SpriteRenderer = m_Transform.Find("Player_Heart").GetComponent<SpriteRenderer>();

        leftArm_ShootPoint = m_Transform.Find("Player_BodyComponents/Player_LeftArm/LeftArm_ShootPoint").GetComponent<Transform>();
        rightArm_ShootPoint = m_Transform.Find("Player_BodyComponents/Player_RightArm/RightArm_ShootPoint").GetComponent<Transform>();

        projectile_Normal_Ball = Resources.Load<GameObject>("Prefabs/Projectile/Normal_Ball");
    }

    private void Update()
    {
        if (isLife)
        {
            CheckInput();

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                heart_SpriteRenderer.enabled = true;
                nowSlowSpeedFactor = slowSpeedFactor;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                heart_SpriteRenderer.enabled = false;
                nowSlowSpeedFactor = 1;
            }

            if (Input.GetKey(KeyCode.Z) && Time.time > shootTime + shootCD)
            {
                shootTime = Time.time;

                GameObject go_0 = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, leftArm_ShootPoint.position, Quaternion.identity);
                GameObject go_1 = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, rightArm_ShootPoint.position, Quaternion.identity);

                go_0.GetComponent<NormalBall>().ShootedAtDirection(10f, new Vector2(0.125f, 0.125f), Vector2.up, shootColor);
                go_0.tag = "PlayerProjectile";
                go_1.GetComponent<NormalBall>().ShootedAtDirection(10f, new Vector2(0.125f, 0.125f), Vector2.up, shootColor);
                go_1.tag = "PlayerProjectile";
            }
        }
    }

    void FixedUpdate()
    {
        if (isLife)
        {
            ApplyMovement();
        }
    }

    private void CheckInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void ApplyMovement()
    {
        if (Mathf.Abs(movement.x) > 0 && Mathf.Abs(movement.y) > 0)
        {
            m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + movement / 1.4f / nowSlowSpeedFactor * speed * Time.fixedDeltaTime);
        }
        else
        {
            m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + movement / nowSlowSpeedFactor * speed * Time.fixedDeltaTime);
        }
    }

    private void Dead()
    {
        if (isLife)
        {
            isLife = false;
            player_BodyComponents.SetActive(false);
            player_Heart.SetActive(false);

            Invoke("Respawn", respawnTime);
        }
        //GameObject.Destroy(gameObject);
    }

    private void Respawn()
    {
        m_Transform.position = respawnPosition;

        isLife = true;
        player_BodyComponents.SetActive(true);
        player_Heart.SetActive(true);
    }
}
