using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tool.Mesh
{

    public class MeshPointController : ControllerBaseModel
    {
        public MeshModel MeshModel;
        public Vector3Int MeshSize;
        public List<MeshPointModel> MeshPoints;
        [Header("Editor Values")]
        public bool ShowHandles;
        public Vector3 LocalPositionOffset;
        public Vector3 Offset;
        public Vector3 RandomizeMin, RandomizeMax;
        public float GizmoScale;
        public Direction Direction;
        public Direction UVSDirection;
        public bool IsReverse;
        [SerializeField] Vector3[] e_points;
        int e_PointIndex;
        [SerializeField] Camera camera;
        [SerializeField] float depth;

        public override void Initialize()
        {
            base.Initialize();

        }

#if UNITY_EDITOR
        private void Update()
        {
            if (e_points == null || e_points.Length < 2)
            {
                e_points = new Vector3[2];
                for (int i = 0; i < e_points.Length; i++)
                {
                    e_points[i] = Vector3.zero;
                }
            }

            if (Input.GetKey(KeyCode.Y))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 pos = Input.mousePosition;
                    pos.z = depth;
                    e_points[e_PointIndex] = camera.ScreenToWorldPoint(pos);
                    e_PointIndex = e_PointIndex + 1 < e_points.Length ? e_PointIndex + 1 : e_PointIndex;
                }
            }

            if (Input.GetKeyUp(KeyCode.Y))
            {
                e_PointIndex = 0;
                createPoints();
            }
        }

        private void createPoints()
        {
            int count = 0;
            MeshPoints = new List<MeshPointModel>();
            int width = 0;
            int height = 0;
            Vector3 startPoint = e_points[0];
            Vector3 diff = Vector3.zero;

            switch (Direction)
            {
                case Direction.X:
                    count = MeshSize.x * MeshSize.y;
                    width = MeshSize.x;
                    height = MeshSize.y;
                    diff = new Vector3(Mathf.Abs(e_points[0].x - e_points[1].x) / MeshSize.x, Mathf.Abs(e_points[0].y - e_points[1].y) / MeshSize.y, 0);
                    break;
                case Direction.Y:
                    count = MeshSize.x * MeshSize.z;
                    width = MeshSize.x;
                    height = MeshSize.z;
                    diff = new Vector3(Mathf.Abs(e_points[0].x - e_points[1].x) / MeshSize.x, 0, Mathf.Abs(e_points[0].z - e_points[1].z) / MeshSize.z);
                    break;
                case Direction.Z:
                    count = MeshSize.z * MeshSize.y;
                    width = MeshSize.z;
                    height = MeshSize.y;
                    diff = new Vector3(0, Mathf.Abs(e_points[0].y - e_points[1].y) / MeshSize.y, Mathf.Abs(e_points[0].z - e_points[1].z) / MeshSize.z);

                    break;
                default:
                    break;
            }


            int x = 0;
            int y = 0;


            for (int i = 0; i < count; i++)
            {
                Vector3 pos = new Vector3();
                switch (Direction)
                {
                    case Direction.X:
                        pos = startPoint + new Vector3(diff.x * x, diff.y * y, 0);
                        break;
                    case Direction.Y:
                        pos = startPoint + new Vector3(diff.x * x, 0, diff.z * y);
                        break;
                    case Direction.Z:
                        pos = startPoint + new Vector3(0, diff.y * y, diff.z * x);
                        break;
                    default:
                        break;
                }

                Ray ray = new Ray(pos, camera.transform.forward);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit))
                {
                    MeshPoints.Add(new MeshPointModel(hit.point));
                }

                x++;
                if (x >= width)
                {
                    x = 0;
                    y++;
                }
            }

        }

#endif

        public void CreateMesh(BoxCollider collider)
        {
            for (int i = 0; i < MeshPoints.Count; i++)
            {
                if (collider.bounds.Contains(MeshPoints[i].LocalPosition))
                {
                    MeshPoints[i].IsSelected = true;
                }
            }
            updateMesh();
        }

        public void DeactiveMeshPoint(BoxCollider collider, Action<Vector3> onHit = null)
        {
            for (int i = 0; i < MeshPoints.Count; i++)
            {
                if (MeshPoints[i].IsSelected)
                {
                    if (collider.bounds.Contains(MeshPoints[i].LocalPosition))
                    {
                        MeshPoints[i].IsSelected = false;
                        onHit?.Invoke(MeshPoints[i].LocalPosition);
                    }
                }
            }
            updateMesh();
        }

        public void SetActiveAllPoint()
        {
            for (int i = 0; i < MeshPoints.Count; i++)
            {
                MeshPoints[i].IsSelected = true;
            }
            updateMesh();
        }

        public void ClearMesh()
        {
            MeshModel.Clear();
        }

        private void updateMesh()
        {
            List<int> triangles = new List<int>();
            List<Vector3> vertices = new List<Vector3>();
            for (int i = 0; i < MeshPoints.Count; i++)
            {
                if (MeshPoints[i].IsSelected)
                {
                    MeshPointModel point = MeshPoints[i];
                    point.VerticesIndex = vertices.Count;
                    vertices.Add(point.LocalPosition + LocalPositionOffset);
                }

                bool canSet = false;
                switch (Direction)
                {
                    case Direction.X:
                        canSet = i % MeshSize.x != 0;
                        break;
                    case Direction.Y:
                        canSet = i % MeshSize.y != 0;
                        break;
                    case Direction.Z:
                        canSet = i % MeshSize.z != 0;
                        break;
                    default:
                        break;
                }

                if (canSet)
                {
                    int leftIndex = 0;
                    int bottomIndex = 0;
                    int bottomLeftIndex = 0;

                    switch (Direction)
                    {
                        case Direction.X:
                            leftIndex = i - 1;
                            bottomIndex = i - MeshSize.x;
                            bottomLeftIndex = i - MeshSize.x - 1;
                            break;
                        case Direction.Y:
                            leftIndex = i - 1;
                            bottomIndex = i - MeshSize.y;
                            bottomLeftIndex = i - MeshSize.y - 1;
                            break;
                        case Direction.Z:
                            leftIndex = i - 1;
                            bottomIndex = i - MeshSize.z;
                            bottomLeftIndex = i - MeshSize.z - 1;
                            break;
                        default:
                            break;
                    }

                    if (isSetted(i, leftIndex, bottomIndex))
                    {
                        if (IsReverse)
                        {
                            triangles.Add(MeshPoints[bottomIndex].VerticesIndex);
                            triangles.Add(MeshPoints[leftIndex].VerticesIndex);
                            triangles.Add(MeshPoints[i].VerticesIndex);
                        }
                        else
                        {
                            triangles.Add(MeshPoints[i].VerticesIndex);
                            triangles.Add(MeshPoints[leftIndex].VerticesIndex);
                            triangles.Add(MeshPoints[bottomIndex].VerticesIndex);
                        }
                    }
                    else
                    {
                        if (isSetted(i, leftIndex, bottomLeftIndex))
                        {
                            if (IsReverse)
                            {
                                triangles.Add(MeshPoints[bottomLeftIndex].VerticesIndex);
                                triangles.Add(MeshPoints[leftIndex].VerticesIndex);
                                triangles.Add(MeshPoints[i].VerticesIndex);
                            }
                            else
                            {
                                triangles.Add(MeshPoints[i].VerticesIndex);
                                triangles.Add(MeshPoints[leftIndex].VerticesIndex);
                                triangles.Add(MeshPoints[bottomLeftIndex].VerticesIndex);
                            }
                        }
                    }

                    if (isSetted(leftIndex, bottomLeftIndex, bottomIndex))
                    {
                        if (IsReverse)
                        {
                            triangles.Add(MeshPoints[bottomIndex].VerticesIndex);
                            triangles.Add(MeshPoints[bottomLeftIndex].VerticesIndex);
                            triangles.Add(MeshPoints[leftIndex].VerticesIndex);
                        }
                        else
                        {
                            triangles.Add(MeshPoints[leftIndex].VerticesIndex);
                            triangles.Add(MeshPoints[bottomLeftIndex].VerticesIndex);
                            triangles.Add(MeshPoints[bottomIndex].VerticesIndex);
                        }
                    }
                    else
                    {
                        if (isSetted(i, bottomLeftIndex, bottomIndex))
                        {
                            if (IsReverse)
                            {
                                triangles.Add(MeshPoints[bottomIndex].VerticesIndex);
                                triangles.Add(MeshPoints[bottomLeftIndex].VerticesIndex);
                                triangles.Add(MeshPoints[i].VerticesIndex);
                            }
                            else
                            {
                                triangles.Add(MeshPoints[i].VerticesIndex);
                                triangles.Add(MeshPoints[bottomLeftIndex].VerticesIndex);
                                triangles.Add(MeshPoints[bottomIndex].VerticesIndex);
                            }
                        }
                    }
                }
            }

            Vector2[] uvs = new Vector2[vertices.Count];



            switch (UVSDirection)
            {
                case Direction.X:
                    for (int i = 0; i < uvs.Length; i++)
                    {
                        uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
                    }
                    break;
                case Direction.Y:
                    for (int i = 0; i < uvs.Length; i++)
                    {
                        uvs[i] = new Vector2(vertices[i].y, vertices[i].z);
                    }
                    break;
                case Direction.Z:
                    for (int i = 0; i < uvs.Length; i++)
                    {
                        uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
                    }
                    break;
                default:
                    break;
            }



            MeshModel.CreateMesh(vertices.ToArray(), triangles.ToArray(), uvs: uvs);
        }

        private bool isSetted(int a, int b, int c)
        {
            if (a >= 0 && b >= 0 && c >= 0)
            {
                if (MeshPoints[a].IsSelected && MeshPoints[b].IsSelected && MeshPoints[c].IsSelected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (MeshPoints != null)
            {
                for (int i = 0; i < MeshPoints.Count; i++)
                {
                    if (MeshPoints[i].IsSelected)
                    {
                        Gizmos.color = Color.blue;
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                    }

                    Gizmos.DrawSphere(MeshPoints[i].LocalPosition + LocalPositionOffset, GizmoScale);
                }
            }

            if (e_points != null)
            {
                for (int i = 0; i < e_points.Length; i++)
                {
                    if (i + 1 < e_points.Length)
                    {
                        Gizmos.DrawLine(e_points[i], e_points[i + 1]);
                    }
                    else
                    {
                        Gizmos.DrawLine(e_points[i], e_points[0]);
                    }
                }
            }
        }

#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MeshPointController))]
    public class MeshPointControllerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MeshPointController controller = ((MeshPointController)target);
            if (controller.ShowHandles)
            {
                if (GUILayout.Button("Hide Handles"))
                {
                    controller.ShowHandles = false;
                }
            }
            else
            {
                if (GUILayout.Button("Show Handles"))
                {
                    controller.ShowHandles = true;
                }
            }


            if (GUILayout.Button("Create Points"))
            {
                Undo.RecordObject(controller, "Create Points");

                int count = controller.MeshSize.x * controller.MeshSize.y * controller.MeshSize.z;
                int x = 0;
                int y = 0;
                int z = 0;
                Vector3 startPosition = new Vector3(-((controller.Offset.x * (float)controller.MeshSize.x) / 2), -((controller.Offset.y * (float)controller.MeshSize.y) / 2), -((controller.Offset.z * (float)controller.MeshSize.z) / 2)) + new Vector3(controller.Offset.x / 2, controller.Offset.y / 2, controller.Offset.z / 2);
                controller.MeshPoints = new List<MeshPointModel>(); ;

                for (int i = 0; i < count; i++)
                {
                    controller.MeshPoints.Add(new MeshPointModel(controller.transform.position + startPosition + new Vector3(controller.Offset.x * x, controller.Offset.y * y, controller.Offset.z * z) + new Vector3(UnityEngine.Random.Range(controller.RandomizeMin.x, controller.RandomizeMax.x), UnityEngine.Random.Range(controller.RandomizeMin.y, controller.RandomizeMax.y), UnityEngine.Random.Range(controller.RandomizeMin.z, controller.RandomizeMax.z))));

                    x++;
                    if (x == controller.MeshSize.x)
                    {
                        x = 0;
                        y++;
                        if (y == controller.MeshSize.y)
                        {
                            y = 0;
                            z++;
                        }
                    }
                }

            }

            if (GUILayout.Button("Reverse List"))
            {
                Undo.RecordObject(controller, "Set Reverse List");
                controller.MeshPoints.Reverse();
            }
        }

        private void OnSceneGUI()
        {
            MeshPointController controller = (MeshPointController)target;

            if (controller.MeshPoints == null)
                return;

            if (controller.ShowHandles)
            {
                for (int i = 0; i < controller.MeshPoints.Count; i++)
                {
                    if (Tools.current == UnityEditor.Tool.Move)
                    {
                        EditorGUI.BeginChangeCheck();
                        Vector3 newPosition = Handles.PositionHandle(controller.MeshPoints[i].LocalPosition + controller.LocalPositionOffset, controller.transform.rotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(controller, "Change Path Position");
                            controller.MeshPoints[i].LocalPosition = newPosition;
                        }
                    }
                }

                if (Event.current.type == EventType.KeyDown)
                {
                    if (Event.current.keyCode == KeyCode.G)
                    {
                        Undo.RecordObject(controller, "Create Point");
                        MeshPointModel newPoint = new MeshPointModel(Vector3.zero);
                        if (controller.MeshPoints.Count > 0)
                            newPoint.LocalPosition = controller.MeshPoints[controller.MeshPoints.Count - 1].LocalPosition + new Vector3(0, 0.1f, 0);

                        controller.MeshPoints.Add(newPoint);
                    }
                }
            }
        }

    }
#endif
}