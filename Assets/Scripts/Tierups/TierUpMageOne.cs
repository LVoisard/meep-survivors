using UnityEngine;

public class TierUpMageOne : TierUpBase
{
    [Header("Sorcerer Tier 1")]
    [SerializeField] private Skill lightning;
    [SerializeField] private Skill explode;
    [SerializeField] private Skill root;

    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            var att = currentMeep.GetComponent<MeepAttack>();

            att.SetSkill(lightning);

        });

        btn2.onClick.AddListener(() =>
        {
            var att = currentMeep.GetComponent<MeepAttack>();
            att.SetSkill(explode);
        });

        btn3.onClick.AddListener(() =>
        {
            var att = currentMeep.GetComponent<MeepAttack>();

            att.SetSkill(root);
        });
    }
}