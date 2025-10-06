using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TierUpHealerTwo : TierUpBase
{
    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<TargetCountEffector>().AddToVal(2);
        });

        btn2.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<DamageEffector>().AddToVal(200);
        });
    }

}