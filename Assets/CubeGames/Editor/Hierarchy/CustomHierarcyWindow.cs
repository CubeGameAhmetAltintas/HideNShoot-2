using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarcyWindow : MonoBehaviour
{
    static CustomHierarcyWindow()
    {
        EditorApplication.hierarchyChanged += onWindowChanged;
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOmGUI;
    }

    static void onWindowChanged()
    {
        EditorApplication.RepaintHierarchyWindow();
    }

    static void HandleHierarchyWindowItemOmGUI(int inSelectionID, Rect inSelectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

        if (obj != null)
        {
            CustomHierarchyItem Label = obj.GetComponent<CustomHierarchyItem>();
            if (Label == null)
                return;

            if (Label != null && Event.current.type == EventType.Repaint)
            {
                #region Determine Styling

                bool ObjectIsSelected = Selection.instanceIDs.Contains(inSelectionID);

                Color BKCol = Label.BackgroundColor;
                Color TextCol = Label.TextColor;
                FontStyle TextStyle = Label.FontStyle;

                #endregion

                #region Draw Background

                //Only draw background if background color is not completely transparent
                if (BKCol.a > 0f)
                {
                    Rect BackgroundOffset = inSelectionRect;
                    BackgroundOffset.x = 0;
                    if (EditorWindow.focusedWindow != null)
                        BackgroundOffset.xMax = EditorWindow.focusedWindow.maxSize.x;

                    EditorGUI.DrawRect(BackgroundOffset, BKCol);
                    inSelectionRect.x = inSelectionRect.x + 20;

                    if (Label.Texture != null)
                        DrawIcon(inSelectionRect, Label.Texture, Label.IconSize);
                }

                #endregion

                EditorGUI.LabelField(inSelectionRect, Label.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = TextCol },
                    fontStyle = TextStyle
                });
            }
        }
    }

    private static void DrawIcon(Rect rect, Texture texture, Vector2 iconSize)
    {
        Rect r = new Rect(rect.x - 20, rect.y, iconSize.x, iconSize.y);
        GUI.DrawTexture(r, texture);
    }
}
#endif
