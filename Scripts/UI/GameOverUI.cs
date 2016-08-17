using UnityEngine;
using System.Collections;
using System;

public class GameOverUI : IGameUI
{
    public override void Close()
    {
        Cache();
        GameInstance.GetInstance().SetPause(false);
        m_gameObject.SetActive(false);
    }
    public void Retry()
    {
        GameInstance.GetInstance().RestartGame(); 
    }
    public override void Open()
    {
        Cache();
        m_gameObject.SetActive(true);
    }
}
