using UnityEngine;

public class MeepUpgradePicker : MonoBehaviour
{

    [Header("Sorcerer Tier 1")]
    [SerializeField] private Skill lightning;
    [SerializeField] private Skill explode;
    [SerializeField] private Skill root;


    public void LevelUpMeep(BaseMeep meep)
    {
        var menus = GetComponentsInChildren<TierUpBase>(true);

        if (meep.GetType() == BaseMeep.MeepType.Melee)
        {
            if (meep.GetLevel() == 2)
            {
                menus[0].Ready(meep);
            }
            else
            {
                menus[1].Ready(meep);
            }
        }
        else if (meep.GetType() == BaseMeep.MeepType.Ranged)
        {
            if (meep.GetLevel() == 2)
            {
                menus[2].Ready(meep);
            }
            else
            {
                menus[3].Ready(meep);
            }
        }
        else if (meep.GetType() == BaseMeep.MeepType.Wizard)
        {
            if (meep.GetLevel() == 2)
            {
                menus[4].Ready(meep);
            }
            else
            {
                menus[5].Ready(meep);
            }
        }
        else if (meep.GetType() == BaseMeep.MeepType.Healer)
        {
            if (meep.GetLevel() == 2)
            {
                menus[6].Ready(meep);
            }
            else
            {
                menus[7].Ready(meep);
            }
        }
        else if (meep.GetType() == BaseMeep.MeepType.Slasher)
        {
            if (meep.GetLevel() == 2)
            {
                menus[8].Ready(meep);
            }
            else
            {
                menus[9].Ready(meep);
            }
        }

    }

}
