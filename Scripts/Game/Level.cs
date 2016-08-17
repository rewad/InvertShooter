using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level
{
    private List<Wave> m_wavesEnemy;
    private int m_currentWave;
    private int m_numWave;
    private Vector2 m_boundBattlefield;
    private int m_numActiveEnemy;
    private Queue<KeyValuePair<int, int>> m_queueNewEnemy;
    private CustomTimer m_spawnTimer;
    private ManagerGameEntity m_managerEnemy;
    private CustomTimer m_delayTimer;
    public Level()
    {
        m_wavesEnemy = new List<Wave>();
        m_spawnTimer = new CustomTimer(0.7f, SpawnNextEnemy, true);
    }
    public void Update(float time)
    {
        if (IsWaveComplete())
        {
            m_delayTimer = new CustomTimer((float)m_wavesEnemy[m_currentWave].Delay, NewWave, false);
            m_delayTimer.Start();
            NewWave();
        }
        if (m_queueNewEnemy.Count > 0) m_spawnTimer.Update(time);
    }
    public void SetBound(Vector2 bound)
    {
        m_boundBattlefield = bound;
    }

    public void LoadLevel(string name, ManagerGameEntity manager)
    {
        m_managerEnemy = manager;
        string text = (Resources.Load(name) as TextAsset).text;
        string[] lines = text.Split('\n');
        int count_lines = lines.Length;

        if (count_lines % 2 != 0)
            Debug.Log("Bad level file");

        m_numWave = count_lines / 2;
        for (int i = 0; i < m_numWave; i++)
        {
            string[] num_enemy = lines[i * 2].Split();
            Wave wave = new Wave();
            wave.Enemy1 = Convert.ToInt32(num_enemy[0]);
            wave.Enemy2 = Convert.ToInt32(num_enemy[1]);
            wave.Enemy3 = Convert.ToInt32(num_enemy[2]);
            wave.Delay = Convert.ToSingle(lines[i * 2 + 1]);

            m_wavesEnemy.Add(wave);
        }
        m_queueNewEnemy = new Queue<KeyValuePair<int, int>>();

    }
    public void DestroyEnemy()
    {
        m_numActiveEnemy--;
    }
    public void NewWave()
    {
        if (m_currentWave == m_wavesEnemy.Count)
        {
            GameInstance.GetInstance().ExitGame();
            return;
        }
        Wave wave = m_wavesEnemy[m_currentWave];

        m_numActiveEnemy = wave.Enemy1 + wave.Enemy2 + wave.Enemy3;
        var temp_enemy_array = new List<KeyValuePair<int, int>>();

        for (int i = 0; i < wave.Enemy1; i++)
            temp_enemy_array.Add(new KeyValuePair<int, int>(0, UnityEngine.Random.Range(1, 100)));

        for (int i = 0; i < wave.Enemy2; i++)
            temp_enemy_array.Add(new KeyValuePair<int, int>(1, UnityEngine.Random.Range(1, 100)));

        for (int i = 0; i < wave.Enemy3; i++)
            temp_enemy_array.Add(new KeyValuePair<int, int>(2, UnityEngine.Random.Range(1, 100)));

        m_queueNewEnemy = new Queue<KeyValuePair<int, int>>(temp_enemy_array.OrderBy(x => x.Value));
        m_currentWave++;
        m_delayTimer.End();
        m_spawnTimer.Start();

    }

    public void Restart()
    {
        m_currentWave = 0;
        m_numActiveEnemy = 0;
        m_queueNewEnemy.Clear();
        m_spawnTimer.Restart();
    }
    private void SpawnNextEnemy()
    {

        var new_type_enemy = m_queueNewEnemy.Dequeue();

        switch (new_type_enemy.Key)
        {
            case 0:
                CreateEnemy<Enemy1>();
                break;
            case 1:
                CreateEnemy<Enemy2>();
                break;
            case 2:
                CreateEnemy<Enemy3>();
                break;
        }

        if (m_queueNewEnemy.Count == 0)
        {
            m_spawnTimer.End();
        }
    }
    private void CreateEnemy<T>()
    {
        GameObject g = m_managerEnemy.Create<T>();
        float radius = g.GetComponent<CircleCollider2D>().radius;
        Vector3 location = SpawnLocation(radius);
        Enemy enemy = g.GetComponent<Enemy>() as Enemy;
        enemy.SetPosition(location);
    }

    private Vector3 SpawnLocation(float radius)
    {
        Vector2 bound = m_boundBattlefield - new Vector2(radius + 0.1f, radius + 0.1f);
        float x = UnityEngine.Random.Range(-bound.x, bound.x);
        float y = UnityEngine.Random.Range(-bound.y, bound.y);
        return new Vector3(x, y, 0);
    }

    public bool IsWaveComplete()
    {
        return m_numActiveEnemy == 0;
    }

}
