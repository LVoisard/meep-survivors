using UnityEngine;

public class TierUpRangedTwo : TierUpBase
{
    [SerializeField] private Skill pierce;
    [SerializeField] private Skill chain;
    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            var att = currentMeep.GetComponent<MeepAttack>();
            att.SetSkill(pierce);
        });

        btn2.onClick.AddListener(() =>
        {
            var att = currentMeep.GetComponent<MeepAttack>();
            att.SetSkill(chain);
        });

        btn3.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<DamageEffector>().AddToVal(200);
        });
    }
}