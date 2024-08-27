using UnityEngine;
using TMPro;

public class PulpitTimer : MonoBehaviour
{
    public float timeToLive = 5.0f;  
    private float countdown;  
    private TextMeshProUGUI timerText;  

    void Start()
    {
        countdown = timeToLive;  
        timerText = GetComponentInChildren<TextMeshProUGUI>();  
        if (timerText == null)
        {
            Debug.LogError("TimerText component not found in children of Pulpit prefab.");
        }
    }

    void Update()
    {
     
        countdown -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = countdown.ToString("F2") + "s";  
        }

        if (countdown <= 0f)
        {
            Destroy(gameObject); 
        }
    }

    public void SetCountdown(float time)
    {
        timeToLive = time;
        countdown = timeToLive;
    }
}
