using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/doofus_diary.json");
        JObject playerData = JObject.Parse(json);
        speed = (float)playerData["player_data"]["speed"];
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Pulpit"))
    {
        FindObjectOfType<ScoreManager>().IncrementScore();
    }
}
}
