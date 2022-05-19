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

    public float circleGunPointAngle = 30;   //圆形的枪口大小
    public float circleGunPointRadius = 0;  //圆形的枪口半径

    [Range(0, 360)] public float sectorAngle = 45.0f;  //扩散角度的伞形区域

    private float spawnTime;
    public float shootDelay;
    private bool isShootComplete;
    public Color shootColor = new Color(40 / 255f, 43 / 255f, 255 / 255f);
    private GameObject projectile_Normal_Ball;
    public float projectileBtwSpeed = 0.1f;

    private float testTime;
    public float testCD = 2f;

    #region 属性
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
    /// 设置该敌人的射击属性
    /// </summary>
    /// <param name="row">行数</param>
    /// <param name="column">列数</param>
    /// <param name="circleGunPointAngle">圆形的枪口大小</param>
    /// <param name="circleGunPointRadius">圆形的枪口半径</param>
    /// <param name="sectorAngle">扩散角度的伞形区域</param>
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
    /// 散弹，三行两列
    /// </summary>
    private void Shoot() //此函数内部用世界坐标系(从上逆时针角度)计算
    {
        float GunPointLeftAngle = ((180 - circleGunPointAngle) / 2) + 90;  //为了直接在Inspector面板调试，放在这里  //枪口最左边的角度(不是第一颗子弹发射的地方！)
        float invisibleColumnsAngleDistance = circleGunPointAngle / (column + 1);   //为了直接在Inspector面板调试，放在这里
        float sectorLeft = ((180 - sectorAngle) / 2) + 90;                 //为了直接在Inspector面板调试，放在这里
        float sectorDistance = sectorAngle / (column + 1);                 //为了直接在Inspector面板调试，放在这里

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
    /// 获取角色圆形枪口的位置(还未加上角色的位置)
    /// </summary>
    /// <param name="angle">世界坐标系(从上逆时针角度)的角度</param>
    /// <returns></returns>
    private Vector3 GetCircleGunPoints(float angle)  //此函数内部用笛卡尔坐标系(画圆从左边开始逆时针)计算，所以angle都要加90
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
