using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bomb;
    public GameObject block;
    GameObject[,] objectBoard = new GameObject[25, 25];
    private int[,] board;
    int[,] UpdateBoard(int[,] board, int x, int y){
        int bomb_count = 0;
        for(int i = -1; i <= 1; i++){
            for(int j = -1; j <= 1; j++){
                if(x + i < 0 || x + i >= board.GetLength(0)) continue;
                if(y + j < 0 || y + j >= board.GetLength(0)) continue;
                if(board[x + i, y + j] == 9){
                    bomb_count++;
                }
            }
        }
        board[x, y] = bomb_count;
        return board;
    }
    int[,] MakeBoard(){
        int[,] board = new int[25, 25];
        System.Random r = new System.Random();
        for(int i = 0; i < 1; i++){
            board[r.Next(0, 25), r.Next(0, 25)] = 9;
        }
        for(int i = 0; i < 25; i++) {
            for(int j = 0; j < 25; j++) {
                if (board[i, j] == 9) continue;
                board = UpdateBoard(board, i, j);
            }  
        }
        return board;
    }
    int[,] InitializeBoard(){
        int[,] board = MakeBoard();
        for(int i = 0; i < 25; i++) {
            for(int j = 0; j < 25; j++){
                if(board[i, j] == 9){
                    GameObject o = Instantiate(bomb, new Vector3(j, 0, i), Quaternion.identity);
                    objectBoard[i, j] = o;
                } 
                else{
                    GameObject o = Instantiate(block, new Vector3(j, 0, i), Quaternion.identity);
                    o.GetComponent<BlockLogic>().x = i;
                    o.GetComponent<BlockLogic>().y = j;
                    objectBoard[i, j] = o;
                }
            }
        }
        return board;
    }
    void Start(){
        this.board = InitializeBoard();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100f)){
                hit.transform.gameObject.GetComponent<BlockLogic>().Reveal(board, objectBoard);
            }
        } 
    }
}