#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Demos.RPGEditor;
using UnityEditor;
using UnityEngine;
using System.Linq;


public class DavidWindow : OdinMenuEditorWindow
{
    [MenuItem("David/DavidWindow")]
    private static void Open()
    {
        var window = GetWindow<DavidWindow>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);
        tree.DefaultMenuStyle.IconSize = 28.00f;
        tree.Config.DrawSearchToolbar = true;

        GlobalSettings globalSettings = new GlobalSettings();
        globalSettings.Start();
        tree.Add("Tools/GlobalSettings", globalSettings);

        return tree;
    }

    private void AddDragHandles(OdinMenuItem menuItem)
    {
        menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
    }

    protected override void OnBeginDrawEditors()
    {
        var selected = this.MenuTree.Selection.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight; 

        // Draws a toolbar with the name of the currently selected menu item.
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif
