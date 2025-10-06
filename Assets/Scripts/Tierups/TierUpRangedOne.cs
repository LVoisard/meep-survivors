public class TierUpRangedOne : TierUpBase
{
    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<DamageEffector>().AddToVal(500);
            currentMeep.GetComponent<CooldownEffector>().AddToVal(-10);
        });

        btn2.onClick.AddListener(() =>
       {
           currentMeep.GetComponent<DamageEffector>().AddToVal(50);
           currentMeep.GetComponent<CooldownEffector>().AddToVal(200);
       });
    }
}