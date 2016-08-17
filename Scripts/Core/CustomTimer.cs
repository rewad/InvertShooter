using System; 

public class CustomTimer
{
    private Action m_action;
    private float m_delay;
    private float m_currentTime;
    private bool m_isStop;
    private bool m_isLoop;

    public CustomTimer(float time, Action func, bool loop = false)
    {
        m_delay = time;
        m_action = func;
        m_isStop = false;
        m_isLoop = loop;
    }
    public void Start()
    {
        m_isStop = false;
    }

    public void Restart()
    {
        m_isStop = false;
        m_currentTime = 0.0f;
    }
    public void End()
    {
        m_isStop = true;
    }
    public void Update(float delta)
    {
        m_currentTime += delta;
        if (m_currentTime >= m_delay && !m_isStop)
        {
            m_action();
            m_currentTime = 0.0f;
            if (!m_isLoop)
                m_isStop = true;
        }
    }
}