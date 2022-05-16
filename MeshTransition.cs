using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTransition : MonoBehaviour
{
    public MeshFilter meshFilter;
    Mesh mesh;
    public Vector3[] init_Vertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        init_Vertices = mesh.vertices;

        Managers.Input.MouseAction -= Input_MouseClicked;
        Managers.Input.MouseAction += Input_MouseClicked;


    }

    void Update()
    {
        
    }

    private void Input_MouseClicked(Define.MouseEvent evt)
    {
        Vector3[] vertices = (Vector3[])init_Vertices.Clone();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        int index = 0;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            Vector3 pos = hit.point;
            float dist = float.MaxValue;
            for (int i = 0; i < vertices.Length; i++)
            {
                if(dist > (pos - transform.TransformPoint(vertices[i])).magnitude)
                {
                    dist = (pos - transform.TransformPoint(vertices[i])).magnitude;
                    index = i;
                }
            }
        }

        vertices[index].y += 1f;
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
