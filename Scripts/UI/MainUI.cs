using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MainUI : IGameUI
{
    private Text m_text;
    void Awake()
    {
        Open();
    }
    public override void Close()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    public override void Open()
    {
        Cache();
        m_gameObject.SetActive(true);
        if (m_text == null)
            m_text = m_transform.GetChild(1).GetComponent<Text>();
        m_text.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
