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
        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!Sprite)
            Sprite = GetComponent<SpriteRenderer>();

        if (Player.transform.position.y < Sprite.bounds.center.y)
        {
            Sprite.sortingOrder = 0;
            Color color = Sprite.color;
            color.a = 1f;
            Sprite.color = color;
        }
        else
        {
            Sprite.sortingOrder = 2;
            Color color = Sprite.color;
            color.a = 0.5f;
            Sprite.color = color;
        }
    }
}
