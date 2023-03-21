using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using History;

namespace Map
{
    public class MapManager
    {
        private List<State> States = new List<State>();
        private List<Country> Countries = new List<Country>();

        public MapManager(List<State> states, List<Country> countries)
        {
            this.States = states;
            UnityEngine.Debug.Log($"Loaded {this.States.Count} states");

            this.Countries = countries;
            UnityEngine.Debug.Log($"Loaded {this.Countries.Count} countries");
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
