using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class ResourceData
    {
        public IEnumerator LoadResource()
        {
            yield return null;
        }

        private IEnumerator RequestDelay(ResourceRequest request)
        {
            while(request.isDone == false)
            {
                yield return null;
            }
        }

    }
}