using System;
using System.Collections;
using System.Collections.Generic;

namespace DarkLordGame
{
    public class Communicator<T, T2, T3, T4>
    {
        List<Action<T, T2, T3, T4>> actionList = new List<Action<T, T2, T3, T4>>();

        public void AddListener(Action<T, T2, T3, T4> action)
        {
            if (actionList.Contains(action))
            {
                return;
            }
            actionList.Add(action);
        }

        public void Invoke(T parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
        {
            int count = actionList.Count;
            for(int i = count - 1; 0 <= i; i--)
            {
                actionList[i].Invoke(parameter1, parameter2, parameter3, parameter4);
            }
        }

        public void RemoveListener(Action<T, T2, T3, T4> action)
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