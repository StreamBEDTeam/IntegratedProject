using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

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
            //EditorGUI.LayerField()
            //EditorGUI.mas
            var layerNames = InternalEditorUtility.layers;
            int tmpMask = 0;
            for (int i = 0; i < layerNames.Length; i++)
            {
                if (((1 << LayerMask.NameToLayer(layerNames[i])) & mask.intValue) > 0)
                {
                    tmpMask |= 1 << i;
                }
            }
            tmpMask = EditorGUI.MaskField(
                position,
                label,
                tmpMask,
                layerNames
                );
            mask.intValue = 0;
            for (int i = 0; i < layerNames.Length; i++)
            {
                if (((1 << i) & tmpMask) > 0)
                {
                    mask.intValue |= 1 << LayerMask.NameToLayer(layerNames[i]);
                }
            }
        }
    }
}
