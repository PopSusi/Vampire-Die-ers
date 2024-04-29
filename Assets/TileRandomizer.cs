using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRandomizer : MonoBehaviour
{
    [SerializeField] Tile randTile;
    Tilemap tm;
    BoundsInt bounds;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<Tilemap>();
        TileBase[] allTiles = tm.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null)
                {
                    tm.SetTile(new Vector3Int(x, y, 0), randTile);//DRAW;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
