using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator _anim;
    Camera _mainCamera;
   
    Ray _ray;
    RaycastHit _hit;

    List<IDamagable> _damagablesInRange;
    [SerializeField] LayerMask _layerMask;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _damagablesInRange = new List<IDamagable>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(_ray, out _hit, 20,_layerMask))
            {
                //Debug.DrawRay(_ray.origin, _ray.direction * 20, Color.red);
               
               //Melee Attack
                var damagable = _hit.transform.GetComponent<IDamagable>();

                if(damagable != null )
                {
                    SimpleAttack(_hit.transform.position);
                }
            }
        }


        if(Input.GetKey(KeyCode.LeftControl))
        {
            _anim.SetBool("Defense",true);
        }else
        {
            _anim.SetBool("Defense",false);
        }
    }

    void SimpleAttack(Vector3 toLook)
    {   
        if(_damagablesInRange.Count >= 1)
        {
            this.transform.LookAt(toLook);
           
            _damagablesInRange[0].Damage(10);
          
            _anim.SetTrigger("SimpleAttack");
        }
    }

    void StrongAttack()
    {
        _anim.SetTrigger("StrongAttack");
    }

    private void OnTriggerEnter(Collider other) 
    {
        var damagable = other.GetComponent<IDamagable>();

        if(damagable != null)
        {
            _damagablesInRange.Add(damagable);
           // Debug.Log("Damagable Add " + other.name);
            //Debug.Log("Damagables in Range " + _damagablesInRange.Count); 
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        var damagable = other.GetComponent<IDamagable>();

        if(damagable != null && _damagablesInRange.Contains(damagable))
        {
            _damagablesInRange.Remove(damagable);
           // Debug.Log("Damagable Add " + other.name);
            //Debug.Log("Damagables in Range " + _damagablesInRange.Count); 
        }    
    }
}
