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
    public static readonly string StateFilePath = $"{Constants.MapFilePath}/state";

    public static readonly string HistoryFilePath = $"{Constants.GameDataPath}/history";
    public static readonly string CountryFilePath = $"{Constants.GameDataPath}/history/country";

    // instance
    public static Script LuaEngine = new Script();

    // camera
    public static float MinCameraPositionY = 10;
    public static float MaxCameraPositionY = 200;
    public static float MoveCameraSpeed = 100;
}
