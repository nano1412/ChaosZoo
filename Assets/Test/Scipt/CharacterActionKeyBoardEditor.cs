using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(LeftCharacterActionKeyBoard))]
public class CharacterActionKeyBoardEditor : Editor
{
   private ReorderableList qcfList;
    private ReorderableList qcbList;
    private ReorderableList hcbfList;

    private void OnEnable()
    {
        qcfList = new ReorderableList(serializedObject, 
            serializedObject.FindProperty("qcfMoves"), 
            true, true, true, true);

        qcfList.drawElementCallback = (rect, index, active, focused) =>
        {
            var element = qcfList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };

        qcbList = new ReorderableList(serializedObject, 
            serializedObject.FindProperty("qcbMoves"), 
            true, true, true, true);

        qcbList.drawElementCallback = (rect, index, active, focused) =>
        {
            var element = qcbList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };

        hcbfList = new ReorderableList(serializedObject, 
            serializedObject.FindProperty("hcbfMoves"), 
            true, true, true, true);

        hcbfList.drawElementCallback = (rect, index, active, focused) =>
        {
            var element = hcbfList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("QCF Moves", EditorStyles.boldLabel);
        qcfList.DoLayoutList();

        EditorGUILayout.LabelField("QCB Moves", EditorStyles.boldLabel);
        qcbList.DoLayoutList();

        EditorGUILayout.LabelField("HCBF Moves", EditorStyles.boldLabel);
        hcbfList.DoLayoutList();
        
        serializedObject.ApplyModifiedProperties();
    }
}

public enum SpecialMove
{
    QCF_Punch,
    QCF_Kick,
    QCF_Slash,
    QCF_HeavySlash,
    QCB_Punch,
    QCB_Kick,
    QCB_Slash,
    QCB_HeavySlash,
    HCBF_Punch,
    HCBF_Kick,
    HCBF_Slash,
    HCBF_HeavySlash
}