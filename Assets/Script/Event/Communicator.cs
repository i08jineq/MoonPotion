using System;
using System.Collections;
using System.Collections.Generic;

namespace DarkLordGame
{
    public class Communicator
    {
        List<Action> actionList = new List<Action>();

        public void AddListener(Action action)
        {
            if (actionList.Contains(action))
            {
                return;
            }
            actionList.Add(action);
        }

        public void Invoke()
        {
            int count = actionList.Count;
            for(int i = count - 1; 0 <= i; i--)
            {
                actionList[i].Invoke();
            }
        }

        public void RemoveListener(Action action)
        {
            if (actionList.Contains(action))
            {
                actionList.Remove(action);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < actionList.Count; i++)
            {
                actionList[i] = null;
            }
            actionList.Clear();
        }
    }
}