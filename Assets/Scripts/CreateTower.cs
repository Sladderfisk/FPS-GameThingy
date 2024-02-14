using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    [SerializeField] private Vector3 pos;
    [SerializeField] private int area;
    [SerializeField] private int height;
    [SerializeField] private float offset;

    private List<GameObject> cubes;

    private void Awake()
    {
        cubes = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) GenerateTower();
        if (Input.GetKeyDown(KeyCode.C)) DeleteAll();
    }

    private void GenerateTower()
    {
        for (int x = 0; x < area; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < area; z++)
                {
                    
                    var pos = new Vector3(this.pos.x + x * offset, this.pos.y + y * offset, this.pos.z + z * offset);
                    cubes.Add(Instantiate(cube, pos, Quaternion.identity));
                }
            }
        }
    }

    private void DeleteAll()
    {
        foreach (var game in cubes)
        {
            Destroy(game);
            
        }

        cubes = new List<GameObject>();
    }
}
