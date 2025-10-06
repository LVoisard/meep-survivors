using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] GameObject MeepPrefab;

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
                    CompressConsecutiveMeeps(children.GetRange(i - 2, 3), i-2);
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
        Transform train = GameObject.FindGameObjectWithTag("PlayerTrain").transform;
        GameObject newMeep = Instantiate(MeepPrefab, train);

        newMeep.transform.localPosition = Vector3.zero;
        newMeep.transform.localScale = Vector3.one;
        newMeep.transform.localRotation = Quaternion.identity;

        BaseMeep baseMeep = newMeep.GetComponent<BaseMeep>();

        baseMeep.SetLevel(meeps[0].GetLevel() + 1);
        baseMeep.SetType(meeps[0].GetType());

        foreach (BaseMeep meep in meeps)
        {
            Destroy(meep.gameObject);
        }
    }
}
