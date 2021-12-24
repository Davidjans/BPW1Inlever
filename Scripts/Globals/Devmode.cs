using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Devmode
{

    //[ShowIf("@Devmode.m_EnableLogs")]
    public static bool m_EnableLogs = true;
    //[ShowIf("@Devmode.m_DevMode")]
    public static bool m_DevMode = false;

    public static bool m_ShowToDO = true;

    public static bool m_DebugMode = true;
    //[ShowIf("@Devmode.m_DevmodeDisplayType", DevmodeDisplays.Normal)]
    public static DevmodeDisplays m_DevmodeDisplayType = DevmodeDisplays.Normal;

    private static DevModeData m_Data;
    private static string m_FileName = "CoolPeopleSettings";
    public static void LoadSettings()
    {
        if (SaveManager.CheckJsonExistence(m_FileName))
	    {
		    m_Data = JsonUtility.FromJson<DevModeData>(SaveManager.LoadTheJson(m_FileName));
	    }
	    else
        {
	        SaveToData();
		    SaveManager.SaveTheJson(m_FileName, SaveManager.TurnIntoJson<DevModeData>(m_Data));
		    m_Data = JsonUtility.FromJson<DevModeData>(SaveManager.LoadTheJson(m_FileName));
	    }

       LoadFromData();
    }

    public static void SaveSettings()
    {
	    SaveManager.SaveTheJson(m_FileName, SaveManager.TurnIntoJson<DevModeData>(m_Data));
    }

    private static void SaveToData()
    {
	    m_Data = new DevModeData();
	    m_Data.m_EnableLogs = m_EnableLogs;
	    m_Data.m_DevMode = m_DevMode;
	    m_Data.m_ShowToDO = m_ShowToDO;
	    m_Data.m_DebugMode = m_DebugMode;
	    m_Data.m_DevmodeDisplayType = m_DevmodeDisplayType;
    }
    private static void LoadFromData()
    {
	    m_EnableLogs = m_Data.m_EnableLogs;
	    m_DevMode = m_Data.m_DevMode;
	    m_ShowToDO = m_Data.m_ShowToDO;
	    m_DebugMode = m_Data.m_DebugMode;
	    m_DevmodeDisplayType = m_Data.m_DevmodeDisplayType;
    }
}

public class DevModeData
{
	public bool m_EnableLogs = true;
	public bool m_DevMode = false;
	public bool m_ShowToDO = true;
	public bool m_DebugMode = true;
	public DevmodeDisplays m_DevmodeDisplayType = DevmodeDisplays.Normal;
}
