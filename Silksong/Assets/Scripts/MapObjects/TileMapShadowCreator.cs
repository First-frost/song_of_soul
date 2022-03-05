using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;
using System.Reflection;

/// <summary>
/// ����Shadow caster 2Dû�취ֱ�����Tilemap��collider2D������
/// �������д�˸��ű����ܽ�tilemap��collider2D������������Ӱ��������������ϡ�
/// </summary>
/// 

public class TileMapShadowCreator : MonoBehaviour
{
    private CompositeCollider2D m_collider;
    private ShadowCaster2D m_shadowCaster;
    public void SetBound()
    {
        m_collider = GetComponent<CompositeCollider2D>();

        //for(int i=0;i<m_collider.pathCount;i++)
        //{
            Vector2[] pathVerts = new Vector2[m_collider.GetPathPointCount(0)];
            m_collider.GetPath(0, pathVerts);
            Vector3[] v = new Vector3[pathVerts.Length];
            for(int j=0;j<pathVerts.Length;j++)
                v[j]=pathVerts[j];
        //GameObject tem = new GameObject("Shadow");
        //tem.transform.parent = this.transform;
        //var render = tem.AddComponent<TilemapRenderer>();
        //render = this.GetComponent<TilemapRenderer>();
        //var tilemap = tem.AddComponent<Tilemap>();
        //tilemap = this.GetComponent<Tilemap>();
        if (this.gameObject.GetComponent<ShadowCaster2D>() != null)
            m_shadowCaster = this.gameObject.GetComponent<ShadowCaster2D>();
        else
            m_shadowCaster =this.gameObject.AddComponent<ShadowCaster2D>();

        m_shadowCaster.GetType().GetField("m_ShapePath", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(m_shadowCaster, v);
       // }





    }
}
[CustomEditor(typeof(TileMapShadowCreator))]
public class TileMapShadowCreator_Editor:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TileMapShadowCreator instance = target as TileMapShadowCreator;
        if(GUILayout.Button("fitting outline"))
        {
            instance.SetBound();
        }
    }
}