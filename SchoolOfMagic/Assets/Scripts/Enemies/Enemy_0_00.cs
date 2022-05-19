using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_0_00 : MonoBehaviour
{
    private Transform m_Transform;

    private int hp = 10;

    public int row = 3;
    public int column = 2;

    public float circleGunPointAngle = 30;   //Բ�ε�ǹ�ڴ�С
    public float circleGunPointRadius = 0;  //Բ�ε�ǹ�ڰ뾶

    [Range(0, 360)] public float sectorAngle = 45.0f;  //��ɢ�Ƕȵ�ɡ������

    private float spawnTime;
    public float shootDelay;
    private bool isShootComplete;
    public Color shootColor = new Color(40 / 255f, 43 / 255f, 255 / 255f);
    private GameObject projectile_Normal_Ball;
    public float projectileBtwSpeed = 0.1f;

    private float testTime;
    public float testCD = 2f;

    #region ����
    public int HP
    {   get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0) DestroyItself();
        }
    }
    #endregion

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        projectile_Normal_Ball = Resources.Load<GameObject>("Prefabs/Projectile/Normal_Ball");

        testTime = Time.time;
        spawnTime = Time.time;
    }

    void Update()
    {
        //test
        //if (Time.time > testTime + testCD)
        //{
        //    testTime = Time.time;
        //    Shoot();
        //}

        if (Time.time > spawnTime + shootDelay && !isShootComplete)
        {
            Shoot();
            isShootComplete = true;
        }
    }

    /// <summary>
    /// ���øõ��˵��������
    /// </summary>
    /// <param name="row">����</param>
    /// <param name="column">����</param>
    /// <param name="circleGunPointAngle">Բ�ε�ǹ�ڴ�С</param>
    /// <param name="circleGunPointRadius">Բ�ε�ǹ�ڰ뾶</param>
    /// <param name="sectorAngle">��ɢ�Ƕȵ�ɡ������</param>
    public void SetShootValue(int row, int column, int circleGunPointAngle, int circleGunPointRadius, float sectorAngle, float shootDelay)
    {
        this.row = row;
        this.column = column;
        this.circleGunPointAngle = circleGunPointAngle;
        this.circleGunPointRadius = circleGunPointRadius;
        this.sectorAngle = sectorAngle;
        this.shootDelay = shootDelay;
    }

    public void SetEnemyBodyValue(int hp, Vector2 scale)
    {
        this.HP = hp;
        m_Transform.localScale = scale;
    }
    
    /// <summary>
    /// ɢ������������
    /// </summary>
    private void Shoot() //�˺����ڲ�����������ϵ(������ʱ��Ƕ�)����
    {
        float GunPointLeftAngle = ((180 - circleGunPointAngle) / 2) + 90;  //Ϊ��ֱ����Inspector�����ԣ���������  //ǹ������ߵĽǶ�(���ǵ�һ���ӵ�����ĵط���)
        float invisibleColumnsAngleDistance = circleGunPointAngle / (column + 1);   //Ϊ��ֱ����Inspector�����ԣ���������
        float sectorLeft = ((180 - sectorAngle) / 2) + 90;                 //Ϊ��ֱ����Inspector�����ԣ���������
        float sectorDistance = sectorAngle / (column + 1);                 //Ϊ��ֱ����Inspector�����ԣ���������

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject go = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, m_Transform.position + GetCircleGunPoints(GunPointLeftAngle + invisibleColumnsAngleDistance * (j + 1)), Quaternion.identity);
                go.GetComponent<NormalBall>().ShootedAtAngle(3.5f - i * projectileBtwSpeed, new Vector2(0.15f, 0.15f), sectorDistance * (j + 1) + sectorLeft, shootColor);
                go.GetComponent<SpriteRenderer>().sortingLayerName = "EnemiesProjectile";
                go.tag = "EnemiesProjectile";
            }
            if (sectorAngle % 360 == 0)
            {
                GameObject go = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, m_Transform.position + GetCircleGunPoints(360), Quaternion.identity);
                go.GetComponent<NormalBall>().ShootedAtAngle(3.5f - i * projectileBtwSpeed, new Vector2(0.15f, 0.15f), 0, shootColor);
                go.GetComponent<SpriteRenderer>().sortingLayerName = "EnemiesProjectile";
                go.tag = "EnemiesProjectile";
            }
        }
    }

    /// <summary>
    /// ��ȡ��ɫԲ��ǹ�ڵ�λ��(��δ���Ͻ�ɫ��λ��)
    /// </summary>
    /// <param name="angle">��������ϵ(������ʱ��Ƕ�)�ĽǶ�</param>
    /// <returns></returns>
    private Vector3 GetCircleGunPoints(float angle)  //�˺����ڲ��õѿ�������ϵ(��Բ����߿�ʼ��ʱ��)���㣬����angle��Ҫ��90
    {
        float xScale = Mathf.Cos((angle + 90) * Mathf.Deg2Rad);
        float yScale = Mathf.Sin((angle + 90) * Mathf.Deg2Rad);

        float x = xScale * circleGunPointRadius;
        float y = yScale * circleGunPointRadius;
        Debug.Log(new Vector3(x, y, 0));
        return new Vector3(x, y, 0);
    }

    private void Damaged(int damage)
    {
        HP -= damage;
    }

    public void DestroyItself()
    {
        GameObject.Destroy(gameObject);
    }
}
