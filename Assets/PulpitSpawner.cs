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
    private bool hasScored = false;

    private Queue<Transform> activePulpits = new Queue<Transform>();
    private float spawnTimer;

    void Start()
    {
        ReadJSONValues(); // Initialize variables from JSON
        spawnTimer = pulpitSpawnTime; // Initialize spawn timer
        SpawnInitialPulpits(); // Create the initial setup with two pulpits
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime; // Countdown the timer

        if (spawnTimer <= 0) // When the timer reaches zero
        {
            SpawnPulpit(); // Spawn a new pulpit
            spawnTimer = pulpitSpawnTime; // Reset the timer
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasScored)
        {
            GameManager.Instance.AddScore(1); // Increase score by 1
            hasScored = true; // Ensure only one score per pulpit
        }
    }

    void SpawnInitialPulpits()
    {
        Vector3 startPosition = new Vector3(0, 0, 0); // Define the starting position
        Transform firstPulpit = Instantiate(pulpitPrefab, startPosition, Quaternion.identity).transform; // Create the first pulpit
        activePulpits.Enqueue(firstPulpit); // Add the first pulpit to the queue

        Vector3 adjacentPosition = startPosition + Vector3.right * spawnDistance; // Define the position for the second pulpit
        Transform secondPulpit = Instantiate(pulpitPrefab, adjacentPosition, Quaternion.identity).transform; // Create the second pulpit
        activePulpits.Enqueue(secondPulpit); // Add the second pulpit to the queue

        SetPulpitDestroyTimers(firstPulpit); // Set the destroy timer for the first pulpit
        SetPulpitDestroyTimers(secondPulpit); // Set the destroy timer for the second pulpit
    }

    void SpawnPulpit()
    {
        if (activePulpits.Count == 2) // Check if there are two pulpits
        {
            DestroyOldestPulpit(); // Destroy the oldest pulpit to maintain only two pulpits
        }

        Transform currentPulpitTransform = activePulpits.Peek(); // Get the most recent pulpit without removing it

        Vector3[] spawnDirections = new Vector3[] // Define possible spawn directions
        {
            Vector3.right * spawnDistance,
            Vector3.left * spawnDistance,
            Vector3.forward * spawnDistance,
            Vector3.back * spawnDistance
        };

        // Choose a random direction from the available spawn directions
        int randomIndex = Random.Range(0, spawnDirections.Length);
        Vector3 spawnPosition = currentPulpitTransform.position + spawnDirections[randomIndex];

        // Ensure the new pulpit is within a reasonable distance from the last one and does not overlap
        if (!IsPositionOccupied(spawnPosition))
        {
            Transform newPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity).transform; // Create a new pulpit
            activePulpits.Enqueue(newPulpit); // Add the new pulpit to the queue
            SetPulpitDestroyTimers(newPulpit); // Set the destroy timer for the new pulpit
        }
    }

    bool IsPositionOccupied(Vector3 position)
    {
        foreach (var pulpit in activePulpits)
        {
            if (pulpit != null && Vector3.Distance(pulpit.position, position) < 0.1f) // Check if any pulpit is too close to the given position
                return true; // Position is occupied
        }
        return false; // Position is not occupied
    }

    void DestroyOldestPulpit()
    {
        if (activePulpits.Count > 0)
        {
            Transform oldestPulpit = activePulpits.Dequeue(); // Remove the oldest pulpit from the queue
            if (oldestPulpit != null)
            {
                Destroy(oldestPulpit.gameObject); // Destroy the pulpit object
            }
        }
    }

    void SetPulpitDestroyTimers(Transform pulpit)
    {
        float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime); // Calculate a random destroy time
        StartCoroutine(DestroyPulpitAfterTime(pulpit, destroyTime)); // Start a coroutine to destroy the pulpit after a delay
    }

    System.Collections.IEnumerator DestroyPulpitAfterTime(Transform pulpit, float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime); // Wait for the destroy time

        if (pulpit != null) // Check if the pulpit still exists
        {
            Destroy(pulpit.gameObject); // Destroy the pulpit object
            if (activePulpits.Count > 0) activePulpits.Dequeue(); // Remove from queue if it's still there
        }
    }

    void ReadJSONValues()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "game_data.json"); // Define the file path
        if (File.Exists(filePath)) // Check if the file exists
        {
            string json = File.ReadAllText(filePath); // Read the file content
            JObject data = JObject.Parse(json); // Parse the JSON content

            minPulpitDestroyTime = (float)data["pulpit_data"]["min_pulpit_destroy_time"]; // Get min destroy time
            maxPulpitDestroyTime = (float)data["pulpit_data"]["max_pulpit_destroy_time"]; // Get max destroy time
            pulpitSpawnTime = (float)data["pulpit_data"]["pulpit_spawn_time"]; // Get spawn time
        }
        else
        {
            Debug.LogError("JSON file not found at: " + filePath); // Log error if file is missing
        }
    }
}
