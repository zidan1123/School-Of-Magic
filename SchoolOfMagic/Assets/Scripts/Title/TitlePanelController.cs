using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePanelController : MonoBehaviour
{
    private TitlePanelView m_TitlePanelView;
    private TitlePanelModel m_TitlePanelModel;

    private List<ButtonController> firstButtonList = new List<ButtonController>();
    private int firstTitleButtonIndex;
    private bool isSelectFirstTitleButton;
    private ButtonController currentFirstTitleSelectedButton;
    private ButtonController targetFirstTitleSelectedButton;

    void Start()
    {
        Init();
        CreateFirstTitle();
        ChangeFirstTitleSelectedButton(0);
    }

    void Update()
    {
        if (!isSelectFirstTitleButton)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) 
            {
                CountFirstTitleButtonIndex(firstTitleButtonIndex + 1);
                ChangeFirstTitleSelectedButton(firstTitleButtonIndex);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CountFirstTitleButtonIndex(firstTitleButtonIndex - 1);
                ChangeFirstTitleSelectedButton(firstTitleButtonIndex);
            }

            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                if (firstTitleButtonIndex == 0)
                {
                    isSelectFirstTitleButton = true;
                    SceneManager.LoadScene("Game01");
                }
            }
        }
    }

    private void Init()
    {
        m_TitlePanelView = gameObject.GetComponent<TitlePanelView>();
        m_TitlePanelModel = gameObject.GetComponent<TitlePanelModel>();
    }

    private void CreateFirstTitle()
    {
        for (int i = 0; i < m_TitlePanelModel.FirstTitleButtonNames.Length; i++)
        {
            ButtonController tempButtonController = GameObject.Instantiate<GameObject>(m_TitlePanelView.Button_GameObject, m_TitlePanelView.FirstTitle_Transform).GetComponent<ButtonController>();
            firstButtonList.Add(tempButtonController);
            tempButtonController.InitButton(i, new Vector3(-520 + i * 30, -10 - i * 100, 0), m_TitlePanelModel.FirstTitleButtonNames[i], m_TitlePanelModel.FirstTitleButtonTexts[i]); 
        }
    }

    private void CountFirstTitleButtonIndex(int afterIndex)
    {
        if ( afterIndex < m_TitlePanelModel.FirstTitleButtonNames.Length && afterIndex >= 0)
        {
            firstTitleButtonIndex = afterIndex;
        }
        else if (afterIndex >= m_TitlePanelModel.FirstTitleButtonNames.Length)
        {
            firstTitleButtonIndex = 0;
        }
        else if (afterIndex < m_TitlePanelModel.FirstTitleButtonNames.Length)
        {
            firstTitleButtonIndex = m_TitlePanelModel.FirstTitleButtonNames.Length - 1;
        }
    }

    private void ChangeFirstTitleSelectedButton(int index)
    {
        if (currentFirstTitleSelectedButton != null) currentFirstTitleSelectedButton.CancelSelected();
        targetFirstTitleSelectedButton = firstButtonList[index];
        currentFirstTitleSelectedButton = targetFirstTitleSelectedButton;
        targetFirstTitleSelectedButton.Selected();
    } 
}
