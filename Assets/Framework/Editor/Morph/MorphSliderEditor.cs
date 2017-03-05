namespace FrameworkHiena
{
    using UnityEngine;
    using UnityEditor;

    public class MorphSliderEditor : EditorWindow {

        private GameObject _currentGO;
        private SkinnedMeshRenderer _smr;
        private Mesh _m;

        [MenuItem("Framework Hiena/Morph/Slider Editor")]
        public static void GetMorphEditor()
        {
            GetWindow<MorphSliderEditor>().Show();
        }

        void OnGUI()
        {
            if(Selection.activeGameObject != null)
            {
                if(_currentGO != Selection.activeGameObject)
                {
                    _currentGO = Selection.activeGameObject;
                    _smr = _currentGO.GetComponent<SkinnedMeshRenderer>();
                    if(_smr != null) _m = _smr.sharedMesh;
                }

                if (_smr != null)
                {
                    for (int i = _m.blendShapeCount - 1; i >= 0; i--)
                    {
                        EditorGUILayout.LabelField("Blend Shape: " + _m.GetBlendShapeName(i));
                        _smr.SetBlendShapeWeight(i, EditorGUILayout.Slider(_smr.GetBlendShapeWeight(i), 0, 100));
                        EditorGUILayout.Space();
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Please select a GameObject with Blend Shapes or Morph");
                }
            }
            else
            {
                EditorGUILayout.LabelField("Please select a GameObject");
            }
        }
    }
}