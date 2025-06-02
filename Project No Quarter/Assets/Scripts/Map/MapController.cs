using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius = 0.9f;  // Radius for the terrain check
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;
    const float deadzone = 0.5f;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDuration;



    void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (currentChunk == null) return;

        Vector2 dir = pm.moveDir;
        // “pure right”: x > deadzone, y is near zero
        if (dir.x > deadzone && Mathf.Abs(dir.y) < deadzone)
        {
            CheckAndSpawn("Right");
        }
        // “pure left”: x < -deadzone, y near zero
        else if (dir.x < -deadzone && Mathf.Abs(dir.y) < deadzone)
        {
            CheckAndSpawn("Left");
        }
        // “pure up”: y > deadzone, x near zero
        else if (dir.y > deadzone && Mathf.Abs(dir.x) < deadzone)
        {
            CheckAndSpawn("Up");
        }
        // “pure down”: y < -deadzone, x near zero
        else if (dir.y < -deadzone && Mathf.Abs(dir.x) < deadzone)
        {
            CheckAndSpawn("Down");
        }
        // diagonal: x > deadzone && y > deadzone
        else if (dir.x > deadzone && dir.y > deadzone)
        {
            CheckAndSpawn("Right Up");
        }
        // diagonal: x < -deadzone && y > deadzone
        else if (dir.x < -deadzone && dir.y > deadzone)
        {
            CheckAndSpawn("Left Up");
        }
        // diagonal: x < -deadzone && y < -deadzone
        else if (dir.x < -deadzone && dir.y < -deadzone)
        {
            CheckAndSpawn("Left Down");
        }
        // diagonal: x > deadzone && y < -deadzone
        else if (dir.x > deadzone && dir.y < -deadzone)
        {
            CheckAndSpawn("Right Down");
        }
        // if no direction is detected, do nothing
    }

    void CheckAndSpawn(string childName)
    {
        Transform marker = currentChunk.transform.Find(childName);
        if (marker == null)
        {
            Debug.LogWarning($"Chunk is missing child '{childName}'.");
            return;
        }
        Vector3 checkPos = marker.position;
        if (!Physics2D.OverlapCircle(checkPos, checkerRadius, terrainMask))
        {
            noTerrainPosition = checkPos;
            SpawnChunk();
        }
    }


    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown   -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return; // Skip optimization if cool down is not finished
        }




        foreach (GameObject chunk in spawnedChunks)
        {
            if (chunk == null) continue; // Skip if the chunk is already destroyed

            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }

        }
    }
}
