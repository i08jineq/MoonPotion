using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class GameEvent
    {
        public Communicator onDayStarted = new Communicator();
        public Communicator onDayEnded = new Communicator();
        public Communicator<int> onDayTimeChanged = new Communicator<int>();
        public Communicator onInteruptedByLevelEvent = new Communicator();
        public Communicator onLeventEventEnded = new Communicator();

        public Communicator<int> onGetGoldFromCustomer = new Communicator<int>();

        public void Clear()
        {
            onDayStarted.Clear();
            onDayEnded.Clear();
        }
    }
}