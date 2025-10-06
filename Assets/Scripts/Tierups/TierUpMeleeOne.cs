using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TierUpMeleeOne : TierUpBase
{

    [Header("Melee Tier 1")]
    [SerializeField] private Skill knockback;
    [SerializeField] private Skill linger;


    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            var att = currentMeep.AddComponent<MeepAttack>();
            att.SetSkill(knockback);
        });

        btn2.onClick.AddListener(() =>
        {
            var att = currentMeep.AddComponent<MeepAttack>();
            att.SetSkill(linger);
        });
    }

}
