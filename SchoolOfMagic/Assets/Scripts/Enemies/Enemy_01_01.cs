using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_01 : MonoBehaviour
{
    private Transform m_Transform; 

    public float circleGunPointAngle = 30;   //圆形的枪口大小
    public float circleGunPointRadius = 1f;  //圆形的枪口半径
    private float radian;

    public int row = 3;
    public int column = 2;
    public float sectorAngle = 30.0f;

    public Color shootColor = new Color(40 / 255f, 43 / 255f, 255 / 255f);
    private GameObject projectile_Normal_Ball;

    private float testTime;
    public float testCD = 2f;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        
        projectile_Normal_Ball = Resources.Load<GameObject>("Prefabs/Normal_Ball");
    }

    void Update()
    {
        if (Time.time > testTime + testCD)
        {
            testTime = Time.time;
            StartCoroutine("Shoot");
        }
    }

    /// <summary>
    /// 散弹，三行两列
    /// </summary>
    private IEnumerator Shoot() //此函数内部用世界坐标系(从上逆时针角度)计算
    {
        float GunPointLeftAngle = ((180 - circleGunPointAngle) / 2) + 90;  //为了直接在Inspector面板调试，放在这里  //枪口最左边的角度(不是第一颗子弹发射的地方！)
        float columnsAngleDistance = circleGunPointAngle / (column + 1);   //为了直接在Inspector面板调试，放在这里
        float sectorLeft = ((180 - sectorAngle) / 2) + 90;                 //为了直接在Inspector面板调试，放在这里
        float sectorDistance = sectorAngle / (column + 1);                 //为了直接在Inspector面板调试，放在这里
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject go = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, m_Transform.position + GetCircleGunPoints(GunPointLeftAngle + columnsAngleDistance * (j + 1)), Quaternion.identity);
                go.GetComponent<NormalBall>().ShootedAtAngle(3.5f, new Vector2(0.15f, 0.15f), sectorDistance * (j + 1) + sectorLeft, shootColor);
                
            }
            if (sectorAngle % 360 == 0)
            {
                GameObject go_01 = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, m_Transform.position + GetCircleGunPoints(360), Quaternion.identity);
                go_01.GetComponent<NormalBall>().ShootedAtAngle(3.5f, new Vector2(0.15f, 0.15f), 0, shootColor);
                go_01.name = "up";
            }
            yield return new WaitForSeconds(0.35f);
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
}
