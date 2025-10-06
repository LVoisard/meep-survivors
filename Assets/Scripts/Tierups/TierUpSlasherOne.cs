public class TierUpSlashOne : TierUpBase
{
    void Awake()
    {
        btn1.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<TargetCountEffector>().AddToVal(2);
        });

        btn2.onClick.AddListener(() =>
        {
            currentMeep.GetComponent<SpeedEffector>().AddToVal(100);
        });
    }
}