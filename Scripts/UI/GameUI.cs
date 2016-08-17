using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameUI : IGameUI
{

    private List<GameObject> m_heartsUI;
    private Text m_scoreText;
    void Awake()
    {
        Cache();
        GameObject template = Resources.Load("UI\\Hearth") as GameObject;
        m_heartsUI = new List<GameObject>();
        Transform panel = m_transform.GetChild(1);
        for (int i = 0; i < 3; i++)
        {
            GameObject new_heart = Instantiate(template);
            m_heartsUI.Add(new_heart);
            new_heart.transform.SetParent(panel);
        }

        m_scoreText = m_transform.GetChild(2).GetChild(0).GetComponent<Text>();
        
    }



    public void UpdateUI(int score,int heath)
    {
        m_scoreText.text = score.ToString();

        for (int i = 0; i < heath; i++)
            m_heartsUI[i].SetActive(true);
        for (int i = heath; i < 3; i++)
            m_heartsUI[i].SetActive(false);
    }

    public override void Open()
    {
        m_gameObject.SetActive(true);
    }

    public override void Close()
    {
        m_gameObject.SetActive(false);
    }
}
