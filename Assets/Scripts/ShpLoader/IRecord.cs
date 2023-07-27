using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace ShpLoader
{
    public interface IRecord
    {
        void Load(ref BinaryReader br);
        long GetLength();
    }

    public interface IElement
    {
        void Load(ref BinaryReader br);
        long GetLength();
        string ToString();
    }

    public interface IRenderable
    {
        void Render(Color color, DbfFile dbfFile, GameObject parentGameObject);
    }

    public interface IRenderableData
    {
        GameObject Render(RangeXY range, Color color);
    }
}
