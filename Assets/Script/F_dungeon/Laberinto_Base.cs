using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constantes_celda;
public class Laberinto_Base : ILaberinto_base
{
    Max_pasos_posibles _MP_posibles = new Max_pasos_posibles();
    Crear_nexo _C_nexo = new Crear_nexo();
    Pasillos _C_pasillos = new Pasillos();
    public void inicializar_tablero() {
        //inicializa la variable que sera usada como tablero para hacer el laberinto
        Debug.Log("Este metodo fue sobrecaragdo usar el nuevo metodo:\ninicializar_tablero(Cell[,] board, int x, int y)");
    }

    bool inicializar_tablero(Cell[,] board)
    {
        if (board==null) {
            Debug.Log("error al inicializar el tablero inicicalizar board con 'new' ");
            return false;
        }
        else{
            //crea un tablero en blanco
            for (int a = 0; a < board.GetLength(0); a++)
            {
                for (int b = 0; b < board.GetLength(1); b++)
                {
                    board[a, b] = new Cell();
                }
            }
            return true;
        }
    }
    //---
    //--------
    //---
    public void crear_camino() {
        //crea la base del laberinto haciedo un camino sin interrupciones desde
        //el punto 0.0 hasta el fin
        Debug.Log("Este metodo fue sobrecaragdo usar el nuevo metodo:\ncrear_camino(Cell[,] board)");
    }

    public void crear_camino(Cell[,] board)
    {
        bool ini_board = inicializar_tablero(board);

        if (!ini_board) return;//si board esta vacio termina la funcion

        board[0, 0].visited = true;
        board[0, 0].inicio = true;
        int ancho = board.GetLength(0);
        int alto = board.GetLength(1);
        int cont_x = 0, cont_y = 0, x_pos, y_pos, pasos;
        int[] ejes = new int[4] { ancho, ancho, alto, alto };
        int rand_avance;

        while (cont_x < ancho)
        {
            /*//desplazameinto en x representa hacia la derecha
            //desplazameinto en y represnta hacia abajo
            //si abro una puerta, en al celda contigua tiro la pared*/

            //chequear posibles caminos inicia al azar
            rand_avance = direccion_posible(board,cont_x, cont_y);
            if (rand_avance != 5)
            {
                pasos = _MP_posibles.max_pasos_posbiles(board, rand_avance, Random.Range(1, ejes[rand_avance]), cont_x, cont_y);
                //Debug.Log("pasos: "+ pasos);
                if (!board[cont_x, cont_y].visited)
                {
                    board[cont_x, cont_y].visited = true;
                    board[cont_x, cont_y].direccion = rand_avance;
                    _C_nexo.crear_nexo(board, cont_x, cont_y);

                }
            }
            else
            {//fin del camino
                pasos = 0;
                //si no hay caminos posibles marco la celda como final
                Debug.Log("no hay caminos posibles");
                board[cont_x, cont_y].fin = true;
                board[cont_x, cont_y].visited = true;
                _C_nexo.crear_nexo(board, cont_x, cont_y);
                break;
            }
            while (pasos > 0 && rand_avance != 5)
            {
                // Debug.Log("paaso: " + pasos + "cont");
                x_pos = cont_x;
                y_pos = cont_y;
                _C_pasillos.Crear_pasillos(board, x_pos, y_pos);
                switch (rand_avance)
                {
                    case 0: //izquierda
                        x_pos -= 1;
                        board[x_pos, cont_y].visited = true;
                        board[cont_x, y_pos].direccion = rand_avance;
                        //abrir puertas tirar pared
                        board[x_pos, cont_y].pared[_DERECHA] = true;
                        board[x_pos + 1, cont_y].puerta[_IZQUIERDA] = true;
                        cont_x--;//retroceder
                        break;
                    case 1: //derecha
                        x_pos += 1;
                        board[x_pos, cont_y].visited = true;
                        board[cont_x, y_pos].direccion = rand_avance;
                        //abrir puertas tirar pared
                        board[x_pos, cont_y].pared[_IZQUIERDA] = true;
                        board[x_pos - 1, cont_y].puerta[_DERECHA] = true;
                        cont_x++;//avanzar
                        break;
                    case 2://abajo
                        y_pos += 1;
                        board[cont_x, y_pos].visited = true;
                        board[cont_x, y_pos].direccion = rand_avance;
                        //abrir puertas tirar pared
                        board[cont_x, y_pos].pared[_ARRIBA] = true;
                        board[cont_x, y_pos - 1].puerta[_ABAJO] = true;
                        cont_y++;//avanzar en y
                        break;
                    case 3://arriba
                        y_pos -= 1;
                        board[cont_x, y_pos].visited = true;
                        board[cont_x, y_pos].direccion = rand_avance;
                        //abrir puertas tirar pared
                        board[cont_x, y_pos].pared[_ABAJO] = true;
                        board[cont_x, y_pos + 1].puerta[_ARRIBA] = true;
                        cont_y--;//retroceder en y
                        break;
                }
                pasos--;
            }
            cont_x++;
            //si sali del limite marco la casilla como final
            if (cont_x >= ancho)
            {
                // Debug.Log("ancho: " + ancho + "alto: " + alto + " cont_x: " + cont_x + "cont_Y: " + cont_y);
                board[ancho - 1, cont_y].fin = true;
                board[ancho - 1, cont_y].visited = true;
                _C_nexo.crear_nexo(board, ancho - 1, cont_y);
            }
        }

    }
    
    //----
    //----------
    //----

    public int direccion_posible(int pos_x, int pos_y) {
        //recibe una coordenada y y chequea sus vecinos para determinar 
        //hacia donde se puede avanzar y devuelve esa direccion.
        Debug.Log("Este metodo fue sobrecaragdo usar el nuevo metodo:\ndireccion_posible(Cell[,] board,int pos_x, int pos_y)");
        return 0;
    }

    int direccion_posible(Cell[,] board,int pos_x, int pos_y)
    {
        //algoritmo para determinar si el casillero propuesto es viable
        int ancho = board.GetLength(0), largo = board.GetLength(1);
        //bool cambiar=false;
        List<int> cardinal = new List<int>();

        if (pos_x - 1 >= 0 && !board[pos_x - 1, pos_y].visited)
        {
            //chequea desplazameintoa a la izquierda
            cardinal.Add(0);
        }

        if (pos_x + 1 < ancho && !board[pos_x + 1, pos_y].visited)
        {
            //chequea desplazameintoa a la derecha
            cardinal.Add(1);
        }


        if (pos_y + 1 < largo && !board[pos_x, pos_y + 1].visited)
        {
            //chequea desplazameintoa hacia abajo
            cardinal.Add(2);
        }


        if (pos_y - 1 >= 0 && !board[pos_x, pos_y - 1].visited)
        {
            //chequea desplazameinto hacia arriba
            cardinal.Add(3);
        }

        if (cardinal.Count == 0)
        {
            return 5;//no hay caminos posibles
        }
        else
        {

            return cardinal[Random.Range(0, cardinal.Count)];
        }

    }
}
