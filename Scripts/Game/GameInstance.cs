using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameInstance : MonoBehaviour
{

    #region singleton

    public static GameInstance GetInstance()
    {

        return m_instance;
    }
    private static GameInstance m_instance;

    #endregion

    #region camera

    public Vector2 HalfSizeBattlefield
    {
        get
        {
            if (m_halfSizeBattlefield == Vector2.zero)
                CalculateBoundScreen();
            return m_halfSizeBattlefield;
        }
    }
    private Vector2 m_halfSizeBattlefield;

    public Camera MainCamera
    {
        get
        {
            if (m_camera == null)
                m_camera = Camera.main;
            return m_camera;
        }
    }
    private Camera m_camera;

    private void CalculateBoundScreen()
    {
        float w = MainCamera.orthographicSize * Screen.width / Screen.height;
        float h = MainCamera.orthographicSize;
        m_halfSizeBattlefield = new Vector2(w, h);
    }

    #endregion

    #region resource

    public ManagerGameEntity ManagerGE
    {
        get
        {
            if (m_managerGameEntity == null)
                m_managerGameEntity = new ManagerGameEntity();

            return m_managerGameEntity;
        }
    }
    private ManagerGameEntity m_managerGameEntity;

    public void AddPool<T>(string name, int size)
    {

        ManagerGE.AddPool(typeof(T), name, size);
    }

    #endregion

    #region game


    private Player m_player;
    private Level m_level;
    private GameOverUI m_gameOverUI;
    private GameUI m_gameUI;
    private GameObject m_sky;
    public static string NameLevel = "level1";
    void Awake()
    {
        m_instance = this;
        m_instance.StartGame();
    }

    public void StartGame()
    {
        InitializationManagerPools(); 
        SpawnPlayer(); 
        m_level = new Level();
        m_level.LoadLevel("Levels\\" + NameLevel, ManagerGE);
        m_level.SetBound(HalfSizeBattlefield);
        gameObject.AddComponent<Sky>();
        gameObject.AddComponent<BonusSystem>();
        m_gameOverUI = FindObjectOfType<GameOverUI>();
        m_gameOverUI.Close();
        m_gameUI = FindObjectOfType<GameUI>();
        m_gameUI.Open();
        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        m_player.Restart();
        m_level.Restart();
        ManagerGE.ClearManager();
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        ManagerGE.ClearManager();
        m_instance = null; 
        SceneManager.LoadScene("Main");
    }

    private void InitializationManagerPools()
    {
        AddPool<Enemy1>("Enemy\\Enemy1", 10);
        AddPool<Enemy2>("Enemy\\Enemy2", 10);
        AddPool<Enemy3>("Enemy\\Enemy3", 10);
        AddPool<BulletPlayer>("Bullet\\BulletPlayer", 20);
        AddPool<BulletEnemy>("Bullet\\BulletEnemy", 20);
        AddPool<Gold>("Games\\Gold", 10);
        AddPool<Cloud>("Games\\Cloud", 10);
        AddPool<Bomb>("Bullet\\Bomb", 10);

        AddPool<MagnetBonus>("Bonus\\Bonus1", 3);
        AddPool<DestroyBonus>("Bonus\\Bonus2", 3);
        AddPool<ShootBonus>("Bonus\\Bonus3", 3);
        AddPool<RandomBonus>("Bonus\\Bonus4", 3);
    } 
    public void UpdateGameUI(int score, int heath)
    {
        m_gameUI.UpdateUI(score, heath);
    }
    private void SpawnPlayer()
    {
        GameObject go = Resources.Load("Games\\Player") as GameObject;
        m_player = Instantiate(go).GetComponent<Player>();
    }

    public void EnemyDestroy()
    {
        m_level.DestroyEnemy();
    }

    public Player GetPlayer()
    {
        return m_player;
    }
    

    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    void Update()
    {
        m_level.Update(Time.deltaTime);
    }
    public void GameOver()
    {
        m_gameOverUI.Open();
        UpdateScore(m_player.GetScore());
        SetPause(true);
    }
    private void UpdateScore(int new_score)
    {
        int old_score = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score", old_score + new_score);
    }
 
    #endregion

}
