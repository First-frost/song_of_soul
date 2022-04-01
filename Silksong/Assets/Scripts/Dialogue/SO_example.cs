using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//���ü��д��
//[CreateAssetMenu(fileName = "SO_example", menuName = "Test/Creat_SO_example")]
public class SO_example : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] int start_index;
    [SerializeField] int final_index;
    [SerializeField] List<string> content = new List<string>();

    //��������SOд������ȡ��Ϣ�ļ��󣬰�����������ָ��λ�ò�����
    [MenuItem("SO_objects/Create/CreateSO_example", false, 2)]
    public static void CreateSO_example()
    {
        SO_example asset = ScriptableObject.CreateInstance<SO_example>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/SO_objects/testSO.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
