using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    MovingPlatform _target;

    private void OnEnable()
    {
        _target = (MovingPlatform)target;
    }

    public override void OnInspectorGUI()
    {
        _target.speed = EditorGUILayout.FloatField("Speed", _target.speed);

        GUI.enabled = false;
        EditorGUILayout.Vector3Field("Final position", _target.finalPos);
    }

    private void OnSceneGUI()
    {
        EditorGUI.BeginChangeCheck();
        Vector3 newFinalPos = Handles.PositionHandle(new Vector3(_target.finalPos.x,
                                                              _target.finalPos.y,
                                                              0), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            //Round Z to 0 to avoid platforming problems.
            Undo.RecordObject(target, "Changed final position");
            _target.finalPos = newFinalPos;
        }
    }
}
