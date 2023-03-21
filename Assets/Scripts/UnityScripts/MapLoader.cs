using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using ShpLoader;

namespace UnityScripts
{
    public class MapLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            IShpFile shapeFile = LoadFiles(Constants.ShpFilePath) as ShpFile;
            var dbfFile = LoadFiles(Constants.DbfFilePath) as DbfFile;
            var color = new Color();
            RenderFiles(color, shapeFile, dbfFile);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IFile LoadFiles(string path)
        {
            IFile file = null;
            var fileExt = Path.GetExtension(path);
            file = FileFactory.CreateInstance(path);
            file.Load();
            return file;
        }

        private void RenderFiles(Color color, IShpFile shapeFile, DbfFile dbfFile)
        {
            var map = new GameObject("Map");
            ((IRenderable)shapeFile).Render(color, dbfFile, map);
            map.AddComponent<Map>();
        }
    }
}
