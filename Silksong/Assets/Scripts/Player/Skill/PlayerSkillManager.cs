using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerSkillManager : MonoBehaviour
{

    [Tooltip("�������м��ܵ�ScriptableObject")]
    public SkillCollection skillCollection;

    [Header("�����ѽ����ļ���")]
    public List<PlayerSkill> unlockedPlayerSkillList;

    [Header("װ���еļ���")]
    public PlayerSkill equippingPlayerSkill = null;

    private bool _CanCastSkill = true;


    [Header("UI���")]
    [SerializeField] Transform pfUnlockedSkillButton;
    [SerializeField] Transform UnlockedSkillContainer;
    [SerializeField] UnityEngine.UI.Text equippingSkill;


    private void Start()
    {
        _CanCastSkill = true;
    }

    /// <summary>
    /// ���ͷż��ܺ���ã����ܿ�ʼ��ȴ
    /// </summary>
    public void StartSkillCoolDown()
    {
        StartCoroutine(SkillCoolDownTimer(equippingPlayerSkill.CoolDown));
    }
    IEnumerator SkillCoolDownTimer(float cooldown)
    {
        PlayerController playerController = gameObject.GetComponent<PlayerController>();

        _CanCastSkill = false;
        playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, false);

        yield return new WaitForSeconds(cooldown);

        _CanCastSkill = true;
        playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, true);
        yield break;
    }

    /// <summary>
    /// ����Ƿ�����ͷż���
    /// ����Ƿ�װ���м��ܣ������Ƿ�����ȴ������������ͷż���
    /// </summary>
    /// <returns>��������ͷż����򷵻�True�������ͷż����򷵻�False</returns>
    public bool CanCastSkill()
    {
        // TODO: check for mana (or soul)

        if (equippingPlayerSkill.Name == PlayerSkill.SkillName.None)
        {
            Debug.LogWarning("û��װ�����ܣ�");
        }
        return _CanCastSkill;
    }


    /// <summary>
    /// װ��һ���ѽ����ļ���
    /// </summary>
    /// <param name="skill"></param>
    public void EquipSkill(PlayerSkill skill)
    {
        equippingSkill.text = skill.Name.ToString();
        equippingPlayerSkill = skill;
    }



    /// <summary>
    /// ���Ҫ�����ļ��ܵ������Ƿ���SkillCollection��
    /// �����SkillCollection��ͽ���
    /// �ӽ̳�UI�Ǳ�͵��
    /// </summary>
    /// <param name="skillName">Ҫ�����ļ��ܵ�����</param>
    public void UnlockSkill(PlayerSkill.SkillName skillName)
    {

        Dictionary<PlayerSkill.SkillName, PlayerSkill> SkillDictionary = new Dictionary<PlayerSkill.SkillName, PlayerSkill>();
        foreach (PlayerSkill skill in skillCollection.AllSkills)
        {
            SkillDictionary[skill.Name] = skill;
        }
        if (SkillDictionary.ContainsKey(skillName))
        {
            unlockedPlayerSkillList.Add(SkillDictionary[skillName]);
        }

        Transform skillbutton = Instantiate(pfUnlockedSkillButton, UnlockedSkillContainer);
        skillbutton.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = skillName.ToString();
        skillbutton.gameObject.GetComponent<UnlockedSkillButton>().EquipSkill += () => { EquipSkill(SkillDictionary[skillName]); };
    }



    public void testUnlockDesolateDive()
    {
        print("testing unlock skill");
        UnlockSkill(PlayerSkill.SkillName.DesolateDive);
    }
    public void testUnlockDecendingDark()
    {
        print("testing unlock skill");
        UnlockSkill(PlayerSkill.SkillName.DescendingDark);
    }
    public void testEquipSkill()
    {
        print("testing equip skill");
        EquipSkill(unlockedPlayerSkillList[0]);
    }
}
