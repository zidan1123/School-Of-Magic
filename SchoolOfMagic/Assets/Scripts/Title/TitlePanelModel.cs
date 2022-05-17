using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanelModel : MonoBehaviour
{
    private string[] firstTitleButtonNames;
    private string[] firstTitleButtonTexts;

    public string[] FirstTitleButtonNames { get { return firstTitleButtonNames; } }
    public string[] FirstTitleButtonTexts { get { return firstTitleButtonTexts; } }

    void Awake()
    {
        Init();
    }

    void Update()
    {

    }

    private void Init()
    {
        firstTitleButtonNames = new string[] { "Button_StartGame", "Button_LevelPractice", "Button_SpellPractice", "Button_Option", "Button_Quit" };
        firstTitleButtonTexts = new string[] { "��ʼ��Ϸ", "�ؿ���ϰ", "������ϰ", "����", "�˳���Ϸ" };
    }
}
