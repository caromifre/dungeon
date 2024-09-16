using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory : MonoBehaviour
{
   // [SerializeField] private Room _roomZero;
   // [SerializeField] private Room _roomOne;
    //private Dictionary< string, Room> _roomsDictionary;

    [SerializeField] private Room[] _rooms;
    

    private void Awake() 
    {
        /*_roomsDictionary = new Dictionary< string, Room>();
       
        foreach (var room in _rooms)
        {
            _roomsDictionary.Add(room.IdRoom, room);
        }*/
    }

    /*public Room Create(string _idRoom)
    {

        if(!_roomsDictionary.TryGetValue(_idRoom, out Room room))
        {
            throw new Exception($"Room with id {_idRoom} does not exist");
            
        }

        return Instantiate(room);

    */
     /*   switch (_idRoom)
        {
            case "roomZero":
                return Instantiate(_roomZero);
                
            case "roomOne":
                return Instantiate(_roomOne);
            
            default:
                return null;
        }
    
    }*/
}
