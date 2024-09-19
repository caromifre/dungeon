
 interface Idungeon_creator {
    
    //inicializa la variable que sera usada como tablero para hacer el laberinto
   void inicializar_tablero();

    //crea la base del laberinto haciedo un camino sin interrupciones desde
    //el punto 0.0 hasta el fin
    void crear_camino();

    //chequea si una celda quedo cerrada y habre puertas para unir caminos
   void crear_nexo(int pos_x, int pos_y);

    //recibe una coordenada y y chequea sus vecinos para determinar 
    //hacia donde se puede avanzar y devuelve esa direccion
    int direccion_posible(int pos_x, int pos_y);

    //funcion para determinar cuanto se puede avanzar en una direccion propuesta
    int max_pasos_posbiles(int dir, int pasos, int pos_x, int pos_y);

    //funcion o "método" para detectar si se puede crear un pasillo
   void Crear_pasillos(int pos_x, int pos_y);

    //funcion para proponer posibles espacin para hacer galerias en la mazmorra
    void crear_galerias();

    //crea la galeria 
    void hacer_galeria(int ini_x, int ini_y, int lim_x, int lim_y);

    //instancia el laberinto
    void GenerateDungeon();
}