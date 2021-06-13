using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class TonnelGenerationSystem : IEcsInitSystem
    {
        private GameObject _tonnel;
        private Mesh _mesh;
        private Vector3[] _vertices;
        private int[] _triangles;

        public void Init()
        {
            _tonnel = new GameObject("Tonnel");
            _mesh = new Mesh();
            _tonnel.AddComponent<MeshFilter>().mesh = _mesh;
            _tonnel.AddComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Tunnel");
            GetVertices();
        }

        private void GetVertices()
        {
            var verices = new Vector3[Tunnel.Points.Count * 4];
            var triangles = new int[Tunnel.Points.Count * 4 * 6];
            var verticesCount = 0;
            var trianglesCount = 0;

            for (int i = 0; i < Tunnel.Points.Count; i++)
            {
                var previous = Tunnel.Points.GetPrevious();
                var next = Tunnel.Points.GetNext();
                var current = Tunnel.Points.GetCurrent();

                var bisector = ((current - previous).normalized + (current - next).normalized).normalized;

                var A = Vector3.Distance(previous, current);
                var B = Vector3.Distance(current, next);
                var C = Vector3.Distance(previous, next);
                
                var P = (A + B + C) / 2;
                
                var height = (2 / C) * Mathf.Sqrt(P * (P - A) * (P - B) * (P - C));

                var heightPoint = Mathf.Sqrt(Mathf.Pow(A, 2) - Mathf.Pow(height, 2)) * (next - previous).normalized + previous;

                var bisectorXZ = new Vector3(bisector.x, 0, bisector.z).normalized;
                var currentHeight = heightPoint + Vector3.up * current.y;
                var bisectorY = ((currentHeight - previous).normalized + (currentHeight - next).normalized).normalized;

                Tunnel.Points.MoveNext();

                var hypotenuseXZ = Tunnel.Diagonal / Mathf.Sin(Vector3.SignedAngle((current - previous).normalized, bisectorXZ, Vector3.up) * (float)Math.PI / 180);
                var hypotenuseY = Tunnel.Diagonal / Mathf.Sin(Vector3.SignedAngle((current - previous).normalized, bisectorY, Vector3.up) * (float)Math.PI / 180);

                var halfHypotenuseXZ = hypotenuseXZ / 2;
                var halfHypotenuseY = hypotenuseY / 2;

                var left = bisectorXZ * halfHypotenuseXZ;
                var right = bisectorXZ * -halfHypotenuseXZ;
                var top = bisectorY * -halfHypotenuseY;
                var buttom = bisectorY * halfHypotenuseY;

                if (top.y < buttom.y)
                {
                    var temp = top;
                    top = buttom;
                    buttom = temp;
                }

                verices[verticesCount++] = left + top + current;
                verices[verticesCount++] = right + top + current;

                verices[verticesCount++] = right + buttom + current;
                verices[verticesCount++] = left + buttom + current;
            }
            
            var trianglesTemplate = new[]
            {
                0, 4, 1,
                4, 5, 1
            };
            
            var trianglesEndTemplate = new[]
            {
                3, 7, 0,
                7, 4, 0
            };
            
            for (int j = 0; j < Tunnel.Points.Count; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        if (i == 3)
                        {
                            triangles[trianglesCount++] = (trianglesEndTemplate[k] + j * 4) % 100;
                            continue;
                        }
                        triangles[trianglesCount++] = (trianglesTemplate[k] + i + j * 4) % 100;
                    }
                }
            }

            _vertices = verices;
            _triangles = triangles;
            UpdateMesh();
        }

        private void UpdateMesh()
        {
            _mesh.Clear();

            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;

            _mesh.RecalculateNormals();
        }
    }
}