using UnityEngine;

public class MeepAttack : MonoBehaviour
{
    [SerializeField] private Skill skill;

    private Skill skillCopy;

    private void Awake()
    {
        SetupAttack(skill);
    }

    public void SetupAttack(Skill skill)
    {
        skillCopy = Instantiate(skill);
        skillCopy.SetOwner(gameObject);
    }

    private void Update()
    {
        if (skillCopy.Ready())
        {
            skillCopy.Perform();
        }
    }
}