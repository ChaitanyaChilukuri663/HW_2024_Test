using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float minPulpitDestroyTime = 4.0f;
    public float maxPulpitDestroyTime = 5.0f;
    public float pulpitSpawnTime = 2.5f;
    public float spawnDistance = 3.0f;

    private Queue<Transform> activePulpits = new Queue<Transform>();
    private float spawnTimer;

    void Start()
    {
        ReadJSONValues();
        spawnTimer = pulpitSpawnTime;
        SpawnInitialPulpits();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnPulpit();
            spawnTimer = pulpitSpawnTime;
        }
    }

    void SpawnInitialPulpits()
    {
        Vector3 startPosition = new Vector3(0, 0, 0);
        Transform firstPulpit = Instantiate(pulpitPrefab, startPosition, Quaternion.identity).transform;
        activePulpits.Enqueue(firstPulpit);

        Vector3 adjacentPosition = startPosition + Vector3.right * spawnDistance;
        Transform secondPulpit = Instantiate(pulpitPrefab, adjacentPosition, Quaternion.identity).transform;
        activePulpits.Enqueue(secondPulpit);

        SetPulpitDestroyTimers(firstPulpit);
        SetPulpitDestroyTimers(secondPulpit);
    }

     void SpawnPulpit()
    {
        if (activePulpits.Count == 2)
        {
            DestroyOldestPulpit();
        }

        Transform currentPulpitTransform = activePulpits.Peek();

        Vector3[] spawnDirections = new Vector3[]
        {
            Vector3.right * spawnDistance,
            Vector3.left * spawnDistance,
            Vector3.forward * spawnDistance,
            Vector3.back * spawnDistance
        };

        int randomIndex = Random.Range(0, spawnDirections.Length);
        Vector3 spawnPosition = currentPulpitTransform.position + spawnDirections[randomIndex];

        if (!IsPositionOccupied(spawnPosition))
        {
            Transform newPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity).transform;
            activePulpits.Enqueue(newPulpit);
            SetPulpitDestroyTimers(newPulpit);

            UpdateOldestPulpitTimer();
        }
    }


    bool IsPositionOccupied(Vector3 position)
    {
        foreach (var pulpit in activePulpits)
        {
            if (pulpit != null && Vector3.Distance(pulpit.position, position) < spawnDistance)
                return true;
        }
        return false;
    }


    void DestroyOldestPulpit()
    {
        if (activePulpits.Count > 0)
        {
            Transform oldestPulpit = activePulpits.Dequeue();
            if (oldestPulpit != null)
            {
                Destroy(oldestPulpit.gameObject);
            }
        }
    }

    void SetPulpitDestroyTimers(Transform pulpit)
    {
        float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
        StartCoroutine(DestroyPulpitAfterTime(pulpit, destroyTime));
    }

    System.Collections.IEnumerator DestroyPulpitAfterTime(Transform pulpit, float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);

        if (pulpit != null)
        {
            Destroy(pulpit.gameObject);
            if (activePulpits.Count > 0) activePulpits.Dequeue();
        }
    }

    void UpdateOldestPulpitTimer()
    {
        if (activePulpits.Count > 0)
        {
            Transform oldestPulpit = activePulpits.Peek();
            PulpitTimer pulpitTimer = oldestPulpit.GetComponent<PulpitTimer>();
            if (pulpitTimer != null)
            {
                pulpitTimer.enabled = true;
            }
        }
    }

    void ReadJSONValues()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "game_data.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JObject data = JObject.Parse(json);

            minPulpitDestroyTime = (float)data["pulpit_data"]["min_pulpit_destroy_time"];
            maxPulpitDestroyTime = (float)data["pulpit_data"]["max_pulpit_destroy_time"];
            pulpitSpawnTime = (float)data["pulpit_data"]["pulpit_spawn_time"];
        }
        else
        {
            Debug.LogError("JSON file not found at: " + filePath);
        }
    }
}
