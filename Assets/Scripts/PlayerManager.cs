using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerType Type = PlayerType.Hippo;

    public Sprite[] PlayerSprites;

    public enum PlayerType
    { 
        Hippo,
        Cheese,
        Rat
    }

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        switch (Type)
        {
            case PlayerType.Hippo:
                spriteRenderer.sprite = PlayerSprites[0];
                break;
            case PlayerType.Cheese:
                spriteRenderer.sprite = PlayerSprites[1];
                break;
            case PlayerType.Rat:
                spriteRenderer.sprite = PlayerSprites[2];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
