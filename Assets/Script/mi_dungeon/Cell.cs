using UnityEngine;

public class Cell
{
    public bool visited = false;
    //public bool[] status = new bool[4];
    public bool[] puerta = new bool[4];
    public bool[] pared = new bool[4];
    public bool galeria;
    public bool rama_galeria;
    public bool pasillo;
    public int direccion;
    public bool vacio;
    public bool fin;
    public bool inicio;

    public void reset_puerta()
    {
        //resetear puertas
        for (int a = 0; a < puerta.Length; a++)
        {
            puerta[a] = false;
        }
    }
    public void reset_pared()
    {
        //resetear paredes
        for (int a = 0; a < pared.Length; a++)
        {
            pared[a] = false;

        }
    }
}