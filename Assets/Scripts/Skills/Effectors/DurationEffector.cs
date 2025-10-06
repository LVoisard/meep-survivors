using UnityEngine;

public class DurationEffector : MonoBehaviour
{
    public float value = 0;
    public void AddToVal(float value)
    {
        this.value += value;
        var attacks = GetComponents<MeepAttack>();
        foreach (var att in attacks)
        {
            att.UpdateSkillEffectors();
        }
    }
}