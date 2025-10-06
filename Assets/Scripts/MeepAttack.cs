using UnityEngine;

[RequireComponent(typeof(AreaOfEffectEffector))]
[RequireComponent(typeof(CooldownEffector))]
[RequireComponent(typeof(DamageEffector))]
[RequireComponent(typeof(DurationEffector))]
[RequireComponent(typeof(SpeedEffector))]
[RequireComponent(typeof(TargetCountEffector))]
public class MeepAttack : MonoBehaviour
{
    [SerializeField] private Skill skill;

    [SerializeField] bool SetupAttackOnAwake = false;

    private AreaOfEffectEffector aoe;
    private CooldownEffector cdr;
    private DamageEffector dmg;
    private DurationEffector dur;
    private SpeedEffector speed;
    private TargetCountEffector target;

    private Skill skillCopy;

    private void Awake()
    {
        aoe = GetComponent<AreaOfEffectEffector>();
        cdr = GetComponent<CooldownEffector>();
        dmg = GetComponent<DamageEffector>();
        dur = GetComponent<DurationEffector>();
        speed = GetComponent<SpeedEffector>();
        target = GetComponent<TargetCountEffector>();;

        if (SetupAttackOnAwake)
            SetupAttack(skill);
    }

    public void SetupAttack(Skill skill)
    {
        skillCopy = Instantiate(skill);
        skillCopy.SetOwner(gameObject);
        UpdateSkillEffectors();
    }

    public void UpdateSkillEffectors()
    {
        Skill.SkillEffectors eff;
        eff.AreaOfEffect = aoe.value;
        eff.CooldownReduction = cdr.value;
        eff.Damage = dmg.value;
        eff.Duration = dur.value;
        eff.Speed = speed.value;
        eff.AdditionalTargets = target.value;

        skillCopy.ApplyEffectors(eff);
    }

    private void Update()
    {
        if (skillCopy.Ready())
        {
            skillCopy.Perform();
        }
    }

    public void SetSkill(Skill s)
    {
        skill = s;
        SetupAttack(skill);
    }
}