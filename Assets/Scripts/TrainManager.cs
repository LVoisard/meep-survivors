using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TrainManager : MonoBehaviour
{
    [SerializeField] GameObject MeepPrefab;
    [SerializeField] float cameraGoUp = 0.1f;
    [SerializeField] float maxCameraGoUp = 3;
    [SerializeField] AudioSource LevelupSound;
    public UnityEvent<BaseMeep> onMeepTierUp = new UnityEvent<BaseMeep>();

    private int childCount = 0;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = Camera.main.transform.localPosition;
    }
    public void OnMeepAdded(BaseMeep meep)
    {
        AnalyzeChildren();

    }

    void LateUpdate()
    {
        float distance = Mathf.Min(cameraGoUp * childCount, maxCameraGoUp);

        // Target position is base position minus forward * distance
        Vector3 desiredPos = targetPos - Camera.main.transform.forward * distance;

        // Smoothly interpolate toward the desired position
        Camera.main.transform.localPosition = Vector3.Lerp(
            Camera.main.transform.localPosition,
            desiredPos,
            Time.deltaTime
        );
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

        childCount = GetComponentsInChildren<BaseMeep>().Length;

    }

    void CompressConsecutiveMeeps(List<BaseMeep> meeps, int index)
    {
        BaseMeep baseMeep = meeps[0];
        baseMeep.SetLevel(meeps[0].GetLevel() + 1);
        LevelupSound.Play();


        for (int i = 1; i < 3; i++)
        {
            Destroy(meeps[i].gameObject);
        }

        onMeepTierUp?.Invoke(baseMeep);
    }
}
