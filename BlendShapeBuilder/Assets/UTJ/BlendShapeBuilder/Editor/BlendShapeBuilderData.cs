using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace UTJ.BlendShapeBuilder
{
    public enum ProjectVerticesRayDirection
    {
        CurrentNormals,
        BaseNomals,
    }

    [Serializable]
    public class BlendShapeFrameData
    {
        public float weight = 100.0f;
        public UnityEngine.Object mesh;
        public bool vertex = true;
        public bool normal = true;
        public bool tangent = true;

        public bool proj = false;
        public npProjectVerticesMode projMode = npProjectVerticesMode.ForwardAndBackward;
        public ProjectVerticesRayDirection projRayDir = ProjectVerticesRayDirection.CurrentNormals;
        public float projMaxRayDistance = 10.0f;
    }

    [Serializable]
    public class BlendShapeData
    {
        public bool fold = true;
        public string name = "";
        public List<BlendShapeFrameData> frames = new List<BlendShapeFrameData>();

        public void ClearInvalidFrames()
        {
            frames.RemoveAll(item => { return item.mesh == null; });
        }

        public void NormalizeWeights()
        {
            int n = frames.Count;
            float step = 100.0f / n;
            for (int i = 0; i < n; ++i)
            {
                frames[i].weight = step * (i + 1);
            }
        }

        public void SortByWeights()
        {
            frames.Sort((x, y) => x.weight.CompareTo(y.weight));
        }
    }

    [Serializable]
    public class BlendShapeBuilderData : ScriptableObject
    {
        public UnityEngine.Object baseMesh;
        public bool preserveExistingBlendShapes = false;
        public List<BlendShapeData> blendShapeData = new List<BlendShapeData>();
    }
}
