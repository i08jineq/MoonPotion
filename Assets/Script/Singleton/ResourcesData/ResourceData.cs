using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class ResourceData
    {
        public GameObject craftUIPrefab;

        private const string craftUIPrefabPath = "Setup/CraftUI";

        public IEnumerator LoadResource()
        {
            LoadSetupUI();
            yield return null;
        }

        private void LoadSetupUI()
        {
            craftUIPrefab = Resources.Load<GameObject>(craftUIPrefabPath);
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