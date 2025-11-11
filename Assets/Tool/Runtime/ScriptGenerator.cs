#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public  class ScriptGenerator
{
    public void GenerateScripts(string _path, List<string> _allScripts)
    {
        foreach (string _script in _allScripts)
        {
            string _newScriptText =
                "using UnityEngine;\r" +
                "\r" +
                "public class " + _script + " : MonoBehaviour\r" +
                "{\r" +
                "\tvoid Start()\r" +
                "\t{\r" +
                "\r" +
                "\t}\r" +
                "\r" +
                "\tvoid Update()\r" +
                "\t{\r" +
                "\r" +
                "\t}\r" +
                "}";

            string _fileName = _path + "/" + _script +".cs";
            File.WriteAllText(_fileName, _newScriptText);
        }
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }
}
#endif