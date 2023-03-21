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

    // instance
    public static Script LuaEngine = new Script();
}
