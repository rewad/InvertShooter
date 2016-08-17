using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LevelsUI : IGameUI
{

    private string[] m_levels;

    public override void Close()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    public override void Open()
    {
        Cache();
        if (m_levels == null)
            LoadFileLevels();

        m_gameObject.SetActive(true);
    }

    public void OpenLevel(string level)
    {
        GameInstance.NameLevel = level;
        SceneManager.LoadScene("Game");  
    }

    private void LoadFileLevels()
    {
        string text = (Resources.Load("Levels\\levels") as TextAsset).text;
        m_levels = text.Split('\n');

        GameObject template_item = Resources.Load("UI\\1") as GameObject;

        Transform parent = m_transform.GetChild(0).GetChild(0).transform;

        for (int i = 0; i < m_levels.Length; i++)
        {
            Transform new_item = Instantiate(template_item).transform;
            new_item.transform.SetParent(parent);
            new_item.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            string[] names = m_levels[i].Split();
            new_item.name = names[0];
            new_item.GetChild(0).GetComponent<Text>().text = names[1];
            new_item.GetComponent<Button>().onClick.AddListener(() => { OpenLevel(names[0]); });
        }
    }


}
