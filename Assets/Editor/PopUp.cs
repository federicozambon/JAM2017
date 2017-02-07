/*
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ActionList))]
public class SomeEditor : Editor
{
    int[] selection;

    ActionList aL;
    SerializedProperty sr_Prop, SceneVar_Prop, minute_Prop, result_Prop, frameProp, boolProp, arraySizeProp;
    public void OnEnable()
    {
        selection = new int[5];

        SceneVar_Prop = serializedObject.FindProperty("actionList");

        aL = FindObjectOfType<ActionList>();
    }

    string[] _choices = new[] { "Guardialinee", "ArbitroDiLinea", "Arbitro1", "Arbitro2", "Arbitro3" };
    int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // Draw the default inspector
        //DrawDefaultInspector();
        //_choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        EditorGUILayout.PropertyField(SceneVar_Prop);
        if (SceneVar_Prop.isExpanded)
        {
            int ListSize = SceneVar_Prop.arraySize;

            EditorGUILayout.IntField("Size " ,ListSize);

            for (int i = 0; i < SceneVar_Prop.arraySize; i++)
            {
                
                SerializedProperty nuovaLista = SceneVar_Prop.GetArrayElementAtIndex(i);
            
                result_Prop = nuovaLista.FindPropertyRelative("minuteAndHalf");
                minute_Prop = nuovaLista.FindPropertyRelative("matchResult");
                boolProp = nuovaLista.FindPropertyRelative("frameList");
                frameProp = nuovaLista.FindPropertyRelative("frameGo");
                sr_Prop = nuovaLista.FindPropertyRelative("sr");

                
                EditorGUILayout.Toggle(boolProp.boolValue);
                EditorGUILayout.TextArea(result_Prop.stringValue);
                EditorGUILayout.TextArea(minute_Prop.stringValue);

                for (int j = 0; j < sr_Prop.arraySize; j++)
                {
                    EditorGUILayout.PropertyField(frameProp);
                    EditorGUILayout.PropertyField(sr_Prop);
                    selection[j] = EditorGUILayout.Popup(selection[j], _choices);
                }

            }

        }
        foreach (var clas in aL.actionList)
        {
            //var someClass = target as SceneVariable;
        }
        // Update the selected choice in the underlying object
        //someClass.spriteIndex = _choices[_choiceIndex];
        serializedObject.ApplyModifiedProperties();
    }
    
}
*/