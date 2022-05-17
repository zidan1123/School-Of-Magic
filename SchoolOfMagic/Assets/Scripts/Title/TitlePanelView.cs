using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanelView : MonoBehaviour
{
    private Transform m_Transform;
    private Transform firstTitle_Transform;

    private GameObject button_GameObject;

    public Transform FirstTitle_Transform { get { return firstTitle_Transform; } }

    public GameObject Button_GameObject { get { return button_GameObject; } }
    

    void Awake()
    {
        Init();
    }

    void Update()
    {
        
    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        firstTitle_Transform = m_Transform.Find("FirstTitle").GetComponent<Transform>();

        button_GameObject = Resources.Load<GameObject>("Prefabs/Button");
    }
}
