using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/KnockbackBounce")]
public class KnockbackBounce : Skill
{
    [SerializeField] private float radius;

    public override void SetOwner(GameObject go)
    {
        base.SetOwner(go);
        go.GetComponentInChildren<Squish>().onSquishStateChanged.AddListener(CheckForAttackReady);
    }

    public override void Perform()
    {
        ready = false;
        Helper.Wait(0.1f, KnockbackInRadius);
    }

    private void KnockbackInRadius()
    {
        var pos = owner.transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), radius * (100f + effectors.AreaOfEffect) / 100f);

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy")
            {
                Vector3 force = hit.transform.position - pos;
                hit.attachedRigidbody.AddForce(force.normalized * 10, ForceMode2D.Impulse);
            }
        }
    }


    private void CheckForAttackReady(Squish.SquishState state)
    {
        if (state == Squish.SquishState.SecondSmush)
        {
            ready = true;
        }
    }
}
