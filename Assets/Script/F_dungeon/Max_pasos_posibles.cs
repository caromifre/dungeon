using UnityEngine;

public class Max_pasos_posibles : IMax_pasos_posible
{
    public int max_pasos_posbiles(int dir, int pasos, int pos_x, int pos_y) {
        Debug.Log("Este metodo fue sobrecaragdo usar el nuevo metodo:\nmax_pasos_posbiles(Cell[,] board,int dir, int pasos, int pos_x, int pos_y)");
        return 0;
    }

    public int max_pasos_posbiles(Cell[,] board,int dir, int pasos, int pos_x, int pos_y) {
        //funcion para determinar cuanto se puede avanzar en una direccion propuesta
        //board es una matriz donde se evaluan las direcciones
        //dir recibe un numero que define una direccion "cardinal" arriba abajo derecha o izquierda
        //paso la cadidad de celdas que se pretende avanzar en la direccion propuesta
        //pos_x, pos_y la coordenda desde donde se quiere evaluar la direccion

        int ancho = board.GetLength(0), alto = board.GetLength(1), n_pasos;

        switch (dir)
        {
            case 0: //izquierda 
                if (pasos > pos_x)
                {//si la cantidad de pasos es mayor a lo que se peude retroceder se devuelve la posicion actual
                    n_pasos = pos_x;
                    //n_pasos=n_pasos>ancho/2?n_pasos/3:n_pasos;
                }
                else
                {
                    n_pasos = pasos;
                }

                break;
            case 1: //dercha
                if (pasos + pos_x < ancho - 1)
                {//si no se supera al ancho desde la posicion devuelve los pasos
                    n_pasos = pasos;
                    //n_pasos = n_pasos > ancho / 2 ? n_pasos / 3 : n_pasos;
                }
                else
                {//de lo contrario devuelve los paso hasta el limite del ancho 
                    n_pasos = ancho - pos_x - 1;
                }
                break;
            case 2://abajo
                if (pasos + pos_y < alto - 1)
                {//si no se supera al alto desde la posicion devuelve los pasos
                    n_pasos = pasos;
                    //n_pasos = n_pasos > alto / 2 ? n_pasos / 3 : n_pasos;
                }
                else
                {//de lo contrario devuelve los paso hasta el limite del alto 
                    n_pasos = alto - pos_y - 1;
                    //n_pasos = n_pasos > alto / 2 ? n_pasos / 3 : n_pasos;
                }
                break;
            case 3://arriba
                if (pasos > pos_y)
                {//si la cantidad de pasos es mayor a lo que se puede retroceder se devuelve la posicion actual
                    n_pasos = pos_y;
                    //n_pasos = n_pasos > alto / 2 ? n_pasos / 3 : n_pasos;
                }
                else
                {
                    n_pasos = pasos;
                }
                break;
            default:
                n_pasos = 0;
                break;
        }
        //devuelve una distancia variable si la distancia es mayor a 0 si no devuleve 0
       // Debug.Log("n_pasos: "+ n_pasos);
        return n_pasos > 0 ? Random.Range(1, n_pasos) : 0;
    }
}
