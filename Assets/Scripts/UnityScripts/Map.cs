using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Map;
using History;
using ShpLoader;
using MoonSharp.Interpreter;

namespace UnityScripts
{
    public class Map : MonoBehaviour
    {
        private MapManager Manager;

        // Start is called before the first frame update
        void Start()
        {
            // register provinces
            var provinces = new List<Province>();
            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                var provinceObject = this.gameObject.transform.GetChild(i).gameObject;
                var record = provinceObject.GetComponent<ObjectRecord>();
                var id = record.Record["iso_3166_2"];
                var objects = new List<GameObject>();

                for (var j = 0; j < provinceObject.transform.childCount; j++)
                {
                    objects.Add(provinceObject.transform.GetChild(j).gameObject);
                }

                provinces.Add(new Province(id, objects));
            }

            // register states
            var states = new List<State>();
            var stateDefineScripts = Directory.GetFiles(Constants.StateFilePath, $"*{Constants.ScriptExtension}");
            foreach (var filePath in stateDefineScripts)
            {
                var engine = Constants.LuaEngine;
                engine.DoFile(filePath);
                var id = (string)engine.Globals["id"];
                var name = (string)engine.Globals["name"];
                var provinceId = ((Table)engine.Globals["provinces"]).Values.Select(v => v.CastToString()).ToArray();

                var provincesInState = new List<Province>();
                foreach (var pId in provinceId)
                {
                    var found = provinces.Find(p => p.Id == pId);
                    if (found != null)
                    {
                        provincesInState.Add(found);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning($"Province id: {pId} was not found at {filePath}");
                    }
                }

                states.Add(new State(id, provincesInState, name));
            }

            // register countries
            var countries = new List<Country>();
            var countryDefineScripts = Directory.GetFiles(Constants.CountryFilePath, $"*{Constants.ScriptExtension}");
            foreach (var filePath in countryDefineScripts)
            {
                var engine = Constants.LuaEngine;
                engine.DoFile(filePath);
                var id = (string)engine.Globals["id"];
                var name = (string)engine.Globals["name"];
                var stateId = ((Table)engine.Globals["states"]).Values.Select(v => v.CastToString()).ToArray();

                var statesInCountry = new List<State>();
                foreach (var sId in stateId)
                {
                    var found = states.Find(s => s.Id == sId);
                    if (found != null)
                    {
                        statesInCountry.Add(found);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning($"State id: {sId} was not found at {filePath}");
                    }
                }

                countries.Add(new Country(id, statesInCountry, name));
            }

            this.Manager = new MapManager(states, countries);
            this.Manager.SetRandomColorByState();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}