using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefab : EditorWindow
{
    [MenuItem("Tools/Replace With Prefab")]
    public static void ShowWindow() => GetWindow<ReplaceWithPrefab>("Replace With Prefab");

    private GameObject prefab;

    private void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        if ( GUILayout.Button("Replace Selected") && prefab != null )
        {
            foreach ( GameObject go in Selection.gameObjects )
            {
                GameObject newGo = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                newGo.transform.SetParent(go.transform.parent);
                newGo.transform.localPosition = go.transform.localPosition;
                newGo.transform.localRotation = go.transform.localRotation;
                newGo.transform.localScale = go.transform.localScale;
                Undo.RegisterCreatedObjectUndo(newGo, "Replace With Prefab");
                Undo.DestroyObjectImmediate(go);
            }
        }
    }
}
