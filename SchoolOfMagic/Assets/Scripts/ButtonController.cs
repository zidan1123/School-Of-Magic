using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private RectTransform m_RectTransform;

    private TextMeshProUGUI m_TextMeshProUGUI;

    public int index;

    private void Awake()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();

        m_TextMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    public void InitButton(int index, Vector3 rectTransform_Position, string name, string text)
    {
        this.index = index;
        m_RectTransform.localPosition = rectTransform_Position;
        gameObject.name = name;
        m_TextMeshProUGUI.text = text;
    }

    public void Selected()
    {
        m_TextMeshProUGUI.color = Color.yellow;
    }

    public void CancelSelected()
    {
        m_TextMeshProUGUI.color = Color.white;
    }
}
