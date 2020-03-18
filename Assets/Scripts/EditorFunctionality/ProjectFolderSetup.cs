

using UnityEngine;
using UnityEditor;
using System.IO;

//Generate Folders in our project    
namespace EditorMethods
{
#if UNITY_EDITOR
    public class FolderSetup
    {
        //Add menu item
        [MenuItem("Project Tools/FolderSetup")]
        //Init
        private static void Init()
        {
            GenerateFolders();
        }

        //Generate folder and names
        private static void GenerateFolders()
        {
            var projectPath = Application.dataPath + "/"; //Store project Path

            //Create folders
            Directory.CreateDirectory(projectPath + "Plugins");
            Directory.CreateDirectory(projectPath + "Sandbox");
            Directory.CreateDirectory(projectPath + "3rd-Party");
            Directory.CreateDirectory(projectPath + "Music");
            Directory.CreateDirectory(projectPath + "SFX");
            Directory.CreateDirectory(projectPath + "Audio");
            Directory.CreateDirectory(projectPath + "Fonts");
            Directory.CreateDirectory(projectPath + "Animation");
            Directory.CreateDirectory(projectPath + "Materials");
            Directory.CreateDirectory(projectPath + "Models");
            Directory.CreateDirectory(projectPath + "Prefabs");
            Directory.CreateDirectory(projectPath + "Editor");
            Directory.CreateDirectory(projectPath + "Scenes");
            Directory.CreateDirectory(projectPath + "Scripts");
            Directory.CreateDirectory(projectPath + "Textures");
            Directory.CreateDirectory(projectPath + "Shaders");
            Directory.CreateDirectory(projectPath + "Others");

            AssetDatabase.Refresh(); //Refresh Project folder in editor
        }
    }
#endif
}
