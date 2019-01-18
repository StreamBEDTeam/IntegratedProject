using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using System;
using UnityEditorInternal;

[Serializable]
public class MaskAttribute : PropertyAttribute
{
    public int mask;

    [CustomPropertyDrawer(typeof(MaskAttribute))]
    public class MaskAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty mask = property.FindPropertyRelative("mask");
            mask.intValue = EditorGUI.MaskField(
                position,
                label,
                mask.intValue,
                InternalEditorUtility.layers);
        }
    }
}
