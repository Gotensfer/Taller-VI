using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LaunchModule))]
public class EditorLaunchModule : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.Space();
        
        LaunchModule lm = (LaunchModule)target;
        
        lm.force = EditorGUILayout.FloatField("Fuerza:", lm.force);
        
        EditorGUILayout.Space();
        
        lm.angle = (float)EditorGUILayout.Slider("Angulo: ", lm.angle, 0, 90);
    }
}
