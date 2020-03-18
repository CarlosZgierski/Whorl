
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class InstantiatorWall : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField]
    Transform wallParent;
    private Tilemap map;
    public DictionaryEntry[] DictionaryEntries;
    public float yValue = 0.93f;



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
        var PrefabToYPlusDictionary = new Dictionary<Tile, float>();
        var FlippedByTile = new Dictionary<Tile, bool>();
        foreach (DictionaryEntry entry in DictionaryEntries)
        {
            TileToPrefabDictionary.Add(entry.tile, entry.prefab);
            PrefabToYPlusDictionary.Add(entry.tile, entry.YPlus);
            FlippedByTile.Add(entry.tile, entry.flipped);

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
                        instatiatedGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        instatiatedGameObject.transform.parent = wallParent;
                        
                        
                        instatiatedGameObject.transform.position = place + LocalCenterPosition + new Vector3(0, yValue, 0);
                        instatiatedGameObject.transform.SetAsLastSibling();

                        if(FlippedByTile[tile])
                        {
                            Vector3 p = instatiatedGameObject.transform.localScale;
                            instatiatedGameObject.transform.localScale = new Vector3(-p.x, p.y, p.z);
                        }

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
        public bool flipped = false;
    }
#endif

}


#if UNITY_EDITOR
[CustomEditor(typeof(InstantiatorWall))]
public class InstantiatorWallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InstantiatorWall myScript = (InstantiatorWall)target;
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
