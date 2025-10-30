using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI endText;

    private float timer;
    private float survivalTime = 300f; 
    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
        if (endText != null)
        {
            endText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameEnded)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F1");

           
            if (timer >= survivalTime)
            {
                gameEnded = true;
                ShowEndMessage("¡VICTORIA! Sobreviviste 5 minutos");
                Time.timeScale = 0; 
            }
        }
    }

    public void UpdateHealth(int value)
    {
        healthText.text = "Health: " + value.ToString();
    }

    public void ShowEndMessage(string message)
    {
        if (endText != null)
        {
            endText.text = message;
            endText.gameObject.SetActive(true); 
        }
        gameEnded = true;
    }
}
