using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constantes_celda;
public class Prop_Celda : MonoBehaviour,ICelda
{
    [SerializeField] GameObject[] _celda_gmo, _puertas_gmo, _pared_puerta_gmo, _pilares_gmo, _paredes;
    //[SerializeField] int _x, _y;


    public void actualizar_celda() {
        Debug.Log("esat funcion ha sido sobrecaragda usar el metodo sobrecargado: actualizar_celda(int elemento, bool[] accion, int _pos = 0)");
    }
    public void actualizar_celda(int elemento, bool[] accion)
    {
        //esta funcion activa o desactiva elemetos de la celda
        //int elemento indica que elemento de la celda se quiere mostrao u ocultar
        //accion inica si se activa o desactiva el elemento indicado, true para desctivar y false para activar
        switch (elemento)
        {
            case _PAREDES:
                //este for dunciona siempre y cuando las paredes y las puertas compartan el mismo espacio
                for (int a = 0; a < 4; a++)
                {
                    _paredes[a].SetActive(!accion[a]);
                    _puertas_gmo[a].SetActive(!accion[a]);
                }
                break;
            case _PUERTAS:
                // Debug.Log("Mostrar puertas");
                for (int a = 0; a < 4; a++)
                {
                    _puertas_gmo[a].SetActive(accion[a]);
                    _pared_puerta_gmo[a].SetActive(!accion[a]);
                }

                break;
        }
    }
}
