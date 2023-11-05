using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bomb;
    void InitializeBoard(){
    }
    void Start(){
        GameObject one = GameObject.FindGameObjectWithTag("1");
        for(int i = 0; i < 25; i++){
            for(int j = 0; j < 25; j++){
                Vector3 position = new Vector3(j, 0, i);
                Instantiate(one, position, Quaternion.identity);
            }
        }
    }

    void Update(){
        
    }
}
