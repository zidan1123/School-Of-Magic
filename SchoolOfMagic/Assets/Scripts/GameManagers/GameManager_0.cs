using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_0 : MonoBehaviour
{
    private Transform m_Transform;

    private GameObject enemy_0_00_Left;
    private GameObject enemy_0_00_Right;

    void Start()
    {
        Init();
        StartCoroutine("small_00");
    }

    void Update()
    {
        
    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        enemy_0_00_Left = Resources.Load<GameObject>("Prefabs/Enemies/Game_0/Enemy_0_00_Left");
        enemy_0_00_Right = Resources.Load<GameObject>("Prefabs/Enemies/Game_0/Enemy_0_00_Right");
    }

    private IEnumerator small_00()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject.Instantiate<GameObject>(enemy_0_00_Left, m_Transform);
            GameObject.Instantiate<GameObject>(enemy_0_00_Right, m_Transform);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
