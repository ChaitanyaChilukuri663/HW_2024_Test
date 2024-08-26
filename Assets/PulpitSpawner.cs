using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json.Linq;
public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;
    private float minDestroyTime = 4f;
    private float maxDestroyTime = 5f;
    private float pulpitSpawnTime = 2.5f;
    private GameObject lastPulpit;

    void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/doofus_diary.json");
        JObject pulpitData = JObject.Parse(json);
        minDestroyTime = (float)pulpitData["pulpit_data"]["min_pulpit_destroy_time"];
        maxDestroyTime = (float)pulpitData["pulpit_data"]["max_pulpit_destroy_time"];
        pulpitSpawnTime = (float)pulpitData["pulpit_data"]["pulpit_spawn_time"];
        StartCoroutine(SpawnPulpit());
    }
    IEnumerator SpawnPulpit()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulpitSpawnTime);
            
            Vector3 spawnPosition = Vector3.zero;
            if (lastPulpit != null)
            {
                spawnPosition = lastPulpit.transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            }
            lastPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);
            Destroy(lastPulpit, Random.Range(minDestroyTime, maxDestroyTime));
        }
    }
}
