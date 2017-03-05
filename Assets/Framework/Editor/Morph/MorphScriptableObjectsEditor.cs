namespace FrameworkHiena
{
    using UnityEditor;
    using UnityEngine;
    //using UnityEditor;

    public class MorphScriptableObjectsEditor : EditorWindow {

        private GameObject _go;
        private MorphContainer _morphContainer;
        private SkinnedMeshRenderer _smr;
        private Mesh _m;

        [MenuItem("Framework Hiena/Morph/Scriptable Objects")]
        public static void GetScriptableObjects()
        {
            GetWindow<MorphScriptableObjectsEditor>();
        }

        void OnGUI()
        {
            if(GUILayout.Button("Create Morph Container"))
            {
                ScriptableObjectCreator.CreateScriptableObject<MorphContainer>();
                _morphContainer = (MorphContainer)Selection.activeObject;
            }
            EditorGUILayout.HelpBox("Morph Container will be created inside the path where you are selecting or inside Assets", MessageType.Info);
            _morphContainer = (MorphContainer)EditorGUILayout.ObjectField("Morph Container", _morphContainer, typeof(MorphContainer), false);
            if (_morphContainer != null)
            {
                GameObject aux = (GameObject)EditorGUILayout.ObjectField("GameObject", _go, typeof(GameObject), true);
                if(aux != _go)
                {
                    _go = aux;
                    _smr = _go.GetComponent<SkinnedMeshRenderer>();
                    if (_smr != null) _m = _smr.sharedMesh;
                }
                if(_smr != null)
                {
                    if(GUILayout.Button("Copy data from Mesh to Morph Container"))
                    {
                        float[] values = new float[_m.blendShapeCount];
                        for (int i = _m.blendShapeCount - 1; i >= 0; i--)
                        {
                            values[i] = _smr.GetBlendShapeWeight(i);
                        }
                        _morphContainer.values = values;
                    }
                    if (_morphContainer.values == null)
                    {
                        EditorGUILayout.HelpBox("Morph Container doesn't have any values. You won't be able to copy data from Morph Container to Mesh", MessageType.Info);
                    }
                    else if(_morphContainer.values.Length != _m.blendShapeCount)
                    {
                        EditorGUILayout.HelpBox("Morph Container has a different ammount of values than the Blend Shapes Count. You won't be able to copy data from Morph Container to Mesh", MessageType.Info);
                    }
                    else
                    {
                        if (GUILayout.Button("Copy data from Morhp Container to Mesh"))
                        {
                            for (int i = _m.blendShapeCount - 1; i >= 0; i--)
                            {
                                _smr.SetBlendShapeWeight(i, _morphContainer.values[i]);
                            }
                        }
                    }
                }
                else
                {
                    if (_go != null) EditorGUILayout.HelpBox("The selected GameObject doesn't have a Skinned Mesh Renderer", MessageType.Error);
                    else EditorGUILayout.HelpBox("Please select a GameObject with Skinned Mesh Renderer", MessageType.Info);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Please insert a ScriptableObject of Morph Container", MessageType.Info);
            }
        }
    }
}