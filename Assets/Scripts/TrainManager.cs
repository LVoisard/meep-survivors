using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TrainManager : MonoBehaviour
{
    [SerializeField] GameObject MeepPrefab;

    public UnityEvent<BaseMeep> onMeepTierUp = new UnityEvent<BaseMeep>();

    public void OnMeepAdded(BaseMeep meep)
    {
        AnalyzeChildren();
    }

    void AnalyzeChildren()
    {
        List<BaseMeep> children = GetComponentsInChildren<BaseMeep>().ToList();

        int count = 1;
        for (int i = 1; i < children.Count; i++)
        {
            if (children[i].GetType() == children[i - 1].GetType() && children[i].GetLevel() == children[i - 1].GetLevel())
            {
                count++;
                if (count == 3)
                {
                    CompressConsecutiveMeeps(children.GetRange(i - 2, 3), i - 2);
                }
            }
            else
            {
                count = 1;
            }
        }
    }

    void CompressConsecutiveMeeps(List<BaseMeep> meeps, int index)
    {
        BaseMeep baseMeep = meeps[0];
        baseMeep.SetLevel(meeps[0].GetLevel() + 1);


        for (int i = 1; i < 3; i++)
        {
            Destroy(meeps[i].gameObject);
        }

        onMeepTierUp?.Invoke(baseMeep);
    }
}
