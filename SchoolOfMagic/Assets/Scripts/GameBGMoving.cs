using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGMoving : MonoBehaviour
{
    private Transform m_Transform;

    public GameObject alternatingPicture;

    public float speed = 5;

    void Start()
    {
        Init();
    }

    private void Update()
    {
        if(m_Transform.position.y <= -32.495f)
        {
            m_Transform.position = alternatingPicture.transform.position + new Vector3(0, 27.865f, 0);
        }
    }

    void FixedUpdate()
    {
        m_Transform.Translate(-m_Transform.up * speed * Time.fixedDeltaTime);
    }

    private void Init()
    {
        m_Transform = gameObject.transform;

        string thisAlternatingPictureIndex =  gameObject.name.Substring(gameObject.name.Length - 1);
        
        if (thisAlternatingPictureIndex == "0") 
            alternatingPicture = GameObject.Find(gameObject.name.Substring(0, gameObject.name.Length - 1) + "1");
        else
            alternatingPicture = GameObject.Find(gameObject.name.Substring(0, gameObject.name.Length - 1) + "0");
    }
}
