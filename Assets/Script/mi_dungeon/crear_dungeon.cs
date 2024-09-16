using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class crear_dungeon : MonoBehaviour
{
    // Clase interna que representa una celda del laberinto
    public class Cell
    {
        // Indica si la celda ha sido visitada
        public bool visited = false;
        // Array de 4 posiciones que indica el estado de las paredes (arriba, derecha, abajo, izquierda)
        public bool[] status = new bool[4];
    }

    // Tamaño del laberinto (número de celdas en el eje X e Y)
    [SerializeField] Vector2 _dungeonSize;
    // Posición de inicio del generador de laberintos
    [SerializeField] int _startPos = 0;

    // Array de prefabs de habitaciones
    [SerializeField] GameObject[] _rooms;
    // Offset para posicionar las habitaciones en el espacio
    [SerializeField] Vector2 offset;

    // Lista de celdas que representarán el tablero del laberinto
    List<Cell> _board;

    // Start es llamado antes de que se inicie la primera actualización del frame
    void Start()
    {
        // Se llama al generador de laberintos (actualmente comentado)
        // MazeGenerator();
    }

    // Genera el dungeon instanciando habitaciones basadas en las celdas visitadas
    void GenerateDungeon()
    {
        // Recorre las celdas del tablero para instanciar habitaciones en base a las celdas visitadas
        for (int i = 0; i < _dungeonSize.x; i++)
        {
            for (int j = 0; j < _dungeonSize.y; j++)
            {
                // Obtiene la celda actual del tablero
                Cell currentCell = _board[Mathf.FloorToInt(i + j * _dungeonSize.x)];

                // Solo genera habitaciones en celdas visitadas
                if (currentCell.visited)
                {
                    // Selecciona una habitación aleatoria
                    int randomRoom = Random.Range(0, _rooms.Length);
                    // Instancia una nueva habitación en la posición adecuada con el offset correspondiente
                    GameObject newRoom = Instantiate(_rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity) as GameObject;

                    // Actualiza la habitación según el estado de las paredes de la celda
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell.status);

                    // Renombra la habitación para facilitar su identificación
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
    }

    // Genera el laberinto completo
    public void MazeGenerator()
    {
        // Crea el tablero del laberinto
        CreateBoard();

        // Genera el laberinto mediante el algoritmo de laberintos
        CreateMaze();

        // Instancia las habitaciones correspondientes
        GenerateDungeon();
    }

    // Crea el laberinto utilizando el algoritmo de backtracking
    private void CreateMaze()
    {
        // Determina la celda inicial
        int currentCell = _startPos;

        // Pila donde se almacenarán las celdas para retroceder si es necesario
        Stack<int> path = new Stack<int>();

        // Determina el máximo de iteraciones para generar el laberinto
        int maxMazeInteractions = _board.Count * 3;

        Debug.Log("Maximo de Interacciones posibles: " + maxMazeInteractions);

        // Itera hasta alcanzar el límite de interacciones o generar todo el laberinto
        for (int k = 0; k < maxMazeInteractions; k++)
        {
            // Marca la celda actual como visitada
            _board[currentCell].visited = true;

            // Si se alcanza la última celda, se detiene la generación del laberinto
            if (currentCell == _board.Count - 1)
            {
                Debug.Log("Interacciones totales: " + k);
                break;
            }

            // Verifica las celdas vecinas no visitadas
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                // Si no hay vecinos disponibles, retrocede a la celda anterior
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
                // Si hay vecinos disponibles, selecciona uno aleatoriamente y avanza
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // Determina la dirección hacia la nueva celda (abajo/derecha o arriba/izquierda) y actualiza las paredes
                if (newCell > currentCell)
                {
                    // Movimiento hacia abajo o derecha
                    if (newCell - 1 == currentCell)
                    {
                        _board[currentCell].status[2] = true;
                        currentCell = newCell;
                        _board[currentCell].status[3] = true;
                    }
                    else
                    {
                        _board[currentCell].status[1] = true;
                        currentCell = newCell;
                        _board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // Movimiento hacia arriba o izquierda
                    if (newCell + 1 == currentCell)
                    {
                        _board[currentCell].status[3] = true;
                        currentCell = newCell;
                        _board[currentCell].status[2] = true;
                    }
                    else
                    {
                        _board[currentCell].status[0] = true;
                        currentCell = newCell;
                        _board[currentCell].status[1] = true;
                    }
                }
            }
        }
    }

    // Crea el tablero del laberinto (lista de celdas)
    private void CreateBoard()
    {
        _board = new List<Cell>();

        // Llena el tablero con celdas vacías
        for (int j = 0; j < (_dungeonSize.x * _dungeonSize.y); j++)
        {
            _board.Add(new Cell());
        }

        Debug.Log("Cantidad de celdas en el tablero: " + _board.Count);
    }

    // Verifica las celdas vecinas que no han sido visitadas
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        Debug.Log("numero de celda: " + cell + " numnero de celda + 1: " + cell + 1 + " ancho del dungeon: " + _dungeonSize.x + " modulo cell y ancho: " + cell % _dungeonSize.x);
        // Verifica la celda superior
        if (cell - _dungeonSize.x >= 0 && !_board[Mathf.FloorToInt(cell - _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - _dungeonSize.x));
        }

        // Verifica la celda inferior
        if (cell + _dungeonSize.x < _board.Count && !_board[Mathf.FloorToInt(cell + _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + _dungeonSize.x));
        }

        // Verifica la celda derecha
        if ((cell + 1) % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        // Verifica la celda izquierda
        if (cell % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }

    // Muestra un botón en la pantalla para regenerar el dungeon
    private void OnGUI()
    {
        float w = Screen.width / 2;
        float h = Screen.height - 80;
        if (GUI.Button(new Rect(w, h, 250, 50), "Regenerar Dungeon"))
        {
            RegenerateDungeon();
        }
    }

    // Regenera el dungeon recargando la escena actual
    void RegenerateDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
