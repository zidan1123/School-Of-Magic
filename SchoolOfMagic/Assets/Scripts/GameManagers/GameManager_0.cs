using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_0 : MonoBehaviour
{
    private Transform m_Transform;

    public Game_Difficulty gama_Difficulty;

    private bool isStartCoroutine;

    private GameObject enemy_0_00_Left;
    private GameObject enemy_0_00_Right;

    void Start()
    {
        Init();
        
    }

    void Update()
    {
        if (!isStartCoroutine)
        {
            switch (gama_Difficulty)
            {
                case Game_Difficulty.Easy:
                    StartCoroutine("small_00_Easy");
                    break;
                case Game_Difficulty.Normal:
                    StartCoroutine("small_00_Normal");
                    break;
                case Game_Difficulty.Hard:
                    StartCoroutine("small_00_Hard");
                    break;
            }
            isStartCoroutine = true;
        }
        
    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        enemy_0_00_Left = Resources.Load<GameObject>("Prefabs/Enemies/Game_0/Enemy_0_00_Left");
        enemy_0_00_Right = Resources.Load<GameObject>("Prefabs/Enemies/Game_0/Enemy_0_00_Right");
    }

    //---------------------------------------------Easy---------------------------------------------

    private IEnumerator small_00_Easy()
    {
        yield return new WaitForSeconds(3);

        List<int> tempIntList = new List<int>();
        List<GameObject> tempList;
        int removeCount = 0;

        //Left
        tempIntList = GetRandumNumberArray(8, 4);
        tempList = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            GameObject tempGameObject_0 = GameObject.Instantiate<GameObject>(enemy_0_00_Left, new Vector3(-3.94f, 4.8835f, 0), Quaternion.identity, m_Transform);
            GameObject tempGameObject_1 = GameObject.Instantiate<GameObject>(enemy_0_00_Left, new Vector3(-3.14f, 4.8835f, 0), Quaternion.identity, m_Transform);

            tempGameObject_0.name = "Enemy0_00_" + tempList.Count;
            tempList.Add(tempGameObject_0);
            tempGameObject_1.name = "Enemy0_00_" + tempList.Count;
            tempList.Add(tempGameObject_1);

            removeCount = 0;
            for (int j = 0; j < tempIntList.Count; j++)
            {
                if (tempIntList[j] < tempList.Count)
                {
                    tempList[tempIntList[j]].GetComponent<Enemy_0_00>().SetShootValue(1, 1, 0, 0, 45, Random.Range(0.1f, 4.5f));
                    tempList[tempIntList[j]].name += "_Shoot";
                    removeCount++;
                }
            }
            tempIntList.RemoveRange(0, removeCount);

            yield return new WaitForSeconds(0.4f);
        }
        
        yield return new WaitForSeconds(1.5f);

        //Right
        tempIntList = GetRandumNumberArray(8, 4);
        tempList = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            GameObject tempGameObject_0 = GameObject.Instantiate<GameObject>(enemy_0_00_Right, new Vector3(-0.56f, 4.8835f, 0), Quaternion.identity, m_Transform);
            GameObject tempGameObject_1 = GameObject.Instantiate<GameObject>(enemy_0_00_Right, new Vector3(0.24f, 4.8835f, 0), Quaternion.identity, m_Transform);

            tempGameObject_0.name = "Enemy0_00_" + tempList.Count;
            tempList.Add(tempGameObject_0);
            tempGameObject_1.name = "Enemy0_00_" + tempList.Count;
            tempList.Add(tempGameObject_1);

            removeCount = 0;
            for (int j = 0; j < tempIntList.Count; j++)
            {
                if (tempIntList[j] < tempList.Count)
                {
                    tempList[tempIntList[j]].GetComponent<Enemy_0_00>().SetShootValue(1, 1, 0, 0, 45, Random.Range(0.1f, 4.5f));
                    tempList[tempIntList[j]].name += "_Shoot";
                    removeCount++;
                }
            }
            tempIntList.RemoveRange(0, removeCount);

            yield return new WaitForSeconds(0.4f);
        }
    }

    //---------------------------------------------Normal---------------------------------------------

    private IEnumerator small_00_Normal()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject.Instantiate<GameObject>(enemy_0_00_Left, m_Transform);
            GameObject.Instantiate<GameObject>(enemy_0_00_Right, m_Transform);
            yield return new WaitForSeconds(0.4f);
        }
    }

    //---------------------------------------------Hard---------------------------------------------

    private IEnumerator small_00_Hard()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject.Instantiate<GameObject>(enemy_0_00_Left, m_Transform);
            GameObject.Instantiate<GameObject>(enemy_0_00_Right, m_Transform);
            yield return new WaitForSeconds(0.4f);
        }
    }

    //---------------------------------------------道具函数---------------------------------------------

    private List<int> GetRandumNumberArray(int randomNumberCount, int listCount)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < randomNumberCount; i++)
        {
            tempList.Add(i);
        }
        for (int i = 0; i < listCount; i++)
        {
            tempList.RemoveAt(Random.Range(0, randomNumberCount - i));  //抽到的被移除
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            Debug.Log(tempList[i]);
        }
        return tempList;
    }
}
