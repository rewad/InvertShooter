using UnityEngine; 

public class BonusSystem : MonoBehaviour
{
    private CustomTimer m_timer;
    private Vector2 m_bound;

    void Awake()
    {
        StartSystem();
    }
    public void StartSystem()
    {
        m_timer = new CustomTimer(5.0f, NewBonus, true);
        m_timer.Start();
        GameInstance instance = GameInstance.GetInstance();
        m_bound = instance.HalfSizeBattlefield;
    }

    void Update()
    {
        m_timer.Update(Time.deltaTime);
    }
    public void NewBonus()
    {
        int rand = Random.Range(0, 3);
        IBonus bonus = null;

        switch (rand)
        {
            case 0:
                bonus = GameInstance.GetInstance().ManagerGE.Create<MagnetBonus>().GetComponent<IBonus>();
                break;
            case 1:
                bonus = GameInstance.GetInstance().ManagerGE.Create<DestroyBonus>().GetComponent<IBonus>();
                break;
            case 2:
                bonus = GameInstance.GetInstance().ManagerGE.Create<ShootBonus>().GetComponent<IBonus>();
                break;
            case 3:
                bonus = GameInstance.GetInstance().ManagerGE.Create<RandomBonus>().GetComponent<IBonus>();
                break;
        }

        bonus.Create();
        float x = m_bound.x - 1.0f;
        x = Random.Range(-x, x);
        float y = m_bound.y + 1.0f;
        bonus.AddPosition(new Vector3(x, y, 0));
    }
}
