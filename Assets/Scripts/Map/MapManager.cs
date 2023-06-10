using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using History;
using ShpLoader;

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

        public void SetRandomColorByCountry()
        {
            foreach (var country in this.Countries)
            {
                var color = new Color(Random.value, Random.value, Random.value, 1.0f);

                foreach (var state in country.States)
                {
                    foreach (var province in state.Provinces)
                    {
                        province.GameObject.GetComponent<MeshRenderer>().material.color = color;
                    }
                }
            }
        }

        public void SetRandomColorByState()
        {
            foreach (var state in this.States)
            {
                var color = new Color(Random.value, Random.value, Random.value, 1.0f);

                foreach (var province in state.Provinces)
                {
                    province.GameObject.GetComponent<MeshRenderer>().material.color = color;
                }
            }
        }

        public void SetRandomColorByProvince()
        {
            foreach (var state in this.States)
            {
                foreach (var province in state.Provinces)
                {
                    var color = new Color(Random.value, Random.value, Random.value, 1.0f);
                    province.GameObject.GetComponent<MeshRenderer>().material.color = color;
                }
            }
        }

        public Province FindProvinceByProvinceObjectRecord(ObjectRecord objectRecord)
        {
            var provinceId = objectRecord.Record[Constants.ObjectRecordProvinceIdKey];

            foreach (var state in this.States)
            {
                foreach (var province in state.Provinces)
                {
                    if (province.Id == provinceId)
                    {
                        return province;
                    }
                }
            }

            return null;
        }

        public State FindStateByProvinceObjectRecord(ObjectRecord objectRecord)
        {
            var province = this.FindProvinceByProvinceObjectRecord(objectRecord);

            if (province == null)
                return null;

            foreach (var state in this.States)
            {
                foreach (var p in state.Provinces)
                {
                    if (p.Id == province.Id)
                    {
                        return state;
                    }
                }
            }

            return null;
        }

        public Country FindCountryByProvinceObjectRecord(ObjectRecord objectRecord)
        {
            var state = this.FindStateByProvinceObjectRecord(objectRecord);

            if (state == null)
                return null;

            var stateId = state.Id;

            foreach (var country in this.Countries)
            {
                foreach (var s in country.States)
                {
                    if (s.Id == stateId)
                    {
                        return country;
                    }
                }
            }

            return null;
        }
    }
}
