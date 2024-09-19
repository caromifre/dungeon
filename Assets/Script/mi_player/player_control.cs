using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{    
    public CharacterController controller;
    [SerializeField] Transform _cam;
    [SerializeField] float _characterSpeed = 3f,
                            _suavizar_rotacion=0.2f;
    float _horizontal,
          _vertical,
          _angulo_rot,
          _angulo,
          _vel_giro_suave;

    Vector3 _direccion,
            _ir_a_dir;

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _direccion = new Vector3(_horizontal, 0f, _vertical).normalized;
        if (_direccion.magnitude >= 0.1f) {
            //calcular angulo de rotacion
            _angulo_rot = Mathf.Atan2(_direccion.x, _direccion.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            
            _angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, _angulo_rot, ref _vel_giro_suave, _suavizar_rotacion);
            //rotar
            transform.rotation = Quaternion.Euler(0f, _angulo, 0f);
            _ir_a_dir = Quaternion.Euler(0f, _angulo_rot, 0f) * Vector3.forward;
            controller.Move(_direccion * _characterSpeed * Time.deltaTime);
        }
    }
}
