using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Constantes_celda;//inluye libreria de constantes para las celdas
public class Propiedades_celda : MonoBehaviour
{

    //[SerializeField] bool[,] _celda = new bool[4,5];//inicializa el array en true

    [SerializeField] GameObject[] _celda_gmo, _puertas_gmo, _pared_puerta_gmo, _pilares_gmo,_paredes;
    [SerializeField] int _x, _y;
    int[,] _tablero;
    /*void Inicializar_Celda() {
    
        
    }*/

    private void Start()
    {
       /* bool[] ac = new bool[4]{false, true, false, true };
            
        actualizar_celda(_PAREDES,ac);*/
        
    }

    public void actualizar_celda(int elemento,bool[] accion, int _pos = 0) { 
        switch (elemento)
        {
            case _PAREDES:
                //_celda_gmo[_PAREDES].SetActive(_accion);
                //este for duncioan siempre y cuando las paredes y las puertas compartan el mismo espacio
                //Debug.Log("ocultar paredes");
                for (int a = 0; a < 4; a++) {
                    _paredes[a].SetActive(!accion[a]);
                    _puertas_gmo[a].SetActive(!accion[a]);
                }
                break;
            case _PUERTAS:
               // Debug.Log("Mostrar puertas");
                for(int a=0; a < 4;a++) {
                    _puertas_gmo[a].SetActive(accion[a]);
                    _pared_puerta_gmo[a].SetActive(!accion[a]);
                }
                
                break;
        }
    }
}
    
