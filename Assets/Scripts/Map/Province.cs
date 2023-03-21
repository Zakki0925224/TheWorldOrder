using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Province
    {
        public string Id { get; }
        public List<GameObject> GameObjects { get; }

        public Province(string id, List<GameObject> gameObjects)
        {
            this.Id = id;
            this.GameObjects = gameObjects;
        }
    }
}
