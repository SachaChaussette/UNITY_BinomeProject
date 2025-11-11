#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptGeneratorEditor : EditorWindow
{
    // Bonjour à celui qui va ouvrir ce script pour regarder comment ça marche, j'ai expliqué en commentaire mais woulah attendez le cours c'est chiant

    event Action updateWindow;

    List<string> scriptsNames = new List<string>();
    string newScriptName = "";
    string pathSelected = Application.dataPath;

    Vector2 windowSize = Vector2.zero;
    ScriptGenerator generator = new ScriptGenerator();

    // Franchement event ou pas event c'est pareil mais c'est l'archi de FranFran
    private void OnEnable()
    {
        updateWindow += UpdateTool;
    }

    // OnGUI c'est le update 
    private void OnGUI()
    {
        windowSize = position.size;
        updateWindow?.Invoke();
    }

    void UpdateTool()
    {
        DrawTitle("Script Generator");
        PathSection();
        AddScriptLayout();
        DrawTitle("Scrips to Create");
        ShowAllScripts();
        GenerateButton();
    }
    
    // J'écris un titre, le Begin/End Horizontal permet d'afficher des éléments horizontalement au lieu de Verticalement
    // FlexibleSpace permet d'encadrer des éléments dans la fenêtre pour que se soit au centre
    void DrawTitle(string _title)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(_title, EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    // Le Button fonctionne simplement, il r'envoie un Booléen et si tu appuies dessus c'est True
    // Faut prendre en compte que le script se joue à chaque tick si c'est pas plus souvent, donc chaque éléments modifiés doivent être bien géré
    void AddScriptLayout()
    {
        EditorGUILayout.BeginHorizontal();
        newScriptName = EditorGUILayout.TextField(newScriptName);
        if (IsNameValid())
        {
            if (GUILayout.Button("+"))
            {
                scriptsNames.Add(newScriptName);
                newScriptName = "";
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    void ShowAllScripts()
    {
        foreach (string _str in scriptsNames)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(_str, EditorStyles.boldLabel, GUILayout.Width(windowSize.x / 1.5f));
            if (GUILayout.Button("-"))
            {
                scriptsNames.Remove(_str);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    bool IsNameValid()
    {
        if (string.IsNullOrEmpty(newScriptName))
            return false;

        if (scriptsNames.Contains(newScriptName))
            return false;

        return true;
    }

    void GenerateButton()
    {
        if (scriptsNames.Count == 0) return;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Generate Scripts"))
        {
            generator.GenerateScripts(pathSelected, scriptsNames);
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    // EditorUtility.OpenFolderPanel Permet d'ouvrir une fenêtre, le Application.dataPath permet de récupérer le dossier Asset + le chemin avant pour connaître l'emplacement du projet Unity
    void PathSection()
    {
        DrawTitle("Current Path : " + pathSelected);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Change Path of Scripts"))
        {
            pathSelected = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
#endif