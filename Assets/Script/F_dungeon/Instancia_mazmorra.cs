using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constantes_celda;

public class Instancia_mazmorra : MonoBehaviour,IGen_mazmorra
{
    Prop_Celda _P_celda;
    public void GenerateDungeon() {
        Debug.Log("Este metodo fue sobrecaragdo usaer el nuevo metodo:\n Generardungeon(GameObject[] rooms, Cell[,] board, int X, int Y,float o_x,float o_y)");
    }
    public void Generardungeon(GameObject[] rooms,Cell[,] board, int X, int Y,float o_x,float o_y) {
        //rooms recibe un array de gameobjects a instanciar donde el elemento [0] es primer elemento a instanciar y [1] sera el ultimo
        //board es una matriz que indica que elemtos se van a instanciar
        // X,Y indican el tamaño de la matriz de objetos a instanciar
        //o_x,o_y es el tamaño de la celda offset en x e y
        GameObject Nueva_celda;
        int celda = 0;
        
        for (int a = 0; a < X; a++)
        {
            for (int b = 0; b < Y; b++)
            {
                if (board[a, b].visited)
                {
                    if (board[a, b].inicio) celda = _INICIO;
                    else if (board[a, b].fin) celda = _FIN;
                    else celda = Random.Range(2, rooms.Length);

                    Nueva_celda = Instantiate(rooms[celda], new Vector3(a * o_x, 0f, -b * o_y), Quaternion.identity) as GameObject;
                    _P_celda = Nueva_celda.GetComponent<Prop_Celda>();
                    _P_celda.actualizar_celda(_PAREDES, board[a, b].pared);
                    _P_celda.actualizar_celda(_PUERTAS, board[a, b].puerta);
                    Nueva_celda.name += " " + a + "_" + b;

                }

            }
        }

    }
}
