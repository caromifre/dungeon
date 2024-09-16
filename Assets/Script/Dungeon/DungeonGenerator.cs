using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    // [SerializeField] Vector2 _dungeonSize;
    [SerializeField] int _x,_y;
    [SerializeField] int _startPos = 0;

    [SerializeField] GameObject[]  _rooms;
    [SerializeField] Vector2 offset;
    
    //List<Cell> _board;
    Cell[,] _board;
   
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }


    void GenerateDungeon()
    {
        /*for(int i = 0; i < _dungeonSize.x; i++)
        {
            for(int j = 0; j < _dungeonSize.y; j++)
            {
                Cell currentCell = _board[Mathf.FloorToInt(i+j*_dungeonSize.x)];

                if(currentCell.visited)
                {
                    int randomRoom = Random.Range(0,_rooms.Length) ;

                    GameObject newRoom =   Instantiate(_rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y),Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }*/

    }

    public void MazeGenerator()
    {
        //crea un tablero en blanco
        _board = new Cell[_x, _y];
        
        //armar laberinto
        for (int a = 0; a < _board.GetLength(0); a++) {
            for (int b=0; b < _board.GetLength(1); b++) {
                
            }
        }
      
    }



    
    //Chequea las celdas vecinas
   /* List<int> CheckNeighbors(int cell)
    {
        //Debug.Log("numero de celda: " + cell + " numnero de celda + 1: " + (cell + 1) + " ancho del dungeon: " + _dungeonSize.x + " modulo cell y ancho: " + cell % _dungeonSize.x);
        List<int> neighbors = new List<int>();
        
        //check Up
        if(cell - _dungeonSize.x >= 0 && !_board[Mathf.FloorToInt(cell - _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - _dungeonSize.x));
        }
        
        //check Down
        if(cell + _dungeonSize.x < _board.Count && !_board[Mathf.FloorToInt(cell + _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + _dungeonSize.x));
        }

        //check Right
        if((cell + 1) % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check Left
        if(cell % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
       // Debug.Log("return de la funcion: " + neighbors);
        return neighbors;
    }*/


    private void OnGUI() 
    {
        float w = Screen.width/2;
        float h = Screen.height - 80;
        if(GUI.Button(new Rect(w,h,250,50), "Regenerate Dungeon"))
        {
            RegenerateDungeon();
        }
    }

    void RegenerateDungeon()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
