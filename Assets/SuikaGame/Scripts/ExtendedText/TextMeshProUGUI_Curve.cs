using TMPro;
using UnityEngine;

namespace SuikaGame.Scripts
{
    public class TextMeshProUGUI_Curve2 : TextMeshProUGUI
    {
        [SerializeField] private AnimationCurve vertexCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.25f, 2.0f), new Keyframe(0.5f, 0), new Keyframe(0.75f, 2.0f), new Keyframe(1, 0f));
        [SerializeField] private float curveScale = 1.0f;
        
        // public new TMP_TextInfo textInfo
        // {
        //     get
        //     {
        //         Debug.Log("textInfo");
        //         return base.textInfo;
        //     }
        // }
        //
        // public new string text
        // {
        //     get => base.text;
        //
        //     set
        //     {
        //         ApplyCurve();
        //         base.text = value;
        //         ApplyCurve();
        //     }
        // }

        protected override void Start()
        {
            base.Start();
            // ApplyCurve();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ApplyCurve();
        }
        
        public override void SetVertices(Vector3[] vertices)
        {
            Debug.Log($"SetVertices");
            base.SetVertices(vertices);
        }

        public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
        {
            Debug.Log($"UpdateVertexData TMP_VertexDataUpdateFlags");
            base.UpdateVertexData(flags);
        }

        public override void UpdateVertexData()
        {
            Debug.Log($"UpdateVertexData");
            base.UpdateVertexData();
        }

        protected override void OnValidate()
        {
            Debug.Log($"VALIDATE {curveScale}");
            ApplyCurve();
            base.OnValidate();
            ApplyCurve();
        }

        public override void SetVerticesDirty()
        {
            Debug.Log($"SetVerticesDirty");
            base.SetVerticesDirty();
            // ApplyCurve();
        }
        
        [ContextMenu("Cgfasf Curve")]
        private void ChangeTExt()
        {
            text = "ASDASD";
        }
        
        private void ApplyCurve()
        {
            Debug.Log($"VALIDATE START");
            
            var tmpText = this;
            if (tmpText == null)
            {
                Debug.LogWarning($"tmpText is null");
                return;
            }
            
            vertexCurve.preWrapMode = WrapMode.Clamp;
            vertexCurve.postWrapMode = WrapMode.Clamp;

            //Mesh mesh = m_TextComponent.textInfo.meshInfo[0].mesh;
            
            tmpText.havePropertiesChanged = true; // Need to force the TextMeshPro Object to be updated.
            
            Debug.Log($"VALIDATE 1");
            if (!tmpText.havePropertiesChanged)
                return;

            tmpText.ForceMeshUpdate(); // Generate the mesh and populate the textInfo with data we can use and manipulate.

            TMP_TextInfo textInfo = tmpText.textInfo;
            Debug.Log($"VALIDATE 2");
            if(textInfo == null)
                return;

            int characterCount = textInfo.characterCount;
            
            Debug.Log($"VALIDATE 3 | {characterCount}");
            if (characterCount == 0)
                return;

            //vertices = textInfo.meshInfo[0].vertices;
            //int lastVertexIndex = textInfo.characterInfo[characterCount - 1].vertexIndex;

            float boundsMinX = tmpText.bounds.min.x; //textInfo.meshInfo[0].mesh.bounds.min.x;
            float boundsMaxX = tmpText.bounds.max.x; //textInfo.meshInfo[0].mesh.bounds.max.x;

            for (int i = 0; i < characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Get the index of the mesh used by this character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                var vertices = textInfo.meshInfo[materialIndex].vertices;

                // Compute the baseline mid point for each character
                Vector3 offsetToMidBaseline =
                    new Vector2((vertices[vertexIndex + 0].x + vertices[vertexIndex + 2].x) / 2,
                        textInfo.characterInfo[i].baseLine);
                //float offsetY = VertexCurve.Evaluate((float)i / characterCount + loopCount / 50f); // Random.Range(-0.25f, 0.25f);

                // Apply offset to adjust our pivot point.
                vertices[vertexIndex + 0] += -offsetToMidBaseline;
                vertices[vertexIndex + 1] += -offsetToMidBaseline;
                vertices[vertexIndex + 2] += -offsetToMidBaseline;
                vertices[vertexIndex + 3] += -offsetToMidBaseline;

                // Compute the angle of rotation for each character based on the animation curve
                float x0 = (offsetToMidBaseline.x - boundsMinX) /
                           (boundsMaxX - boundsMinX); // Character's position relative to the bounds of the mesh.
                float x1 = x0 + 0.0001f;
                float y0 = vertexCurve.Evaluate(x0) * curveScale;
                float y1 = vertexCurve.Evaluate(x1) * curveScale;
                
                Vector3 horizontal = new Vector3(1, 0, 0);
                //Vector3 normal = new Vector3(-(y1 - y0), (x1 * (boundsMaxX - boundsMinX) + boundsMinX) - offsetToMidBaseline.x, 0);
                Vector3 tangent = new Vector3(x1 * (boundsMaxX - boundsMinX) + boundsMinX, y1) -
                                  new Vector3(offsetToMidBaseline.x, y0);

                float dot = Mathf.Acos(Vector3.Dot(horizontal, tangent.normalized)) * 57.2957795f;
                Vector3 cross = Vector3.Cross(horizontal, tangent);
                float angle = cross.z > 0 ? dot : 360 - dot;

                var matrix = Matrix4x4.TRS(new Vector3(0, y0, 0), Quaternion.Euler(0, 0, angle), Vector3.one);

                vertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                vertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                vertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                vertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);

                vertices[vertexIndex + 0] += offsetToMidBaseline;
                vertices[vertexIndex + 1] += offsetToMidBaseline;
                vertices[vertexIndex + 2] += offsetToMidBaseline;
                vertices[vertexIndex + 3] += offsetToMidBaseline;
            }
            
            // Upload the mesh with the revised information
            Debug.Log($"VALIDATE UpdateVertexData");
            tmpText.UpdateVertexData();
            Debug.Log($"VALIDATE END");
        }
    }


    public class TextMeshProUGUI_Curve : TextMeshProUGUI
    {
        [SerializeField] private AnimationCurve vertexCurve = new AnimationCurve(new Keyframe(0, 0),
            new Keyframe(0.25f, 2.0f), new Keyframe(0.5f, 0), new Keyframe(0.75f, 2.0f), new Keyframe(1, 0f));

        [SerializeField] private float curveScale = 1.0f;

        public new string text
        {
            get => base.text;
        
            set
            {
                ApplyCurve();
                base.text = value;
                ApplyCurve();
            }
        }
        
        // protected override void Start()
        // {
        //     base.Start();
        //     ApplyCurve();
        // }
        //
        // protected override void OnEnable()
        // {
        //     base.OnEnable();
        //     ApplyCurve();
        // }

        protected override void OnValidate()
        {
            base.OnValidate();
            ApplyCurve();
        }

        public override void SetVerticesDirty()
        {
            base.SetVerticesDirty();
            ApplyCurve();
        }

        private void ApplyCurve()
        {
            TMP_Text tmpText = this;
            if (tmpText == null)
            {
                Debug.LogWarning("tmpText is null");
                return;
            }

            vertexCurve.preWrapMode = WrapMode.Clamp;
            vertexCurve.postWrapMode = WrapMode.Clamp;

            tmpText.ForceMeshUpdate(); // Ensure the mesh is updated

            TMP_TextInfo textInfo = tmpText.textInfo;
            if (textInfo == null) return;

            int characterCount = textInfo.characterCount;
            if (characterCount == 0) return;

            float boundsMinX = tmpText.bounds.min.x;
            float boundsMaxX = tmpText.bounds.max.x;

            for (int i = 0; i < characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                Vector3 offsetToMidBaseline =
                    new Vector2((vertices[vertexIndex + 0].x + vertices[vertexIndex + 2].x) / 2,
                        textInfo.characterInfo[i].baseLine);

                // Apply offset to adjust the pivot point.
                vertices[vertexIndex + 0] += -offsetToMidBaseline;
                vertices[vertexIndex + 1] += -offsetToMidBaseline;
                vertices[vertexIndex + 2] += -offsetToMidBaseline;
                vertices[vertexIndex + 3] += -offsetToMidBaseline;

                float x0 = (offsetToMidBaseline.x - boundsMinX) / (boundsMaxX - boundsMinX);
                float x1 = x0 + 0.0001f;
                float y0 = vertexCurve.Evaluate(x0) * curveScale;
                float y1 = vertexCurve.Evaluate(x1) * curveScale;

                Vector3 horizontal = new Vector3(1, 0, 0);
                Vector3 tangent = new Vector3(x1 * (boundsMaxX - boundsMinX) + boundsMinX, y1) -
                                  new Vector3(offsetToMidBaseline.x, y0);
                float dot = Mathf.Acos(Vector3.Dot(horizontal, tangent.normalized)) * Mathf.Rad2Deg;
                Vector3 cross = Vector3.Cross(horizontal, tangent);
                float angle = cross.z > 0 ? dot : 360 - dot;

                Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0, y0, 0), Quaternion.Euler(0, 0, angle), Vector3.one);

                vertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                vertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                vertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                vertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);

                vertices[vertexIndex + 0] += offsetToMidBaseline;
                vertices[vertexIndex + 1] += offsetToMidBaseline;
                vertices[vertexIndex + 2] += offsetToMidBaseline;
                vertices[vertexIndex + 3] += offsetToMidBaseline;
            }

            tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }
}