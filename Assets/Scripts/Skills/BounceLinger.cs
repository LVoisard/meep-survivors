using UnityEngine;


[CreateAssetMenu(fileName = "Skills", menuName = "Skill/BounceLinger")]
public class BounceLinger : Skill
{
    [SerializeField] private float damageOverTime;
    [SerializeField] private float duration;
    [SerializeField] private DamageOverTime groundEffectPrefab;

    string targetTag = "Enemy";

    public override void SetOwner(GameObject go)
    {
        base.SetOwner(go);
        go.GetComponentInChildren<Squish>().onSquishStateChanged.AddListener(CheckForAttackReady);
        if (owner.tag == "Player") targetTag = "Enemy";
        if (owner.tag == "Enemy") targetTag = "Player";
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
        dmg.Setup(targetTag, damageOverTime, duration, effectors);
    }

    private void CheckForAttackReady(Squish.SquishState state)
    {
        if (state == Squish.SquishState.SecondSmush)
        {
            ready = true;
        }
    }
}
