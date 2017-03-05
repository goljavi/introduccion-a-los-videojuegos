namespace FrameworkHiena
{
    using UnityEngine;
    using UnityEditor;

    public static class ScriptableObjectCreator
    {
        /// <summary>
        /// Creates a ScriptableObject at Assets/Framework Hiena/Output with the name of the type.
        /// </summary>
        /// <typeparam name="T">Type of the ScriptableObject. Must inherate from ScriptableObject</typeparam>
        public static void CreateScriptableObject<T>() where T : ScriptableObject
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), AssetDatabase.GenerateUniqueAssetPath("Assets/Framework Hiena/Output/" + typeof(T).ToString() + ".asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Creates a ScriptableObject at given path with the name of the type.
        /// </summary>
        /// <typeparam name="T">Type of the ScriptableObject. Must inherate from ScriptableObject</typeparam>
        /// <param name="path">Path where is going to be saved. Example: "Assets/"</param>
        public static void CreateScriptableObject<T>(string path) where T : ScriptableObject
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), AssetDatabase.GenerateUniqueAssetPath(path + typeof(T).ToString() + ".asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Creates a ScriptableObject at given path with given name.
        /// </summary>
        /// <typeparam name="T">Type of the ScriptableObject. Must inherate from ScriptableObject</typeparam>
        /// <param name="path">Path where is going to be saved. Example: "Assets/"</param>
        /// <param name="name">Name of the file. Example "AwesomeAsset"</param>
        public static void CreateScriptableObject<T>(string path, string name) where T : ScriptableObject
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), AssetDatabase.GenerateUniqueAssetPath(path + name + ".asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}