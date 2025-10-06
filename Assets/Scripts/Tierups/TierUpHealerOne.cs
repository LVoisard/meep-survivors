using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TierUpHealerOne : TierUpBase
{
    [SerializeField] private Skill buff;
    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<TargetCountEffector>().AddToVal(2);
        });

        btn2.onClick.AddListener(() =>
        {
            var att = currentMeep.AddComponent<MeepAttack>();
            att.SetSkill(buff);
        });
    }

}
