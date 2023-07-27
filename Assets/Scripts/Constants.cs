using System;
using System.IO;
using UnityEngine;
using MoonSharp.Interpreter;

public static class Constants
{
    //public static readonly string GameDataPath = $"{Application.dataPath}/GameData";
    public static readonly string GameDataPath = $"{Application.dataPath}/../GameData"; // for debug

    public static readonly string ScriptExtension = ".lua";

    // file path
    public static readonly string ShpFilePath = $"{Constants.GameDataPath}/ne2_10m/ne_10m_admin_1_states_provinces.shp";
    public static readonly string DbfFilePath = $"{Constants.GameDataPath}/ne2_10m/ne_10m_admin_1_states_provinces.dbf";

    public static readonly string MapFilePath = $"{Constants.GameDataPath}/map";
    public static readonly string StateFilePath = $"{Constants.MapFilePath}/states";

    public static readonly string HistoryFilePath = $"{Constants.GameDataPath}/history";
    public static readonly string CountryFilePath = $"{Constants.HistoryFilePath}/countries";

    public static readonly string GraphicsFilePath = $"{Constants.GameDataPath}/graphics";
    public static readonly string FlagsFilePath = $"{Constants.GraphicsFilePath}/flags";

    // object record
    public static readonly string ObjectRecordProvinceIdKey = "adm1_code";

    // instance
    public static Script LuaEngine = new Script();

    // camera
    public static float MinCameraPositionY = 10;
    public static float MaxCameraPositionY = 200;
    public static float MoveCameraSpeed = 100;

    public static Sprite LoadPng(string filePath)
    {
        using (var fs = new FileStream(filePath, FileMode.Open))
        {
            using (var br = new BinaryReader(fs))
            {
                var bytes = br.ReadBytes((int)br.BaseStream.Length);
                var tex = new Texture2D(0, 0);
                tex.LoadImage(bytes);
                return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            }
        }
    }
}
