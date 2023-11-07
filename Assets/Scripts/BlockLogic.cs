using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public bool revealed = false;
    public int x{get; set;}
    public int y{get; set;}
    public void Reveal(int[,] board, GameObject[,] objectBoard){
        if(revealed) return;
        revealed = true;
        int bomb_count = board[x, y];
        if(bomb_count > 0){
            transform.GetChild(board[x, y] - 1).gameObject.SetActive(true);
        }
        else{
            transform.position = transform.position + new Vector3(0, -0.5f, 0);
            for(int i = -1; i <= 1; i++){
                for(int j = -1; j <= 1; j++){
                    if(x + i < 0 || x + i >= board.GetLength(0)) continue;
                    if(y + j < 0 || y + j >= board.GetLength(0)) continue;
                    objectBoard[x + i, y + j].GetComponent<BlockLogic>().Reveal(board, objectBoard);
                }
            }
        }
    }
    void Start(){
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);   
        }
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
    }
}
