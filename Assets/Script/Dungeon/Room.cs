using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    [SerializeField] private string _idRoom;

    public string IdRoom {get { return _idRoom;}}
}
