using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "game_data.json"));
        JObject playerData = JObject.Parse(json);
        speed = (float)playerData["player_data"]["speed"];
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);

        if (transform.position.y < -1)
        {
            GameManager.Instance.GameOver(GameManager.Instance.GetScore());
        }
    }

    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Pulpit"))
    {
        
        ScoreManager.instance.IncreaseScore(1); 
    }
}

}
