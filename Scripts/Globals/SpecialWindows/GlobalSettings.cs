using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class GlobalSettings 
{
    
    #region LogSettings
    [HideIf("@Devmode.m_EnableLogs")]
    [Button(ButtonSizes.Medium), GUIColor(1, 0.2f, 0)]
    private void LogsDisabled()
    {
        Devmode.m_EnableLogs = true;
        DoLogs();
    }

    [ShowIf("@Devmode.m_EnableLogs")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
    private void LogsEnabled()
    {
        Devmode.m_EnableLogs = false;
        DoLogs();
    }
    #endregion
    #region DevMode
    [HideIf("@Devmode.m_DevMode")]
    [Button(ButtonSizes.Medium), GUIColor(1, 0.2f, 0)]
    public void DevmodeDisabled()
    {
        Devmode.m_DevMode = true;
        DoDevMode();
    }

    [ShowIf("@Devmode.m_DevMode")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
    public void DevmodeEnabled()
    {
        Devmode.m_DevMode = false;
        DoDevMode();
    }
    #endregion
    #region ShowToDO
    [HideIf("@Devmode.m_ShowToDO")]
    [Button(ButtonSizes.Medium), GUIColor(1, 0.2f, 0)]
    private void ShowToDoDisabled()
    {
        Devmode.m_ShowToDO = true;
        DoToDo();
    }

    [ShowIf("@Devmode.m_ShowToDO")]

    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0)]
    private void ShowToDoEnabled()
    {
        Devmode.m_ShowToDO = false;
        DoToDo();
    }
    #endregion
    #region DevModeDisplay
    [OnValueChanged("OnDisplayChanged")]
    public DevmodeDisplays m_DevmodeDisplays;
	private void OnDisplayChanged()
	{
        Devmode.m_DevmodeDisplayType = m_DevmodeDisplays;
        FileBasedPrefs.SetInt("DisplayType", (int)m_DevmodeDisplays);
    }
	#endregion

    public void Start()
	{
        m_DevmodeDisplays = Devmode.m_DevmodeDisplayType;
	}
	private void DoLogs()
    {
	    SaveSettings();
    }

    private void DoDevMode()
    {
	    SaveSettings();
    }

    private void DoToDo()
    {
	    SaveSettings();
    }

    private void SaveSettings()
    {
        Devmode.SaveSettings();
    }
}
