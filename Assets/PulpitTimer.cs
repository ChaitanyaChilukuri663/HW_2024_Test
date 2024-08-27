using UnityEngine;
using UnityEngine.UI;

public class PulpitTimer : MonoBehaviour
{
    public float timeToLive = 5.0f;  
    private float countdown;
    private Text timerText;  

    void Start()
    {
        countdown = timeToLive;  
        timerText = GetComponentInChildren<Text>();  
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
}
