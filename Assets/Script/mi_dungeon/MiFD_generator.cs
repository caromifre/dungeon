
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Constantes_celda;
public class MiFD_generator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        //public bool[] status = new bool[4];
        public bool[] puerta = new bool[4];
        public bool[] pared=new bool[4];
        public bool galeria;
        public bool rama_galeria;
        public bool pasillo;
        public int direccion;
        public bool vacio;
        public bool fin;

        public void reset_puerta() {
            //resetear puertas
            for (int a = 0; a < puerta.Length; a++) { 
                puerta[a] = false;
            }
        }
        public void reset_pared() {
            //resetear paredes
            for (int a = 0; a < pared.Length; a++)
            {
                pared[a] = false;
            }
        }
    }

    // [SerializeField] Vector2 _dungeonSize;
    [SerializeField] int _x, _y;
    [SerializeField] int _startPos = 0;

    [SerializeField] GameObject[] _rooms;
    [SerializeField] Vector2 offset;
    [SerializeField] int _Galerias;
    GameObject _Nueva_celda;
    //List<Cell> _board;
    Cell[,] _board;

    // Start is called before the first frame update
    void Start()
    {
        //Solo inicia si los valores para la matriz son mayores que 0
        if (_x > 0 && _y > 0)
        {
            inicializar_tablero();
            MazeGenerator();
        }
        else
        {
            Debug.LogError("los valores para x e y tiene que ser mayores a 0");
        }
    }

    void GenerateDungeon()
    {
        int celda = 0;
        //Debug.Log("generar dungeon");
        for (int a = 0; a < _x; a++) {
            for (int b = 0; b < _y; b++) {
                if (_board[a, b].visited)
                {
                    if (_board[a, b].fin) celda = 1;
                    else celda = 0;
                    
                   // celda = Random.Range(0,_rooms.Length);
                    //int randomRoom = Random.Range(0, _rooms.Length);
                    _Nueva_celda = Instantiate(_rooms[celda], new Vector3(a * offset.x, 0f, -b * offset.y), Quaternion.identity) as GameObject;
                    Propiedades_celda p_c= _Nueva_celda.GetComponent<Propiedades_celda>();
                    p_c.actualizar_celda(_PAREDES, _board[a, b].pared);
                    p_c.actualizar_celda(_PUERTAS, _board[a, b].puerta);
                    _Nueva_celda.name += " " + a + "_" + b;

                }
                
            }
        }

    }

    public void MazeGenerator()
    {
        
        crear_camino();
        //abrir_puertas();
        crear_galerias();
        GenerateDungeon();
    }


    void inicializar_tablero() {

        //crea un tablero en blanco
        _board = new Cell[_x, _y];

        for (int a = 0; a < _board.GetLength(0); a++)
        {
            for (int b = 0; b < _board.GetLength(1); b++)
            {
                _board[a, b] = new Cell();
            }
        }
    }
    //---
    //-----------------
    //---
    void crear_camino() {
        _board[0, 0].visited = true;
        int ancho = _board.GetLength(0);
        int alto = _board.GetLength(1);
        int cont_x=0,cont_y=0,x_pos,y_pos,pasos;
        int[] ejes= new int[4] {ancho,ancho,alto, alto };
       // Cell cel_pos;//celda actual
        int rand_avance;
       /* for (int a = 0; a < _board.GetLength(0); a++)
        {
            for (int b = 0; b < _board.GetLength(1); b++)
            {
                //desplazameinto en x representa hacia la derecha
                //desplazameinto en y represnta hacia abajo
                //si abro una puerta, en al celda contigua tiro la pared
                //chequear posibles caminos inicia al azar

                rand_avance = direccion_posible(a, b);
                int x_pos = a, y_pos = b;
                switch (rand_avance)
                {
                    case 0: //izquierda
                        x_pos -= 1;
                        _board[x_pos, b].visited = true;
                        break;
                    case 1: //dercha
                        x_pos += 1;
                        _board[x_pos, b].visited = true;
                        break;
                    case 2://abajo
                        y_pos += 1;
                        _board[a, y_pos].visited = true;
                        break;
                    case 3://arriba
                        y_pos -= 1;
                        _board[a, y_pos].visited = true;
                        break;
                }       
                
            }
           
        }//fin for*/

        while (cont_x<ancho) {
            /*//desplazameinto en x representa hacia la derecha
            //desplazameinto en y represnta hacia abajo
            //si abro una puerta, en al celda contigua tiro la pared*/

            //chequear posibles caminos inicia al azar
            rand_avance = direccion_posible(cont_x, cont_y);
            if (rand_avance != 5)
            {
                pasos =max_pasos_posbiles(rand_avance, Random.Range(1, ejes[rand_avance]),cont_x,cont_y);

                if (!_board[cont_x, cont_y].visited) {
                    _board[cont_x, cont_y].visited=true;
                    _board[cont_x, cont_y].direccion = rand_avance;
                    crear_nexo(cont_x, cont_y);

                }
            }
            else {//fin del camino
                pasos=0;
                //si no hay caminos posibles marco la celda como final
                Debug.Log("no hay caminos posibles");
                _board[cont_x, cont_y].fin = true;
                _board[cont_x, cont_y].visited = true;
                crear_nexo(cont_x, cont_y);
                break;
            }
            while (pasos > 0 && rand_avance!=5) {
               // Debug.Log("paaso: " + pasos + "cont");
                x_pos = cont_x;
                y_pos = cont_y;
                Crear_pasillos(x_pos, y_pos);
                switch (rand_avance)
                {
                    case 0: //izquierda
                        x_pos -= 1;
                        _board[x_pos, cont_y].visited = true;
                        //abrir puertas tirar pared
                        _board[x_pos, cont_y].pared[_DERECHA] = true;                        
                        _board[x_pos + 1, cont_y].puerta[_IZQUIERDA] = true;
                        //guardar direccion
                        _board[x_pos+1, cont_y].direccion = rand_avance;
                        cont_x--;//retroceder
                        break;
                    case 1: //derecha
                        x_pos += 1;
                        _board[x_pos, cont_y].visited = true;
                        //abrir puertas tirar pared
                        _board[x_pos, cont_y].pared[_IZQUIERDA] = true;                        
                        _board[x_pos - 1, cont_y].puerta[_DERECHA] = true;
                        //guardar direccion
                        _board[x_pos-1, cont_y].direccion = rand_avance;
                        cont_x++;//avanzar
                        break;
                    case 2://abajo
                        y_pos += 1;
                        _board[cont_x, y_pos].visited = true;
                        //abrir puertas tirar pared
                        _board[cont_x, y_pos].pared[_ARRIBA] = true;
                        _board[cont_x, y_pos - 1].puerta[_ABAJO]= true;
                        //guardar direccion
                        _board[x_pos, cont_y-1].direccion = rand_avance;
                        cont_y++;//avanzar en y
                        break;
                    case 3://arriba
                        y_pos -= 1;
                        _board[cont_x, y_pos].visited = true;
                        //abrir puertas tirar pared
                        _board[cont_x, y_pos].pared[_ABAJO] = true;
                        _board[cont_x, y_pos + 1].puerta[_ARRIBA] = true;
                        _board[x_pos, cont_y + 1].direccion = rand_avance;
                        cont_y--;//retroceder en y
                        break;
                }
                pasos--;
            }
            cont_x++;
            //si sali del limite marco la casilla como final
            if (cont_x >= ancho)
            {
                Debug.Log("ancho: " + ancho + "alto: " + alto + " cont_x: " + cont_x + "cont_Y: " + cont_y);
                _board[ancho - 1, cont_y].fin = true;
                _board[ancho - 1, cont_y].visited = true;
                crear_nexo(ancho - 1, cont_y);
            }
        }
        
    }

    //---
    //-----------------
    //---
    void crear_nexo(int pos_x,int pos_y) {
        //funcion o "método" para unir los caminos en cada nueva iteracion
        int ancho = _board.GetLength(0), largo = _board.GetLength(1);
    
        if (pos_x - 1 >= 0 && _board[pos_x - 1, pos_y].visited)
        {
            _board[pos_x, pos_y].pared[_IZQUIERDA] = true;
            _board[pos_x-1, pos_y].puerta[_DERECHA] = true;
        }

        if (pos_x + 1 < ancho && _board[pos_x + 1, pos_y].visited)
        {
            _board[pos_x, pos_y].pared[_DERECHA] = true;
            _board[pos_x + 1, pos_y].puerta[_IZQUIERDA] = true;
        }


        if (pos_y + 1 < largo && _board[pos_x, pos_y + 1].visited)
        {
            _board[pos_x, pos_y].pared[_ABAJO] = true;
            _board[pos_x, pos_y+1].puerta[_ARRIBA] = true;
        }


        if (pos_y - 1 >= 0 && _board[pos_x, pos_y - 1].visited)
        {
            _board[pos_x, pos_y].pared[_ARRIBA] = true;
            _board[pos_x, pos_y - 1].puerta[_ABAJO] = true;
        }

    }
    //---
    //-----------------
    //---

    int direccion_posible(int pos_x,int pos_y) {
       //algoritmo para determinar si el casillero propuesto es viable
        int ancho=_board.GetLength(0),largo=_board.GetLength(1);
        //bool cambiar=false;
        List<int> cardinal=new List<int>();
       
        if (pos_x - 1 >= 0 && !_board[pos_x - 1, pos_y].visited) {
            //chequea desplazameintoa a la izquierda
            cardinal.Add(0);
        }
                      
        if (pos_x + 1 < ancho && !_board[pos_x + 1, pos_y].visited)
        {
            //chequea desplazameintoa a la derecha
            cardinal.Add(1);
        }
 
                  
        if ( pos_y + 1 < largo && !_board[pos_x , pos_y + 1].visited)
        {
            //chequea desplazameintoa hacia abajo
            cardinal.Add(2);
        }


        if (pos_y - 1 >= 0 && !_board[pos_x, pos_y - 1].visited)
        {
            //chequea desplazameinto hacia arriba
            cardinal.Add(3);
        }   

        if (cardinal.Count == 0)
        {
            return 5;//no hay caminos posibles
        }
        else {
            
             return cardinal[ Random.Range(0, cardinal.Count)];
        }
        
    }

    //---
    //------------------------
    //---
    int max_pasos_posbiles(int dir,int pasos, int pos_x, int pos_y) {
        //funcion para determinar cuanto se puede avanzar en una direccion propuesta
      int ancho= _board.GetLength(0),alto=_board.GetLength(1),n_pasos;
        
        switch (dir)
        {
            case 0: //izquierda 
                if (pasos > pos_x)
                {//si la cantidad de pasos es mayor a lo que se peude retroceder se devuelve la posicion actual
                    n_pasos= pos_x;
                }
                else {
                    n_pasos = pasos;
                }
                
                break;
            case 1: //dercha
                if (pasos + pos_x < ancho-1)
                {//si no se supera al ancho desde la posicion devuelve los pasos
                    n_pasos = pasos;
                }
                else {//de lo contrario devuelve los paso hasta el limite del ancho 
                    n_pasos = ancho  - pos_x-1;
                }
                break;
            case 2://abajo
                if (pasos + pos_y < alto-1)
                {//si no se supera al alto desde la posicion devuelve los pasos
                    n_pasos = pasos;
                }
                else
                {//de lo contrario devuelve los paso hasta el limite del alto 
                    n_pasos = alto  - pos_y-1;
                }
                break;
            case 3://arriba
                if (pasos > pos_y)
                {//si la cantidad de pasos es mayor a lo que se puede retroceder se devuelve la posicion actual
                    n_pasos = pos_y;
                }
                else {
                    n_pasos = pasos;
                }
                break;
            default:
                n_pasos = 0; 
                break;
        }
        return n_pasos;
    }

    //---
    //------------------------
    //---
    void Crear_pasillos(int pos_x, int pos_y) {
        //funcion o "método" para detectar si se puede rear un pasillo
        //los pasillos no pueden ser de mas d ela mitad del largo o del ancho de la mazmorra
        //la celda 0.0 no puede ser un pasillo
        //las celdas que se convierten en pasillos se marcan como tal
        int ancho = _board.GetLength(0), largo = _board.GetLength(1),dir,pasos,avan_x=pos_x,avan_y=pos_y;
        int[] ejes = new int[4] { ancho, ancho, largo, largo };

        if (pos_x == 0 && pos_y == 0) {
            //salgo si es la casilla 0
            return;
        }
        if (_board[pos_x, pos_y].pasillo) {
            //si la celda es un pasillo salgo
            return;
        }

        //buscar direccion posible
        if (pos_x - 1 >= 0 && _board[pos_x - 1, pos_y].visited && _board[pos_x - 1, pos_y].pared[_IZQUIERDA])
        {
            pasos = Random.Range(1, ejes[0] % 2);
            pasos = max_pasos_posbiles(0, pasos, pos_x, pos_y);
            dir = 0;
        }
        else if (pos_x + 1 < ancho && _board[pos_x + 1, pos_y].visited && _board[pos_x + 1, pos_y].pared[_DERECHA])
        {
            pasos = Random.Range(1, ejes[1] % 2);
            pasos = max_pasos_posbiles(0, pasos, pos_x, pos_y);
            dir = 1;
        }
        else if (pos_y + 1 < largo && _board[pos_x, pos_y + 1].visited && _board[pos_x, pos_y+1].pared[_ABAJO])
        {
            pasos = Random.Range(1, ejes[2] % 2);
            pasos = max_pasos_posbiles(0, pasos, pos_x, pos_y);
            dir = 2;
        }
        else if (pos_y - 1 >= 0 && _board[pos_x, pos_y - 1].visited && _board[pos_x, pos_y - 1].pared[_ARRIBA])
        {
            pasos = Random.Range(1, ejes[3] % 2);
            pasos = max_pasos_posbiles(0, pasos, pos_x, pos_y);
            dir = 3;
        }
        else {
            //no hay direccion posible y retorna
            return;
        }
        //Debug.Log("crear pasillo: " + pasos + " dir: " + dir);
        //crear pasillo
        while (pasos >= 0) {
            if (avan_x != 0 && avan_y != 0)//controlar que no se llego a la celda 0
            {   
                //activar modo pasillo para la celda actual
                if (!_board[avan_x, avan_y].pasillo) _board[avan_x, avan_y].pasillo = true;

                if (dir == 0 && avan_x - 1 >= 0 && _board[avan_x - 1, avan_y].visited)
                {//izquierda
                    
                    _board[avan_x - 1, avan_y].pasillo = true;
                    //apagar puertas
                    if(_board[avan_x, avan_y].puerta[_IZQUIERDA]) _board[avan_x, avan_y].puerta[_IZQUIERDA]=false;
                    if (_board[avan_x - 1 , avan_y].puerta[_DERECHA]) _board[avan_x-1, avan_y].puerta[_DERECHA] = false;
                    //apagar paredes
                    _board[avan_x, avan_y].pared[_IZQUIERDA]=true;
                    _board[avan_x - 1, avan_y].pared[_DERECHA] = true;
                    avan_x--;//retoceder
                }

                else if (dir == 1 && avan_x + 1 < ancho && _board[avan_x + 1, avan_y].visited)
                {//derecha
                    
                    _board[avan_x + 1, avan_y].pasillo = true;
                    //apagar puertas
                    if (_board[avan_x, avan_y].puerta[_DERECHA]) _board[avan_x, avan_y].puerta[_DERECHA ] = false;
                    if (_board[avan_x + 1, avan_y].puerta[_IZQUIERDA]) _board[avan_x + 1, avan_y].puerta[_IZQUIERDA] = false;
                    //apagar paredes
                    _board[avan_x, avan_y].pared[_IZQUIERDA] = true;
                    _board[avan_x - 1, avan_y].pared[_DERECHA] = true;
                    avan_x++;//aavnzar
                }

                else if (dir == 2 && pos_y + 1 < largo && _board[avan_x, avan_y + 1].visited)
                {//abajo
                    
                    _board[avan_x, avan_y + 1].pasillo = true;
                    //apagar puertas
                    if (_board[avan_x, avan_y].puerta[_ABAJO]) _board[avan_x, avan_y].puerta[_ABAJO] = false;
                    if (_board[avan_x, avan_y + 1 ].puerta[_ARRIBA]) _board[avan_x, avan_y + 1].puerta[_ARRIBA] = false;
                    //apagar paredes
                    _board[avan_x, avan_y].pared[_ABAJO] = true;
                    _board[avan_x, avan_y +1 ].pared[_ARRIBA] = true;
                    avan_y++;//avanzar
                }

                else if (dir == 3 && pos_y - 1 >= 0 && _board[avan_x, avan_y - 1].visited)
                {//arriba
                    _board[avan_x, avan_y - 1].pasillo = true;
                    //apagar puertas
                    if (_board[avan_x, avan_y].puerta[_ARRIBA]) _board[avan_x, avan_y].puerta[_ARRIBA] = false;
                    if (_board[avan_x, avan_y - 1].puerta[_ABAJO]) _board[avan_x, avan_y - 1].puerta[_ABAJO] = false;
                    //apagar paredes
                    _board[avan_x, avan_y].pared[_ARRIBA] = true;
                    _board[avan_x, avan_y - 1].pared[_ABAJO] = true;
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

    void crear_galerias(int x=1,int y=1) {
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
        /*
        int dir,cant_gal;
        Cell cel_pos;
        
        //obtengo la cantidad maxima de galerias que pueden entrar en la mazmorra
        cant_gal = (_board.Length - lim_x * lim_y) / 4;

        if(y==0) lim_y = 1;
        for (int a =lim_x; a< ancho;a++) {
            for (int b = lim_y; b < alto; b++) {
                cel_pos= _board[a,b];
                if (cel_pos.visited && !cel_pos.galeria && cant_gal>0) {
                    dir = cel_pos.direccion;
                    if () { 
                    }
                }
            }
        }*/
        int ancho = _board.GetLength(0), alto = _board.GetLength(1), lim_x, lim_y;
        const int EJE_X = 0,EJE_Y=1;
        int[] coord_board = new int[1];
        List<int[]> Lista_coord = new List<int[]>();

        lim_x = x < 1 ? 1 : x;
        lim_y = y < 1 ? 1 : y;

        for (int a = 0; a < ancho; a++) {
            for (int b = 0; b < alto; b++) {
                if (_board[a,b].visited){
                    coord_board[EJE_X] = a;
                    coord_board[EJE_Y] = b;
                    Lista_coord.Add(coord_board);
                }
            }
        }

        //crear primer galeria en el anteultimo elemento
        coord_board = Lista_coord[Lista_coord.Count-2];

        //testear por cuadrante probar arriba a la izquierda
        int pos_x = coord_board[EJE_X], pos_y=coord_board[EJE_Y];
        int cont_pos = 0;

        //probar limite de la diagonal galeria arriba a la izquierda
        if (pos_y - 1 >= 0 && pos_x - 1 >= 0 && !_board[pos_x-1,pos_y-1].galeria) { 
            //si la diagonal no fallo el esapcio esta libre para dibujar una galeria
            //1ro restedo la casilla

        }
        
        if (pos_x - 1 >= 0 && _board[pos_x - 1, pos_y].visited)
        {
            //chequea desplazameintoa a la izquierda
        }

        if (pos_x + 1 < ancho && _board[pos_x + 1, pos_y].visited)
        {
            //chequea desplazameintoa a la derecha
        }


        if (pos_y + 1 < alto && _board[pos_x, pos_y + 1].visited)
        {
            //chequea desplazameintoa hacia abajo
        }


        if (pos_y - 1 >= 0 && _board[pos_x, pos_y - 1].visited)
        {
            //chequea desplazameintoa hacia arriba
        }

    }
    //---
    //------
    //---
     void espacio_galeria(int pos_x, int pos_y,int lim_x,int lim_y)
    {
        
    }


    private void OnGUI()
    {
        float w = Screen.width / 2;
        float h = Screen.height - 80;
        if (GUI.Button(new Rect(w, h, 250, 50), "Regenerate Dungeon"))
        {
            RegenerateDungeon();
        }
    }

    void RegenerateDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
