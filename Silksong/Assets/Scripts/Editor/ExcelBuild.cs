using System.Collections;
using System.Collections.Generic;
using Excel;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ExcelBuild : Editor
{
        [MenuItem("CustomEditor/CreateItemAsset")]
         public static void CreateWeaponUpdate()
        {
            WeaponUpdate manager = ScriptableObject.CreateInstance<WeaponUpdate>();
            //��ֵ
            manager.weaponUpgradeInfoList = ExcelReader.CreateWeaponUpgradeInfoWithExcel(ExcelConfig.excelsFolderPath + "weapon.xlsx");

            //ȷ���ļ��д���
            if (!Directory.Exists(ExcelConfig.assetPath))
            {
                Directory.CreateDirectory(ExcelConfig.assetPath);
            }

            //asset�ļ���·�� Ҫ��"Assets/..."��ʼ������CreateAsset�ᱨ��
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "Item");
            //����һ��Asset�ļ�
            AssetDatabase.CreateAsset(manager, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
       }

}
