using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(CreateTexture))]
public class CreateTextureInspector : Editor
{
    CreateTexture myTarget;

    public void OnEnable()
    {
        myTarget = (CreateTexture)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("New Texture"))
        {
            myTarget.GenerateTexture((int)myTarget.textureSize, myTarget.filterMode, myTarget.wrapMode);
        }
    }
}