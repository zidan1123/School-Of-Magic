using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingxing : MonoBehaviour
{
    private Transform m_Transform;

    public float GunPointSize = 0.7f;      //平行的枪口大小  

    public int row = 3;
    public int column = 2;
    public float sectorAngle = 30.0f;    //平行枪口散射伞形范围

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
    private IEnumerator Shoot()
    {
        Vector3 GunPointLeft = m_Transform.position - new Vector3(GunPointSize / 2, 0, 0);  //为了直接在Inspector面板调试，放在这里  //平行枪口用  //枪口最左边(不是第一颗子弹发射的地方！)
        float columnsDistance = GunPointSize / (column + 1);                                //为了直接在Inspector面板调试，放在这里  //平行枪口用
        float sectorLeft = (180 - sectorAngle) / 2;                                 //为了直接在Inspector面板调试，放在这里
        float sectorDistance = sectorAngle / (column + 1);                                  //为了直接在Inspector面板调试，放在这里

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject go = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, GunPointLeft + new Vector3(columnsDistance * (j + 1), 0, 0), Quaternion.identity);
                go.GetComponent<NormalBall>().ShootedAtAngle(3.5f, new Vector2(0.15f, 0.15f), sectorDistance * (j + 1) + sectorLeft + 90, shootColor);

            }
            if (sectorAngle % 360 == 0)
            {
                GameObject go_01 = GameObject.Instantiate<GameObject>(projectile_Normal_Ball, m_Transform.position, Quaternion.identity);
                go_01.GetComponent<NormalBall>().ShootedAtAngle(3.5f, new Vector2(0.15f, 0.15f), 0, shootColor);
                go_01.name = "up";
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
}
