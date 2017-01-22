using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float TileOffset;

    public GameObject[] TilePrefabs;
    public GameObject EndingTile;
    public GameObject[] EnemyPrefabs;
    public GameObject CarPrefab;

	void Start()
    {
		
	}
	
	void Update()
    {
		
	}

    /// <summary>
    /// Generate the level, given the number of tiles.
    /// </summary>
    /// <param name="levelLength"></param>
    /// <param name="enemies"></param>
    /// <param name="cars"></param>
    public void GenerateLevel(int levelLength, int enemies, int cars)
    {
        float zOffset = 0;

        for (int i = 0; i < levelLength; ++i)
        {
            // Take a random tile and place it in the world
            int randomTileIndex = Random.Range(0, TilePrefabs.Length);
            GameObject randomTilePrefab = TilePrefabs[randomTileIndex];

            GameObject randomTile = Instantiate(randomTilePrefab);

            randomTile.transform.position = new Vector3(0, 0, zOffset);

            zOffset += TileOffset;
        }

        // Spawn level ending tile
        GameObject endingTile = Instantiate(EndingTile);

        endingTile.transform.position = new Vector3(0, 0, zOffset);

        // Enemy spawning. Uses zOffset as helper
        // Divide the enemy count into 5 sections, but randomize the position within the section

        var enemySections = Mathf.Min(levelLength, enemies);
        var startOffset = 15f;

        var possibleStartXs = new List<float> {-1.5f, -1, -0.5f, 0.5f, 1};
        var startZs = new List<float>();

        var sectionSize = (zOffset - startOffset) / enemySections;

        for (var i = 0; i < enemySections; ++i)
        {
            startZs.Add(startOffset + sectionSize * i);
        }

        var enemiesPerSection = new List<int>();
        for (var i = 0; i < enemySections; ++i)
        {
            enemiesPerSection.Add(0);
        }

        var enemiesAdded = 0;
        while (enemiesAdded < enemies)
        {
            enemiesPerSection[enemiesAdded % enemySections]++;
            enemiesAdded++;
        }

        for (var i = 0; i < enemySections; ++i)
        {
            for (var j = 0; j < enemiesPerSection[i]; j++)
            {
                var randNormal = RandomFromDistribution.RandomRangeNormalDistribution(0.25f, 0.75f,
                    RandomFromDistribution.ConfidenceLevel_e._99);

                var spawnZ = startZs[i] + randNormal * TileOffset;
                var spawnX = possibleStartXs[Random.Range(0, 5)];

                var randomEnemyPrefab = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)];

                var enemy = Instantiate(randomEnemyPrefab);
                enemy.transform.position = new Vector3(spawnX, 0f, spawnZ);

                enemy.GetComponent<EnemyController>().enabled = false;
                enemy.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        // Car spawning
        // Divide active area into equal chunks
        // Place cars on normal distributions along the chunk

        var carSectionSize = (zOffset - startOffset) / cars;
        
        var startXPerSide = new Dictionary<int, float>();
        startXPerSide[1] = 17.5f;
        startXPerSide[-1] = 8.5f;

        for (var i = 0; i < cars; ++i)
        {
            var randStart = RandomFromDistribution.RandomRangeNormalDistribution(0.25f, 0.75f,
                RandomFromDistribution.ConfidenceLevel_e._99);
            var startZ = startOffset + carSectionSize * (i + randStart);
            var randDirection = Random.Range(0, 2)*2 -1; // -1 or 1

            var car = Instantiate(CarPrefab);

            car.transform.position += new Vector3(startXPerSide[randDirection], 0f, startZ);

            car.GetComponent<Car>().Direction = randDirection;

            car.GetComponent<Car>().enabled = false;
            car.transform.Find("pCube1").gameObject.SetActive(false);
        }
    }
}
