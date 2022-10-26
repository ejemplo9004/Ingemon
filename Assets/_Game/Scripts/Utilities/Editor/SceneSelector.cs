using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSelector : EditorWindow
{
    private EditorScenes scene;

    [MenuItem("Ingemon/Scene Selector")]
    public static void ShowWindow()
    {
        GetWindow<SceneSelector>("Scene Selector");
    }

    private void OnGUI()
    {
        scene = (EditorScenes)EditorGUILayout.EnumPopup("Select the scene:", scene);
        if (GUILayout.Button("Open Scene"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/_Game/Scenes/" + scene + ".unity");
        }
    }
}
