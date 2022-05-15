using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_01 : MonoBehaviour
{
    private Transform m_Transform; 

    public float circleGunPointAngle = 30;   //Բ�ε�ǹ�ڴ�С
    public float circleGunPointRadius = 1f;  //Բ�ε�ǹ�ڰ뾶
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
    /// ɢ������������
    /// </summary>
    private IEnumerator Shoot() //�˺����ڲ�����������ϵ(������ʱ��Ƕ�)����
    {
        float GunPointLeftAngle = ((180 - circleGunPointAngle) / 2) + 90;  //Ϊ��ֱ����Inspector�����ԣ���������  //ǹ������ߵĽǶ�(���ǵ�һ���ӵ�����ĵط���)
        float columnsAngleDistance = circleGunPointAngle / (column + 1);   //Ϊ��ֱ����Inspector�����ԣ���������
        float sectorLeft = ((180 - sectorAngle) / 2) + 90;                 //Ϊ��ֱ����Inspector�����ԣ���������
        float sectorDistance = sectorAngle / (column + 1);                 //Ϊ��ֱ����Inspector�����ԣ���������
        
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
}
