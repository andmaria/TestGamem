using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject[] cardPrefabs;
    public int countCard = 15;
    private int i;

    void Start()
    {
        Index.Shuffle(spawnPoint);
        Index.Shuffle(cardPrefabs);
        Spawn();
    }

    public void Spawn()
    {
        for (i = 0; i < countCard; i++)
        {
            GameObject obj = cardPrefabs[i];
            Transform pos = spawnPoint[i];
            Instantiate(obj, pos.position, pos.rotation);
        }
    }
}

  
    
   