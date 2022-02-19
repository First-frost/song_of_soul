using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ToBindAssets
{
	public static SO_example_container temp_container;
	[MenuItem("SO_objects/Operation/ToBindContainer", false, 1)]
	public static void ToBindTheContainer()
	{
		string SoContainerPath = "Assets/Resources/SO_Container/testSOContainer.asset";
		//Debug.Log(Application.dataPath);
        if (File.Exists(SoContainerPath))
        {
			Debug.Log(File.Exists(SoContainerPath));
			SO_example_container temp = Resources.Load<SO_example_container>("SO_Container/" + "testSOContainer");
			Debug.Log(temp.name);
			if (temp != null)
			{
				temp_container = SO_example_container.CreateInstance<SO_example_container>();
				temp.SO_list = new List<SO_example>(0);
				temp_container.SO_list = temp.SO_list ;
				Debug.Log("�󶨶�Ӧ·����Container");
			}
			else if(temp == null) {
				Debug.Log("�󶨶�Ӧ·����Containerʧ��");
			}

        }
        else{
			Debug.Log("·����Container������");
		}
	}
	
	[MenuItem("SO_objects/Operation/ToBindAssetForContainer", false, 2)]
    public static void  ToFIndAndBindAssetForContainer() {
		//��ȡĿ¼·��
		string path = "Assets/Resources/SO_objects/";
		//Ŀ¼������ʼ��ȡ���ж�Ӧ�����ļ�
		if (Directory.Exists(path))
		{
			Debug.Log("Ŀ¼��ȷ����ʼ����");
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			FileInfo[] fileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			for (int i = 0; i < fileInfos.Length; i++)
			{
				if (fileInfos[i].Name.EndsWith(".asset"))
				{
					if (fileInfos[i].Name.Contains("SO_"))//����������������������ҳ���
					{
						Debug.Log("�ҵ���ӦSO����:" + fileInfos[i].Name);
						if (temp_container != null)
						{
							SO_example temp = SO_example.CreateInstance<SO_example>();
							string FileName = fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 6);
							Debug.Log("����SO����:"+fileInfos[i].Name.Substring(0, fileInfos[i].Name.Length - 6));
							temp =Resources.Load<SO_example>("SO_objects/" +FileName);
							if (temp != null)
							{
								//temp_container.SO_list = new List<SO_example>(0);
								temp_container.SO_list.Add(temp);
							}
							else {
								Debug.Log("So_example�ļ���ȡ��������");
							}
						}
					}
				}
				
			}

		}
		else {
			Debug.Log("��ǰ�涨Ŀ¼������");
		}
		
    }

}
    

