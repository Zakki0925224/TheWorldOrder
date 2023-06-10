using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ShpLoader;
using History;
using Map;
using UnityEngine;
using UnityScripts.Events;
using MoonSharp.Interpreter;

namespace Map
{
    public static class MapLoader
    {
        public static MapManager Load()
        {
            IShpFile shapeFile = LoadFiles(Constants.ShpFilePath) as ShpFile;
            var dbfFile = LoadFiles(Constants.DbfFilePath) as DbfFile;
            var color = new Color();
            return RenderFiles(color, shapeFile, dbfFile);
        }

        private static IFile LoadFiles(string path)
        {
            IFile file = null;
            var fileExt = Path.GetExtension(path);
            file = FileFactory.CreateInstance(path);
            file.Load();
            return file;
        }

        private static MapManager RenderFiles(Color color, IShpFile shapeFile, DbfFile dbfFile)
        {
            var map = new GameObject("Map");
            ((IRenderable)shapeFile).Render(color, dbfFile, map);
            return InitMapManager(map);
        }

        private static MapManager InitMapManager(GameObject parentMapObject)
        {
            // register provinces
            var provinces = new List<Province>();
            for (var i = 0; i < parentMapObject.transform.childCount; i++)
            {
                var provinceObject = parentMapObject.transform.GetChild(i).gameObject;
                var record = provinceObject.GetComponent<ObjectRecord>();
                var id = record.Record[Constants.ObjectRecordProvinceIdKey];
                var combineMeshes = new List<CombineInstance>();

                for (var j = 0; j < provinceObject.transform.childCount; j++)
                {
                    var childGameObject = provinceObject.transform.GetChild(j).gameObject;
                    var childMeshFilter = childGameObject.GetComponent<MeshFilter>();

                    var combineMesh = new CombineInstance();
                    combineMesh.mesh = childMeshFilter.sharedMesh;
                    combineMesh.transform = childMeshFilter.transform.localToWorldMatrix;

                    childMeshFilter.gameObject.SetActive(false);

                    combineMeshes.Add(combineMesh);
                }

                var meshFilter = provinceObject.AddComponent<MeshFilter>() as MeshFilter;
                var meshRenderer = provinceObject.AddComponent<MeshRenderer>() as MeshRenderer;

                meshFilter.mesh = new Mesh();
                meshFilter.mesh.CombineMeshes(combineMeshes.ToArray());
                meshRenderer.material = new Material(Shader.Find("Standard"));

                var collider = provinceObject.AddComponent<MeshCollider>() as MeshCollider;
                collider = new MeshCollider();

                provinceObject.AddComponent<ProvinceObjectEvent>();

                provinces.Add(new Province(id, provinceObject));
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
                        Debug.LogWarning($"Province id: {pId} was not found at {filePath}");
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
                        Debug.LogWarning($"State id: {sId} was not found at {filePath}");
                    }
                }

                // load flag
                var flagPath = $"{Constants.FlagsFilePath}/{id}.png";

                if (!File.Exists(flagPath))
                    flagPath = $"{Constants.FlagsFilePath}/Unknown.png";

                var sprite = Constants.LoadPng(flagPath);

                countries.Add(new Country(id, statesInCountry, name, sprite));
            }

            var mapManager = new MapManager(states, countries);
            return mapManager;
        }
    }
}
