using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int _width, _height, _extraGrids;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private GameObject tilesHolder;

    [SerializeField] private GameObject treesHolder;

    [SerializeField] private GameObject[] treesPrefabs;

    public void GenerateGrid(){
        int totalwidth = _width + _extraGrids * 2;
        int totalheight = _height + _extraGrids * 2;

        for(int x=0; x<totalwidth; x++){
            for(int y=0; y<totalheight; y++){
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,0,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = tilesHolder.transform;
                
                if(x>_extraGrids-1 && x<totalwidth-_extraGrids && y>_extraGrids-1 && y<totalheight-_extraGrids){
                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);
                }
                else if(x!=0 || y!=0){
                    if(Random.Range(0,20) == 0){
                        int index = Random.Range(0, treesPrefabs.Length);

                        var spawnedTree = Instantiate(treesPrefabs[index], new Vector3(x,1,y), Quaternion.identity);
                        spawnedTree.name = $"Tree {x} {y}";
                        spawnedTree.transform.parent = treesHolder.transform;
                        float locScale = Random.Range(0.5f, 1f);
                        spawnedTree.transform.localScale = new Vector3(locScale,locScale,locScale);
                    }
                }
            }
        }
    }
}
