using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Province
    {
        public string Id { get; }
        public GameObject GameObject { get; }

        public Province(string id, GameObject gameObject)
        {
            this.Id = id;
            this.GameObject = gameObject;
        }
    }
}
