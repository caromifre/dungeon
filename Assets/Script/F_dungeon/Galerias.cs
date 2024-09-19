using System.Collections.Generic;
using UnityEngine;
using static Constantes_celda;
public class Galerias : IGalerias
{
    Crear_nexo _Cr_Nexo = new Crear_nexo();
    
    public void crear_galerias() {
        //funcion para proponer posibles espacios para hacer galerias en la mazmorra
        Debug.Log("Este metodo fue sobrecaragdo usaer el nuevo metodo:\ncrear_galerias(Cell[,] board)");
    }

    public void crear_galerias(Cell[,] board)
    {
        //funcion para agregar galerias a la mazmorra
        //la galeria no puede solaparse con la celda donde parece el jugador
        //lo minimo que ocupa una galeria son 4 celdas puede tener un pasillo que la 
        //separe del camino principal
        //cualquier pasillo se puede convertir en galeria
        //una galeria no se puede crear o solapar con otra galeria
        //una galeria puede desembocar en mas galerias
        //una galeria que no tiene salida tiene que tener alguna recompesa
        //la direccion general para recorrer el camino es tanto en x o en y de menos a mas
        //esto sirve para para saber que puerta bloquear si quiero obligar a tomar una desviacion
        //hacia una galeria
               
        int ancho = board.GetLength(0), alto = board.GetLength(1), cont_g = 0;
        const int EJE_X = 0, EJE_Y = 1;
        int max_gal = board.Length / 4; //determinar la catidad maxima de galerias en el tablero
        int[] coordboard;
        List<int[]> Lista_coord = new List<int[]>();

        for (int a = 0; a < ancho; a++)
        {
            for (int b = 0; b < alto; b++)
            {
                //busco las celdas activas y que no sean galerias no sean el fin y dentro de los limites
                if (board[a, b].visited && !board[a, b].galeria && !board[a, b].fin && !board[a, b].inicio)
                {
                    coordboard = new int[2] { a, b };
                    Lista_coord.Add(coordboard);
                }
            }
        }

        for (int a = Lista_coord.Count - 1; a >= 0; a--)
        {
            coordboard = Lista_coord[Lista_coord.Count-1];
            //crear primer galeria en el anteultimo elemento
            if (max_gal >= board.Length / 4)
            {
                coordboard = Lista_coord[a];    
                max_gal--;//descontar galeria
                //crear deistancia variable entre galerias
                cont_g = Random.Range(3, 8);    
            }
            else if (max_gal < (board.Length / 4) && max_gal > 0)
            {
                if (cont_g == 0)
                {
                    coordboard = Lista_coord[a];
                    max_gal--;//descontar galeria
                    //crear deistancia variable entre galerias
                    cont_g = Random.Range(3, 8);
                    
                }
                cont_g--;

            }
            
            
            /*else if (Lista_coord.Count / 4 == a) coordboard = Lista_coord[a];

            else if (Lista_coord.Count / 2 == a) coordboard = Lista_coord[a];
            else coordboard = Lista_coord[Lista_coord.Count - 1];*/

            //Debug.Log("testeando lista: " + a);
            //testear por cuadrantes
            int pos_x = coordboard[EJE_X], pos_y = coordboard[EJE_Y];

            //probar limite de la diagonal galeria arriba a la izquierda
            if (pos_y - 1 >= 0 && pos_x - 1 >= 0 && !board[pos_x - 1, pos_y - 1].galeria)
            {
                //Debug.Log("//probar limite de la diagonal galeria arriba a la izquierda");
                hacer_galeria(board, pos_x - 1, pos_y - 1, pos_x, pos_y);
            }
            //probar limite diagonal arriba derecho
            if (pos_y - 1 >= 0 && pos_x + 1 < ancho && !board[pos_x + 1, pos_y - 1].galeria)
            {
                //Debug.Log("//probar limite diagonal arriba derecho");
                hacer_galeria(board, pos_x, pos_y - 1, pos_x + 1, pos_y);
            }
            //probar limite diagonal abajo izquierda
            if (pos_y + 1 < alto && pos_x - 1 >= 0 && !board[pos_x - 1, pos_y + 1].galeria)
            {
                //Debug.Log("//probar limite diagonal abajo izquierda");
                hacer_galeria(board, pos_x - 1, pos_y, pos_x, pos_y + 1);
            }

            //probar diagonal abajo a la derecha
            if (pos_y + 1 < alto && pos_x + 1 < ancho && !board[pos_x + 1, pos_y + 1].galeria)
            {
                //Debug.Log("//probar diagonal abajo a la derecha");
                hacer_galeria(board, pos_x, pos_y, pos_x + 1, pos_y + 1);
            }
        }
    }

    //---
    //-----------
    //---

    public void hacer_galeria( int ini_x, int ini_y, int lim_x, int lim_y) {
        Debug.Log("Este metodo fue sobrecaragdo usaer el nuevo metodo:\nhacer_galeria(Cell[,] board, int ini_x, int ini_y, int lim_x, int lim_y)");
    }

    public void hacer_galeria(Cell[,] board, int ini_x, int ini_y, int lim_x, int lim_y)
    {
        //crea una galeria si posible en el espacio propuesto 
        //ini's tienen que ser la coordenadas menores del sector
        //lim's las cooredenadas mayores para correr el bucle

        int cont = 0;
        bool gal;
        for (int c = 0; c <= 1; c++)
        {

            gal = cont == 4 ? true : false;

            for (int a = ini_x; a <= lim_x; a++)
            {
                for (int b = ini_y; b <= lim_y; b++)
                {
                    if (!board[a, b].galeria && !board[a, b].inicio && !board[a, b].fin && !gal)
                    {
                        cont++;
                       // Debug.Log("conteos:" + cont);
                    }
                    if (gal)
                    {
                        board[a, b].reset_pared();
                        board[a, b].reset_puerta();
                        board[a, b].visited = true;
                        board[a, b].galeria = true;
                        //crear compartimientos
                        if (cont == 4)
                        {
                            //estoy arriba a la izquierda                             
                            board[a, b].pared[_ABAJO] = true;
                            board[a, b].pared[_DERECHA] = true;
                           _Cr_Nexo.crear_nexo(board,a, b);
                        }
                        if (cont == 3)
                        {
                            //estoy arriba a la derecha                            
                            board[a, b].pared[_DERECHA] = true;
                            board[a, b].pared[_ARRIBA] = true;
                            _Cr_Nexo.crear_nexo(board, a, b);
                        }
                        if (cont == 2)
                        {
                            //estoy abajo a la izquierda                            
                            board[a, b].pared[_ABAJO] = true;
                            board[a, b].pared[_IZQUIERDA] = true;
                            _Cr_Nexo.crear_nexo(board, a, b);
                        }
                        if (cont == 1)
                        {
                            //estoy anajo a la derecha
                            board[a, b].pared[_ARRIBA] = true;
                            board[a, b].pared[_IZQUIERDA] = true;
                            _Cr_Nexo.crear_nexo(board, a, b);
                        }
                        cont--;
                    }
                }
            }

        }
        //return cont;
    }


}
