using UnityEngine;
using System.Collections;
using System;

public class PauseUI : IGameUI {

	public void Resume()
    {
        gameObject.SetActive(false);
    }
    public void Retry()
    {
        GameInstance.GetInstance().RestartGame();
        gameObject.SetActive(false);

    }
    public void Exit()
    {
        GameInstance.GetInstance().ExitGame();
        gameObject.SetActive(false); 
    }
  
    public override void Open()
    {
        Cache();
        GameInstance.GetInstance().SetPause(true);
        m_gameObject.SetActive(true);
    }

    public override void Close()
    {
        Cache();
        GameInstance.GetInstance().SetPause(false);
        m_gameObject.SetActive(false);
    }
}
