
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Instantiator : MonoBehaviour
{
#if UNITY_EDITOR
    private Tilemap map;
    public DictionaryEntry[] DictionaryEntries;



    public void InstantiateTilesAndClean()
    {
        InstantiateByTiles();
        CleanTiles();
    }

    public void InstantiateByTiles() 
    {
        map = GetComponent<Tilemap>();
        Vector3 LocalCenterPosition = new Vector3(map.cellSize.x, 0, map.cellSize.y) / 2;
        var TileToPrefabDictionary = new Dictionary<Tile, GameObject>();
        var PrefabToYPlusDictionary = new Dictionary<GameObject, float>();
        foreach (DictionaryEntry entry in DictionaryEntries)
        {
            TileToPrefabDictionary.Add(entry.tile, entry.prefab);
            PrefabToYPlusDictionary.Add(entry.prefab, entry.YPlus);
        }

        for (int y = map.origin.y; y < (map.origin.y + map.size.y); y++)
        {
            for (int x = map.origin.x; x < (map.origin.x + map.size.x); x++)
            {
                Tile tile = map.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    if (TileToPrefabDictionary.ContainsKey(tile))
                    {
                        Vector3Int localPlace = new Vector3Int(x, y, 0);
                        //Vector3Int localPlace = (new Vector3Int(tile., p, (int)tileMap.transform.position.y));
                        Vector3 place = map.CellToWorld(localPlace);
                        GameObject instatiatedGameObject = PrefabUtility.InstantiatePrefab(TileToPrefabDictionary[tile] as GameObject) as GameObject;
                        instatiatedGameObject.transform.position = place + LocalCenterPosition + new Vector3(0, PrefabToYPlusDictionary[TileToPrefabDictionary[tile]], 0);
                        instatiatedGameObject.transform.SetAsLastSibling();
                        //Instantiate(TileToPrefabDictionary[tile], place + LocalCenterPosition + new Vector3(0, PrefabToYPlusDictionary[TileToPrefabDictionary[tile]],0), Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogWarning("Prefab of tile " + tile.name + " not found!");
                    }
                }
            }
        }
        
    }

    public void CleanTiles()
    {
        map = GetComponent<Tilemap>();
        map.ClearAllTiles();
    }

    [System.Serializable]
    public class DictionaryEntry
    {
        public string name;
        public Tile tile;
        public GameObject prefab;
        public float YPlus;
    }
#endif

}


#if UNITY_EDITOR
[CustomEditor(typeof(Instantiator))]
public class InstantiatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Instantiator myScript = (Instantiator)target;
        if (GUILayout.Button("Instantiate and Clean Tiles"))
        {
            myScript.InstantiateTilesAndClean();
        }
        if (GUILayout.Button("Instantiate but Keep Tiles"))
        {
            myScript.InstantiateByTiles();
        }
        if (GUILayout.Button("Clean Tiles"))
        {
            myScript.CleanTiles();
        }

    }
}
#endif
