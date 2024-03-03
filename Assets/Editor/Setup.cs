using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.AssetDatabase;

namespace Editor
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Refresh")]
        public static void CreateFolders()
        {
            Folders.CreateDefault("Assets","Art","Editor","Source","Materials","Prefabs","ScriptableObjects","Settings");
            Refresh();
        }

        private static class Folders
        {
            public static void CreateDefault(string a_root, params string[] a_folders)
            {
                var fullPath = Path.Combine(Application.dataPath, a_root);

                foreach (var folder in a_folders)
                {
                    var path = Path.Combine(fullPath, folder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
    }
}