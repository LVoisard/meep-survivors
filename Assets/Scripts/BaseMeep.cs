using System.Collections.Generic;
using UnityEngine;

public class BaseMeep : MonoBehaviour
{
    [SerializeField] Sprite[] SpriteMap;
    [SerializeField] private MeepType Type  = MeepType.Purple;
    [SerializeField] private int Level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
    }

    public enum MeepType
    {
        Purple,
        Yellow,
        Red,
        Blue,
        Green
    }


    public new MeepType GetType() { return Type; }
    public void SetType(MeepType type)
    {
        Type = type;
        GetComponentInChildren<SpriteRenderer>().sprite = SpriteMap[(int)type];
    }

    public int GetLevel() { return Level; }
    public void SetLevel(int lvl)
    {
        Level = lvl;

        //0.5, 1, 1.5 scale (player is size 1)
        transform.localScale = Vector3.zero;
        for (int i = 0; i < lvl; i++)
            transform.localScale += Vector3.one * 0.5f;
    }
}
