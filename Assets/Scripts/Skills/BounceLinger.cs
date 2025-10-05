using UnityEngine;


[CreateAssetMenu(fileName = "Skills", menuName = "Skill/BounceLinger")]
public class BounceLinger : Skill
{
    [SerializeField] private float damageOverTime;
    [SerializeField] private float duration;
    [SerializeField] private DamageOverTime groundEffectPrefab;

    public override void SetOwner(GameObject go)
    {
        base.SetOwner(go);
        go.GetComponentInChildren<Squish>().onSquishStateChanged.AddListener(CheckForAttackReady);
    }
    public override void Perform()
    {
        ready = false;
        Helper.Wait(0.1f, CreateGroundEffect);
    }

    private void CreateGroundEffect()
    {
        Vector3 pos = owner.transform.position;
        pos.z = 0;
        DamageOverTime dmg = Instantiate(groundEffectPrefab, pos, Quaternion.identity);
        dmg.Setup(damageOverTime, duration);
    }

    private void CheckForAttackReady(Squish.SquishState state)
    {
        if (state == Squish.SquishState.SecondSmush)
        {
            ready = true;
        }
    }
}
