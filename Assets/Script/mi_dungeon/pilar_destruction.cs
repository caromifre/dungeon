using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilar_destruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, .01f);

        foreach (Collider col in colliders)
        {
            // Verificar si el objeto colisionado tiene el tag "pilar" y no es el mismo objeto
            if (col.tag == "pilar" && col.gameObject != this.gameObject)
            {
                Destroy(col.gameObject);
            }
        }
    }

}