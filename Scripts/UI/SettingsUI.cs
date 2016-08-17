using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : IGameUI
{

    private Button m_settingsBtn;
    public override void Close()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    public override void Open()
    {
        Cache();
        if (m_settingsBtn == null)
        {
            m_settingsBtn = m_transform.GetChild(1).GetChild(0).gameObject.GetComponent<Button>();
        }
        m_settingsBtn.onClick.RemoveAllListeners();
        m_settingsBtn.onClick.AddListener(() => { InputChange(m_settingsBtn); });
        LoadType(m_settingsBtn);
        m_gameObject.SetActive(true);
    }
    public void InputChange(Button btn)
    {
        int type = PlayerPrefs.GetInt("Input");
        type = type == 0 ? 1 : 0;
        btn.transform.GetChild(0).GetComponent<Text>().text = type == 0 ? "Keyboard" : "Mouse";
        PlayerPrefs.SetInt("Input", type);
    }
    public void LoadType(Button btn)
    {
        int type = PlayerPrefs.GetInt("Input");
        btn.transform.GetChild(0).GetComponent<Text>().text = type == 0 ? "Keyboard" : "Mouse";
    }
    public void ClearScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
}
