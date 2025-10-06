public class TierUpMageTwo : TierUpBase
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