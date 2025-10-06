using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerType Type = PlayerType.Hippo;

    public Sprite[] PlayerSprites;

    public float AreaOfEffectEffector = 0;
    public float CooldownEffector = 0;
    public float DamageEffector = 0;
    public float DurationEffector = 0;
    public float SpeedEffector = 0;
    public int TargetCountEffector = 0;

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
