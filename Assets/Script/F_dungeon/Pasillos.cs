using UnityEngine;
using static Constantes_celda;
public class Pasillos : IPasillos
{
    Max_pasos_posibles _Mp_Posibles = new Max_pasos_posibles();
    public void Crear_pasillos(int pos_x, int pos_y) {
        Debug.Log("Este metodo fue sobrecaragdo usaer el nuevo metodo:\nCrear_pasillos(Cell[,] board,int pos_x, int pos_y)");
    }

    public void Crear_pasillos(Cell[,] board,int pos_x, int pos_y)
    {
        //funcion o "método" para detectar si se puede crear un pasillo
        //los pasillos no pueden ser de mas d ela mitad del largo o del ancho de la mazmorra
        //la celda 0.0 no puede ser un pasillo
        //las celdas que se convierten en pasillos se marcan como tal
        int ancho = board.GetLength(0), largo = board.GetLength(1), dir, pasos, avan_x = pos_x, avan_y = pos_y;
        int[] ejes = new int[4] { ancho, ancho, largo, largo };

        if (pos_x == 0 && pos_y == 0)
        {
            //salgo si es la casilla 0
            return;
        }
        if (board[pos_x, pos_y].pasillo)
        {
            //si la celda es un pasillo salgo
            return;
        }

        //buscar direccion posible
        if (pos_x - 1 >= 0 && board[pos_x - 1, pos_y].visited && board[pos_x - 1, pos_y].pared[_IZQUIERDA])
        {
            pasos = Random.Range(1, ejes[0] % 2);
            pasos =_Mp_Posibles.max_pasos_posbiles(board,0, pasos, pos_x, pos_y);
            dir = 0;
        }
        else if (pos_x + 1 < ancho && board[pos_x + 1, pos_y].visited && board[pos_x + 1, pos_y].pared[_DERECHA])
        {
            pasos = Random.Range(1, ejes[1] % 2);
            pasos = _Mp_Posibles.max_pasos_posbiles(board, 0, pasos, pos_x, pos_y);
            dir = 1;
        }
        else if (pos_y + 1 < largo && board[pos_x, pos_y + 1].visited && board[pos_x, pos_y + 1].pared[_ABAJO])
        {
            pasos = Random.Range(1, ejes[2] % 2);
            pasos = _Mp_Posibles.max_pasos_posbiles(board, 0, pasos, pos_x, pos_y);
            dir = 2;
        }
        else if (pos_y - 1 >= 0 && board[pos_x, pos_y - 1].visited && board[pos_x, pos_y - 1].pared[_ARRIBA])
        {
            pasos = Random.Range(1, ejes[3] % 2);
            pasos = _Mp_Posibles.max_pasos_posbiles(board, 0, pasos, pos_x, pos_y);
            dir = 3;
        }
        else
        {
            //no hay direccion posible y retorna
            return;
        }
        //Debug.Log("crear pasillo: " + pasos + " dir: " + dir);
        //crear pasillo
        while (pasos >= 0)
        {
            if (avan_x != 0 && avan_y != 0)//controlar que no se llego a la celda 0
            {
                //activar modo pasillo para la celda actual
                if (!board[avan_x, avan_y].pasillo) board[avan_x, avan_y].pasillo = true;

                if (dir == 0 && avan_x - 1 >= 0 && board[avan_x - 1, avan_y].visited)
                {//izquierda

                    board[avan_x - 1, avan_y].pasillo = true;
                    //apagar puertas
                    if (board[avan_x, avan_y].puerta[_IZQUIERDA]) board[avan_x, avan_y].puerta[_IZQUIERDA] = false;
                    if (board[avan_x - 1, avan_y].puerta[_DERECHA]) board[avan_x - 1, avan_y].puerta[_DERECHA] = false;
                    //apagar paredes
                    board[avan_x, avan_y].pared[_IZQUIERDA] = true;
                    board[avan_x - 1, avan_y].pared[_DERECHA] = true;
                    avan_x--;//retoceder
                }

                else if (dir == 1 && avan_x + 1 < ancho && board[avan_x + 1, avan_y].visited)
                {//derecha

                    board[avan_x + 1, avan_y].pasillo = true;
                    //apagar puertas
                    if (board[avan_x, avan_y].puerta[_DERECHA]) board[avan_x, avan_y].puerta[_DERECHA] = false;
                    if (board[avan_x + 1, avan_y].puerta[_IZQUIERDA]) board[avan_x + 1, avan_y].puerta[_IZQUIERDA] = false;
                    //apagar paredes
                    board[avan_x, avan_y].pared[_IZQUIERDA] = true;
                    board[avan_x - 1, avan_y].pared[_DERECHA] = true;
                    avan_x++;//aavnzar
                }

                else if (dir == 2 && pos_y + 1 < largo && board[avan_x, avan_y + 1].visited)
                {//abajo

                    board[avan_x, avan_y + 1].pasillo = true;
                    //apagar puertas
                    if (board[avan_x, avan_y].puerta[_ABAJO]) board[avan_x, avan_y].puerta[_ABAJO] = false;
                    if (board[avan_x, avan_y + 1].puerta[_ARRIBA]) board[avan_x, avan_y + 1].puerta[_ARRIBA] = false;
                    //apagar paredes
                    board[avan_x, avan_y].pared[_ABAJO] = true;
                    board[avan_x, avan_y + 1].pared[_ARRIBA] = true;
                    avan_y++;//avanzar
                }

                else if (dir == 3 && pos_y - 1 >= 0 && board[avan_x, avan_y - 1].visited)
                {//arriba
                    board[avan_x, avan_y - 1].pasillo = true;
                    //apagar puertas
                    if (board[avan_x, avan_y].puerta[_ARRIBA]) board[avan_x, avan_y].puerta[_ARRIBA] = false;
                    if (board[avan_x, avan_y - 1].puerta[_ABAJO]) board[avan_x, avan_y - 1].puerta[_ABAJO] = false;
                    //apagar paredes
                    board[avan_x, avan_y].pared[_ARRIBA] = true;
                    board[avan_x, avan_y - 1].pared[_ABAJO] = true;
                    avan_y--;//retroceder
                }

            }
            else
            {
                break;
            }
            pasos--;
        }
    }
}
