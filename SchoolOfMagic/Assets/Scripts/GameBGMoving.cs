using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGMoving : MonoBehaviour
{
    private Transform m_Transform;
    private GameObject clouds;

    public float speed = 5;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        m_Transform.Translate(-m_Transform.up * speed * Time.fixedDeltaTime);
    }

    private void Init()
    {
        m_Transform = gameObject.transform;
        clouds = Resources.Load<GameObject>("Prefabs/Clouds");

        CreateClouds();
    }

    private void CreateClouds()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject.Instantiate(clouds, new Vector3(-2.1f, 4995.25f + 24.75f * i, 0), Quaternion.identity, m_Transform);
        }
    }
}
