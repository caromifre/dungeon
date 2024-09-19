

public interface IMax_pasos_posible
{
    //funcion para determinar cuanto se puede avanzar en una direccion propuesta
    int max_pasos_posbiles(int dir, int pasos, int pos_x, int pos_y);
    //dir recibe un numero que define una direccion "cardinal" arriba abajo derecha o izquierda
    //paso la cadidad de celdas que se pretende avanzar en la direccion propuesta
    //pos_x, pos_y la coordenda desde donde se quiere evaluar la direccion
}
