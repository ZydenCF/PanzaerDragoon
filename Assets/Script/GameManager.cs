using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private List<EnemyBase> enemies = new List<EnemyBase>();
    private Dictionary<string, int> enemyScores = new Dictionary<string, int>();

    public delegate void GameEvent();
    public event GameEvent OnPlayerDead;
    public event GameEvent OnAllEnemiesDefeated;
    public event GameEvent OnBossDefeated;
    public event GameEvent OnTimeFinished;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        enemyScores.Add("EnemyShooter", 10);
        enemyScores.Add("EnemyCharger", 15);
        enemyScores.Add("MiniBoss", 50);
    }

    public void OnEnemyDeadEvent(EnemyBase enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            if (OnAllEnemiesDefeated != null)
            {
                OnAllEnemiesDefeated();
            }
        }
    }

    public void OnPlayerDeadEvent()
    {
        if (OnPlayerDead != null)
        {
            OnPlayerDead();
        }

        UIManager.Instance.ShowEndMessage("GAME OVER - Has muerto");
        Time.timeScale = 0;
    }

    public void OnBossDefeatedEvent()
    {
        if (OnBossDefeated != null)
        {
            OnBossDefeated();
        }

        UIManager.Instance.ShowEndMessage("¡BOSS DERROTADO! Increíble");
    }

    public void OnTimeFinishedEvent()
    {
        if (OnTimeFinished != null)
        {
            OnTimeFinished();
        }

        UIManager.Instance.ShowEndMessage("TIEMPO AGOTADO");
    }
}
