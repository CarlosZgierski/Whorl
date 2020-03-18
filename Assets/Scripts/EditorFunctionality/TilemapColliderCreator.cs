
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Linq;

[RequireComponent(typeof(Tilemap))]
public class TilemapColliderCreator : MonoBehaviour
{
    public GridLayout layout;
    public Tilemap tileMap;
    public List<Vector3> view;
    public int ZTiles = 0;



    public void BuildColliders()
    {
        layout = GetComponent<GridLayout>();
        tileMap = GetComponent<Tilemap>();
        Vector3 LayoutDefaultScale = layout.transform.localScale;
        layout.transform.localScale = new Vector3(1, 1, 1);
        //CleanColliders();
        List<Vector3> RemainingTiles = new List<Vector3>();
        CreateVerticalColliders(out RemainingTiles);
        CreateHorizontalAndSingleColliders(RemainingTiles);
        layout.transform.localScale = LayoutDefaultScale;

    }

    public void CreatePivot()
    {
        Vector3[] tiles = GetAllTilesPosition();
        GameObject x = new GameObject();
        x.name = gameObject.name + " pivot";
        x.transform.position = tiles[0];
    
    }

    public void CreateVerticalColliders(out List<Vector3> RemainingTiles)
    {
        RemainingTiles = new List<Vector3>();
        Vector3 LocalCenterPosition = new Vector3(layout.cellSize.x, 0, layout.cellSize.y) / 2;
        Vector3[] tiles = GetAllTilesPosition();
        for (int i = 0; i < tiles.Length; i ++)
        {
            int multiplicador = 1;
            bool doneSection = false;

            while (!doneSection)
            {
                if (i + multiplicador < tiles.Length)
                {
                    if (tiles[i + multiplicador].x == tiles[i].x)
                    {
                        if (tiles[i + multiplicador].z == tiles[i].z + layout.cellSize.y * multiplicador)
                        {
                            multiplicador++;

                        }
                        else
                        {
                            doneSection = true;
                        }
                    }
                    else
                    {
                        doneSection = true;

                    }
                }
                else
                {
                    doneSection = true;
                }
            }

            //Instantiate(prefab, x + pa , Quaternion.identity);
            
            
            if(multiplicador > 1)
            {
                /*
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = (tiles[i] + tiles[i + multiplicador - 1]) / 2 + LocalCenterPosition;
                cube.transform.localScale = LocalCenterPosition * 2 + new Vector3(0, 5, (multiplicador - 1) * layout.cellSize.y);
                cube.transform.parent = transform;
                cube.GetComponent<Renderer>().enabled = false;
                */

                BoxCollider box = gameObject.AddComponent<BoxCollider>();
                
                box.center = (tiles[i] + tiles[i + multiplicador - 1]) / 2 + LocalCenterPosition - transform.position;
                
                box.size = LocalCenterPosition * 2 + new Vector3(0, 5, (multiplicador - 1) * layout.cellSize.y);
      
                

                i += multiplicador - 1;

            }
            else
            {
                RemainingTiles.Add(tiles[i]);
                //box.center = (tiles[i] + LocalCenterPosition);
                //box.size = LocalCenterPosition * 2 + new Vector3(0, 5, 0);

            }
            
            
            multiplicador = 0;
            
        }
    }

    public void CreateHorizontalAndSingleColliders(List<Vector3> RemainingTiles)
    {
        view.Clear();
        view = RemainingTiles.OrderBy(x => x.z).ThenBy(z => z.x).ToList();
        RemainingTiles = view;
        Vector3 LocalCenterPosition = new Vector3(layout.cellSize.x, 0, layout.cellSize.y) / 2;
        Vector3[] tiles = RemainingTiles.ToArray();
        for (int i = 0; i < tiles.Length; i++)
        {
            int multiplicador = 1;
            bool doneSection = false;

            while (!doneSection)
            {
                if (i + multiplicador < tiles.Length)
                {
                    if (tiles[i + multiplicador].z == tiles[i].z)
                    {
                        if (tiles[i + multiplicador].x == tiles[i].x + layout.cellSize.x * multiplicador)
                        {
                            multiplicador++;

                        }
                        else
                        {
                            doneSection = true;
                        }
                    }
                    else
                    {
                        doneSection = true;

                    }
                }
                else
                {
                    doneSection = true;
                }
            }

            //Instantiate(prefab, x + pa , Quaternion.identity);

            
            if (multiplicador > 1)
            {
                /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = (tiles[i] + tiles[i + multiplicador - 1]) / 2 + LocalCenterPosition;
                cube.transform.localScale = LocalCenterPosition * 2 + new Vector3((multiplicador - 1) * layout.cellSize.x, 5, 0);
                cube.transform.parent = transform;
                cube.GetComponent<Renderer>().enabled = false;
                */

                BoxCollider box = gameObject.AddComponent<BoxCollider>();

                box.center = (tiles[i] + tiles[i + multiplicador - 1]) / 2 + LocalCenterPosition - transform.position ;
               
                box.size = LocalCenterPosition * 2 + new Vector3((multiplicador - 1) * layout.cellSize.x, 5, 0);
          
                

                i += multiplicador - 1;

            }
            else
            {
                /*
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = (tiles[i] + tiles[i + multiplicador - 1]) / 2 + LocalCenterPosition;
                cube.transform.localScale = LocalCenterPosition * 2 + new Vector3(0, 5, (multiplicador - 1) * layout.cellSize.y);
                cube.transform.parent = transform;
                cube.GetComponent<Renderer>().enabled = false;
                */
                BoxCollider box = gameObject.AddComponent<BoxCollider>();
                box.center = (tiles[i] + LocalCenterPosition) - transform.position;
                box.size = LocalCenterPosition * 2 + new Vector3(0, 5, 0);
               

            }


            multiplicador = 0;

        }
    }

    public void CleanColliders()
    {
        foreach(BoxCollider x in GetComponents<BoxCollider>())
        {
           DestroyImmediate(x);
        }
        foreach(Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if(t != transform)
            DestroyImmediate(t.gameObject);
        }
    }

    public Vector3[] GetAllTilesPosition()
    {
        List<Vector3> availablePlaces = new List<Vector3>();
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, ZTiles));
                //Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        return availablePlaces.ToArray();
    }

    public static T[] GetTiles<T>(Tilemap tilemap) where T : TileBase
    {
        List<T> tiles = new List<T>();
        
        for (int y = tilemap.origin.y; y < (tilemap.origin.y + tilemap.size.y); y++)
        {
            for (int x = tilemap.origin.x; x < (tilemap.origin.x + tilemap.size.x); x++)
            {
                T tile = tilemap.GetTile<T>(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
        }
        return tiles.ToArray();
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(TilemapColliderCreator))]
public class TileMapColliderCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TilemapColliderCreator myScript = (TilemapColliderCreator)target;
        if (GUILayout.Button("Clean Colliders"))
        {
            myScript.CleanColliders();
        }
        if (GUILayout.Button("Build Colliders"))
        {
            myScript.BuildColliders();
        }
        if (GUILayout.Button("Create Pivot"))
        {
            myScript.CreatePivot();
        }
        DrawDefaultInspector();

        
    }
}

#endif
