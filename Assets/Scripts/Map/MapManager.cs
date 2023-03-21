using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager
    {
        private List<State> States = new List<State>();

        public MapManager(List<State> states)
        {
            this.States = states;
            UnityEngine.Debug.Log($"Loaded {this.States.Count} states");
        }

        public void SetRandomColorByState()
        {
            foreach (var state in this.States)
            {
                var color = new Color(Random.value, Random.value, Random.value, 1.0f);

                foreach (var province in state.Provinces)
                {
                    foreach (var obj in province.GameObjects)
                    {
                        obj.GetComponent<MeshRenderer>().material.color = color;
                    }
                }
            }
        }
    }
}
