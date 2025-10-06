using UnityEngine;

public class TargetCountEffector : MonoBehaviour
{
    public int value = 0;
    public void AddToVal(int value)
    {
        this.value += value;
        var attacks = GetComponents<MeepAttack>();
        foreach (var att in attacks)
        {
            att.UpdateSkillEffectors();
        }
    }
}