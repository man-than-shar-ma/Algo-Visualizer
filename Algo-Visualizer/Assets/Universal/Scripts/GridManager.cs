using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height, _extraGrids;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private GameObject TilesHolder;

    // public NavMeshSurface surface;

    // Start is called before the first frame update
    void Awake()
    {
        //This will generate the grid from the tiles prefab
        GenerateGrid();
        // surface.BuildNavMesh();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void GenerateGrid(){
        int totalwidth = _width + _extraGrids * 2;
        int totalheight = _height + _extraGrids * 2;

        for(int x=0; x<totalwidth; x++){
            for(int y=0; y<totalheight; y++){
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,0,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = TilesHolder.transform;
                
                if(x>_extraGrids-1 && x<totalwidth-_extraGrids && y>_extraGrids-1 && y<totalheight-_extraGrids){
                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);
                }
            }
        }
    }
}
