using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerSkillManager : MonoBehaviour
{

    [Tooltip("The ScriptableObject that holds all the skills")]
    public SkillCollection skillCollection;

    /// <summary>
    /// ���ڼ����Ҫ�����ļ����Ƿ���skillCollection��
    /// </summary>
    Dictionary<PlayerSkill.SkillName, PlayerSkill> SkillDictionary;

    /// <summary>
    /// ����Ѿ����������м���
    /// </summary>
    public List<PlayerSkill> unlockedPlayerSkillList;

    /// <summary>
    /// ���װ���ļ���
    /// </summary>
    public PlayerSkill equippingPlayerSkill = null;

    private bool _CanCastSkill = true;


    [Header("UI")]
    [SerializeField] Transform pfUnlockedSkillButton;
    [SerializeField] Transform UnlockedSkillContainer;
    [SerializeField] Text equippingSkillText;


    private void Start()
    {
        _CanCastSkill = true;

        SkillDictionary = new Dictionary<PlayerSkill.SkillName, PlayerSkill>();
        foreach (PlayerSkill skill in skillCollection.AllSkills)
        {
            SkillDictionary[skill.Name] = skill;
        }

        // if the equipping skill is none, animator parameter SkillReady will never be true
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        if (equippingPlayerSkill.Name == PlayerSkill.SkillName.None)
        {
            playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, false);
        }
    }

    private void Update()
    {
        
        if (equippingPlayerSkill == null)
        {
            equippingSkillText.text = "null";
        }
        else
        {
            equippingSkillText.text = equippingPlayerSkill.Name.ToString();
        }
        
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
    /// ����Ƿ�װ���м��ܣ������Ƿ�����ȴ�����������ͷż���
    /// </summary>
    /// <returns>��������ͷż����򷵻�True�������ͷż����򷵻�False</returns>
    public bool CanCastSkill()
    {
        // TODO: check for mana (or soul)

        Debug.Log(equippingPlayerSkill.Name);

        if (equippingPlayerSkill.Name == PlayerSkill.SkillName.None)
        {
            Debug.LogWarning("You haven't equipped any skill!");
        }
        return _CanCastSkill;
    }


    /// <summary>
    /// װ��һ���ѽ����ļ���
    /// </summary>
    /// <param name="skill"></param>
    public void EquipSkill(PlayerSkill skill)
    {
        //equippingSkillText.text = skill.Name.ToString();
        equippingPlayerSkill = skill;

        // if the equipping skill is not none, set animator parameter SkillReady to true
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        if (skill.Name != PlayerSkill.SkillName.None)
        {
            playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, true);
        }
        else
        {
            playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, false);
        }
        //Debug.Log(equippingPlayerSkill.Name);
    }



    /// <summary>
    /// ����һ����SkillCollection�еļ���
    /// </summary>
    /// <param name="skillName">Ҫ�����ļ��ܵ�����</param>
    public void UnlockSkill(PlayerSkill.SkillName skillName)
    {
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
}
