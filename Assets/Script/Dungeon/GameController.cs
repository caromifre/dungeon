using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    DungeonGenerator dungeonGenerator;
    [SerializeField] GameObject generator;

    // Start is called before the first frame update
    void Start()
    {

      dungeonGenerator = generator.GetComponent<DungeonGenerator>();

      dungeonGenerator.MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
