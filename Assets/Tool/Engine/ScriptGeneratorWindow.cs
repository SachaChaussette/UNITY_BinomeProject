#if UNITY_EDITOR
using UnityEditor;

public class ScriptGeneratorWindow
{
    // MenuItem => Créer un onglet dans d'Unity
    [MenuItem("Tool/ScriptGenerator")]
    static void OpenWindow()
    {
        // Ouvre la window de ScriptGeneratorEditor
        EditorWindow.GetWindow<ScriptGeneratorEditor>();
    }
}
#endif