using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skill/BounceAttack")]
public class LightingStikeSkill : Skill
{
    public override void Perform()
    {
        throw new System.NotImplementedException();
    }

    protected override Vector3[] FindTargets(Vector3 pos)
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None)
            .Select(x => x.transform.position).ToList();

        for (int i = 0; i < this.targetCount; i++)
        {
            Random.Range(0, enemies.Count());

        }
        return null;
    }
}