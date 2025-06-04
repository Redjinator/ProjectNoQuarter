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
        if (!currentChunk) return;

        Vector2[] directions = new Vector2[]
        {
            Vector2.right, Vector2.left,
            Vector2.up, Vector2.down,
            Vector2.right + Vector2.up,
            Vector2.right + Vector2.down,
            Vector2.left + Vector2.up,
            Vector2.left + Vector2.down
        };

        foreach (var dir in directions)
        {
            string dirName = DirectionToName(dir);
            Transform edge = currentChunk.transform.Find(dirName);
            if (edge && !Physics2D.OverlapCircle(edge.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = edge.position;
                SpawnChunk();
            }
        }
    }

    string DirectionToName(Vector2 dir)
    {
        if (dir == Vector2.right) return "Right";
        if (dir == Vector2.left) return "Left";
        if (dir == Vector2.up) return "Up";
        if (dir == Vector2.down) return "Down";
        if (dir == Vector2.right + Vector2.up) return "Right Up";
        if (dir == Vector2.right + Vector2.down) return "Right Down";
        if (dir == Vector2.left + Vector2.up) return "Left Up";
        if (dir == Vector2.left + Vector2.down) return "Left Down";
        return "";
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
