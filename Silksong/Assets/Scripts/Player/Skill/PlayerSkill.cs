using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSkill
{
    public enum SkillName
    {
        None,
        DesolateDive,
        DescendingDark,
    }
    public SkillName Name;

    public string Description;

    public int Damage;

    public int ManaCost; 

    public float CoolDown;

    /// <summary>
    /// ��������
    /// </summary>
    public void Cast()
    {
        switch (Name)
        {
            default: break;

            case SkillName.DesolateDive:
                DesolateDive();
                break;
            case SkillName.DescendingDark:
                DescendingDark();
                break;
        }
    }



    #region ����
    // ����ļ���Ч����ʵ�֣���Cast()�����

    private void DesolateDive()
    {
        //PlayerController.Instance...
        Debug.Log("Casting Desolate Dive");
    }
    private void DescendingDark()
    {

        Debug.Log("Casting Descending Dark");
    }



    #endregion
}
