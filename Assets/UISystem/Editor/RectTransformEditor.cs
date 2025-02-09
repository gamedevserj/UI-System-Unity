using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UISystem.EditorScripts
{
    [CustomEditor(typeof(RectTransform), true)]
    public class RectTransformEditor : Editor
    {
        private Editor _editorInstance;

        private void OnEnable()
        {
            Assembly ass = Assembly.GetAssembly(typeof(Editor));
            Type rtEditor = ass.GetType("UnityEditor.RectTransformEditor");
            _editorInstance = CreateEditor(target, rtEditor);
        }

        private void OnDisable()
        {
            DestroyImmediate(_editorInstance);
        }

        public override void OnInspectorGUI()
        {
            _editorInstance.OnInspectorGUI();
            if (GUILayout.Button("Anchors to corners"))
            {
                AnchorsToCorners();
            }
        }

        private void OnSceneGUI()
        {
            MethodInfo onSceneGUI_Method = _editorInstance.GetType().GetMethod("OnSceneGUI", BindingFlags.NonPublic | BindingFlags.Instance);
            onSceneGUI_Method.Invoke(_editorInstance, null);
        }

        // code is taken from
        // https://discussions.unity.com/t/unity-4-6-beta-anchor-snap-to-button-new-ui-system/115487/2
        private void AnchorsToCorners()
        {
            RectTransform t = Selection.activeTransform as RectTransform;
            RectTransform pt = Selection.activeTransform.parent as RectTransform;

            if (t == null || pt == null) return;

            Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                                t.anchorMin.y + t.offsetMin.y / pt.rect.height);
            Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                                t.anchorMax.y + t.offsetMax.y / pt.rect.height);

            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }

    }
}