using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TierUpMeleeTwo : TierUpBase
{


    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            var aoe = currentMeep.GetComponent<AreaOfEffectEffector>();
            aoe.AddToVal(50);
        });

        btn2.onClick.AddListener(() =>
        {
            var ct = currentMeep.GetComponent<TargetCountEffector>();
            ct.AddToVal(2);
        });
    }

}