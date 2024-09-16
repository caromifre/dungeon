using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constantes_celda;
public class MiFD_generator3 : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
        public bool[] puerta = new bool[4];
    }

    [SerializeField] Vector2 _dungeonSize;
    [SerializeField] int _startPos = 0;

    [SerializeField] GameObject[] _rooms;
    [SerializeField] Vector2 offset;

    List<Cell> _board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }


    void GenerateDungeon()
    {
        for (int i = 0; i < _dungeonSize.x; i++)
        {
            for (int j = 0; j < _dungeonSize.y; j++)
            {
                Cell currentCell = _board[Mathf.FloorToInt(i + j * _dungeonSize.x)];

                if (currentCell.visited)
                {
                    int randomRoom = Random.Range(0, _rooms.Length);

                    GameObject newRoom = Instantiate(_rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity) as GameObject;
                    Propiedades_celda rb = newRoom.GetComponent<Propiedades_celda>();
                    rb.actualizar_celda(_PUERTAS, currentCell.puerta);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }

    }

    public void MazeGenerator()
    {
        //Create Dungeon board
        _board = new List<Cell>();

        /* for(int i = 0; i < _dungeonSize.x; i++)
         {
             for(int j = 0; j < _dungeonSize.y; j++)
             {
                 _board.Add(new Cell());
             }
         }*/

        float boardLenght = _dungeonSize.x * _dungeonSize.y;

        for (int i = 0; i < boardLenght; i++)
        {
            _board.Add(new Cell());
        }

        //Create Dungeon Maze
        //StarPosition determina el casillero donde el arranca el Dungeon
        int currentCell = _startPos;

        //Generamos la Pila(Stack) donde armaremos el Laberinto
        Stack<int> path = new Stack<int>();

        int k = 0;
        //while(k < 1000)
        while (currentCell != _board.Count - 1)
        {
            //Debug.Log("Interaciones: " + k++);

            //marca la celda actual como visitada
            _board[currentCell].visited = true;

            //salida de emergencia si entra en bucle infinito
            if (k >= _board.Count * 3)
            {
                break;
            }

            //Check Neighbors cells
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                Debug.Log("valor de newcell: " + newCell);
                if (newCell > currentCell)
                {
                    //down or right
                    Debug.Log("valor newcell abajo o darecha: " + newCell + "valor currentcell: " + currentCell);
                    if (newCell - 1 == currentCell)
                    {
                        _board[currentCell].puerta[2] = true;
                        currentCell = newCell;
                        _board[currentCell].puerta[3] = true;
                    }
                    else
                    {
                        _board[currentCell].puerta[1] = true;
                        currentCell = newCell;
                        _board[currentCell].puerta[0] = true;
                    }
                }
                else
                {
                    //up or left
                    Debug.Log("valor newcell arriba o izq: " + newCell + "valor currentcell: " + currentCell);
                    if (newCell + 1 == currentCell)
                    {
                        _board[currentCell].puerta[3] = true;
                        currentCell = newCell;
                        _board[currentCell].puerta[2] = true;
                    }
                    else
                    {
                        _board[currentCell].puerta[0] = true;
                        currentCell = newCell;
                        _board[currentCell].puerta[1] = true;
                    }
                }

            }
        }

        //Instantiate rooms
        GenerateDungeon();

    }




    //Chequea las celdas vecinas
    List<int> CheckNeighbors(int cell)
    {
        //Debug.Log("numero de celda: " + cell + " numnero de celda + 1: " + (cell + 1) + " ancho del dungeon: " + _dungeonSize.x + " modulo cell y ancho: " + cell % _dungeonSize.x);
        List<int> neighbors = new List<int>();

        //check Up
        if (cell - _dungeonSize.x >= 0 && !_board[Mathf.FloorToInt(cell - _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - _dungeonSize.x));
        }

        //check Down
        if (cell + _dungeonSize.x < _board.Count && !_board[Mathf.FloorToInt(cell + _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + _dungeonSize.x));
        }

        //check Right
        if ((cell + 1) % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check Left
        if (cell % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
        //Debug.Log("return de la funcion: " + neighbors);
        return neighbors;
    }


    private void OnGUI()
    {
        float w = Screen.width / 2;
        float h = Screen.height - 80;
        if (GUI.Button(new Rect(w, h, 250, 50), "Regenerate Dungeon"))
        {
            RegenerateDungeon();
        }
    }

    void RegenerateDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
