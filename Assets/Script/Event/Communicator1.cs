﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DarkLordGame
{
    public class Communicator<T>
    {
        List<Action<T>> actionList = new List<Action<T>>();

        public void AddListener(Action<T> action)
        {
            if (actionList.Contains(action))
            {
                return;
            }
            actionList.Add(action);
        }

        public void Invoke(T parameter1)
        {
            int count = actionList.Count;
            for(int i = count - 1; 0 <= i; i--)
            {
                actionList[i].Invoke(parameter1);
            }
        }

        public void RemoveListener(Action<T> action)
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