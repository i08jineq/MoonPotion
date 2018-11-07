using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class Model
    {
        public static Model instance;
        public GameEvent gameEvent = new GameEvent();//clear at the end of level

        public Model()
        {
            gameEvent = new GameEvent();
        }

        public static void Init()
        {
            if (instance == null)
            {
                instance = new Model();
            }
        }
    }
}