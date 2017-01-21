using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float TileOffset;

    public GameObject[] TilePrefabs;

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
    public void GenerateLevel(int levelLength)
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
    }
}
