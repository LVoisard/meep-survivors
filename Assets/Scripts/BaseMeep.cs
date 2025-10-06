using System.Collections.Generic;
using UnityEngine;

public class BaseMeep : MonoBehaviour
{
    [SerializeField] private MeepType Type  = MeepType.Healer;
    [SerializeField] private int Level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
    }

    public enum MeepType
    {
        Healer,
        Slasher,
        Wizard,
        Ranged,
        Melee
    }

    void UpdateVisuals()
    {
        if (Level == 1)
            GetComponentInChildren<SpriteRenderer>().sprite = DataManager.Instance.MeepSpriteMap[(int)Type];
        else if (Level == 2)
            GetComponentInChildren<SpriteRenderer>().sprite = DataManager.Instance.UpgradedMeepSpriteMap[(int)Type];
        else
            GetComponentInChildren<SpriteRenderer>().sprite = DataManager.Instance.MegaUpgradedMeepSpriteMap[(int)Type];
    }

    public new MeepType GetType() { return Type; }
    public void SetType(MeepType type)
    {
        Type = type;

        UpdateVisuals();

        MeepAttack attack = GetComponent<MeepAttack>();

        if (Level == 1)
        {
            switch (GetType())
            {
                case MeepType.Healer:
                    attack.SetSkill(DataManager.Instance.Skills[0]);
                    break;
                case MeepType.Slasher:
                    attack.SetSkill(DataManager.Instance.Skills[1]);
                    break;
                case MeepType.Wizard:
                    attack.SetSkill(DataManager.Instance.Skills[2]);
                    break;
                case MeepType.Ranged:
                    attack.SetSkill(DataManager.Instance.Skills[3]);
                    break;
                case MeepType.Melee:
                    attack.SetSkill(DataManager.Instance.Skills[4]);
                    break;
            }

            attack.enabled = true;
        }
    }

    public int GetLevel() { return Level; }
    public void SetLevel(int lvl)
    {
        Level = lvl;

        UpdateVisuals();

        //0.5, 1, 1.5 scale (player is size 1)
        transform.localScale = Vector3.zero;
        for (int i = 0; i < lvl; i++)
            transform.localScale += Vector3.one * 0.5f;
    }
}
