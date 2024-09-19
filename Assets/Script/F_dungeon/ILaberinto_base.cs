
public interface ILaberinto_base 
{
    //inicializa la variable que sera usada como tablero para hacer el laberinto
    void inicializar_tablero();

    //crea la base del laberinto haciedo un camino sin interrupciones desde
    //el punto 0.0 hasta el fin
    void crear_camino();

    //recibe una coordenada y y chequea sus vecinos para determinar 
    //hacia donde se puede avanzar y devuelve esa direccion
    int direccion_posible(int pos_x, int pos_y);
}
