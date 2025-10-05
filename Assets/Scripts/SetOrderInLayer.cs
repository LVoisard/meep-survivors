using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SetOrderInLayer : MonoBehaviour
{
    SpriteRenderer Sprite;
    Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Player.transform.position.y < Sprite.bounds.center.y)
            Sprite.sortingOrder = 0;
        else
            Sprite.sortingOrder = 2;
    }
}
