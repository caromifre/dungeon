using UnityEngine;
using static Constantes_celda;
public class Crear_nexo : ICrear_nexo
{
    public void crear_nexo(int pos_x, int pos_y) {
        Debug.Log("Este metodo fue sobrecaragdo usaer el nuevo metodo:\ncrear_nexo(Cell[] board,int pos_x, int pos_y)");
    }

    public void crear_nexo(Cell[,] board, int pos_x, int pos_y)
    {
        //funcion o "método" para unir los caminos en cada nueva iteracion creando puertas
        //board es una matriz donde se evaluan las direcciones para crear una puerta y si no exite la crea
        //pos_x,pos_y es la coordenda desde donde se evaluan que puertas crear
        int ancho = board.GetLength(0), largo = board.GetLength(1);

        if (pos_x - 1 >= 0 && board[pos_x - 1, pos_y].visited)
        {
            if (!board[pos_x - 1, pos_y].galeria)
            {
                board[pos_x, pos_y].pared[_IZQUIERDA] = true;
                board[pos_x - 1, pos_y].puerta[_DERECHA] = true;
                //chequear conclicto de paredes
                if (board[pos_x, pos_y].pared[_IZQUIERDA] && board[pos_x - 1, pos_y].pared[_DERECHA])
                {
                    board[pos_x - 1, pos_y].pared[_DERECHA] = false;
                }
            }
            else
            {
                //chequear conflicto de paredes para las galerias para encontrar el limite
                if (!board[pos_x, pos_y].pared[_IZQUIERDA] /*&& !board[pos_x - 1, pos_y].pared[_DERECHA]*/)
                {
                    //crear una puerta
                    board[pos_x, pos_y].puerta[_IZQUIERDA] = true;
                    board[pos_x - 1, pos_y].puerta[_DERECHA] = true;
                }
            }
        }

        if (pos_x + 1 < ancho && board[pos_x + 1, pos_y].visited /*&& !board[pos_x + 1, pos_y].galeria*/)
        {
            if (!board[pos_x + 1, pos_y].galeria)
            {
                board[pos_x, pos_y].pared[_DERECHA] = true;
                board[pos_x + 1, pos_y].puerta[_IZQUIERDA] = true;
                //chequear conclito de paredes
                if (board[pos_x, pos_y].pared[_DERECHA] && board[pos_x + 1, pos_y].pared[_IZQUIERDA])
                {
                    board[pos_x + 1, pos_y].pared[_IZQUIERDA] = false;
                }
            }
            else
            {
                if (!board[pos_x, pos_y].pared[_DERECHA] /*&& !board[pos_x + 1, pos_y].pared[_IZQUIERDA]*/)
                {
                    board[pos_x, pos_y].puerta[_DERECHA] = true;
                    board[pos_x + 1, pos_y].puerta[_IZQUIERDA] = true;
                }
            }
        }


        if (pos_y + 1 < largo && board[pos_x, pos_y + 1].visited /*&& !board[pos_x, pos_y + 1].galeria*/)
        {
            if (!board[pos_x, pos_y + 1].galeria)
            {
                board[pos_x, pos_y].pared[_ABAJO] = true;
                board[pos_x, pos_y + 1].puerta[_ARRIBA] = true;
                //chequear conclito de paredes
                if (board[pos_x, pos_y].pared[_ABAJO] && board[pos_x, pos_y + 1].pared[_ARRIBA])
                {
                    board[pos_x, pos_y + 1].pared[_ARRIBA] = false;
                }
            }
            else
            {
                if (!board[pos_x, pos_y].pared[_ABAJO] /*&& !board[pos_x, pos_y + 1].pared[_ARRIBA]*/)
                {
                    board[pos_x, pos_y].puerta[_ABAJO] = true;
                    board[pos_x, pos_y + 1].puerta[_ARRIBA] = true;
                }
            }
        }


        if (pos_y - 1 >= 0 && board[pos_x, pos_y - 1].visited /*&& !board[pos_x, pos_y - 1].galeria*/)
        {
            if (!board[pos_x, pos_y - 1].galeria)
            {
                board[pos_x, pos_y].pared[_ARRIBA] = true;
                board[pos_x, pos_y - 1].puerta[_ABAJO] = true;
                //chequear conclito de paredes
                if (board[pos_x, pos_y].pared[_ARRIBA] && board[pos_x, pos_y - 1].pared[_ABAJO])
                {
                    board[pos_x, pos_y - 1].pared[_ABAJO] = false;
                }
            }
            else
            {
                if (!board[pos_x, pos_y].pared[_ARRIBA] /*&& !board[pos_x, pos_y - 1].pared[_ABAJO]*/)
                {
                    board[pos_x, pos_y].puerta[_ARRIBA] = true;
                    board[pos_x, pos_y - 1].puerta[_ABAJO] = true;
                }
            }
        }
    }
     
}

   

