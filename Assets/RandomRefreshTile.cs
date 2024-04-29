using System;
using UnityEngine.Tilemaps;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu()]
[Serializable]
public class RandomRefreshTile : Tile
{
    /// <summary>
    /// The Sprites used for randomizing output.
    /// </summary>
    [SerializeField]
    public Sprite[] m_Sprites;

    /// <summary>
    /// Retrieves any tile rendering data from the scripted tile.
    /// </summary>
    /// <param name="position">Position of the Tile on the Tilemap.</param>
    /// <param name="tilemap">The Tilemap the tile is present on.</param>
    /// <param name="tileData">Data to render the tile.</param>
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        if ((m_Sprites != null) && (m_Sprites.Length > 0))
        {
            int idx = Random.Range(0, m_Sprites.Length);
            tileData.sprite = m_Sprites[idx];
        }
    }
}