using UnityEngine;

public class SquidBoss : Enemy
{
    private bool regularProjReady = true;
    private bool circleProjReady = true;
    private GameObject player;

    private float followRadius = 3f;

    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private float circleAttackCooldown = 1;
    [SerializeField] private BasicBullet squidProj;
    [SerializeField] private SharkFinProjectile circleProj;
    Skill.SkillEffectors e = new Skill.SkillEffectors();
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        followRadius -= Time.deltaTime;
        followRadius = Mathf.Max(1, followRadius);
        e.AreaOfEffect -= 10 * Time.deltaTime;
        e.AreaOfEffect = Mathf.Max(-100, e.AreaOfEffect);
        // if (regularProjReady)
        // {
        //     FireRegularProj();
        // }

        if (circleProjReady)
        {
            FireSharkProj();
        }
    }

    void FireSharkProj()
    {
        circleProjReady = false;
        Vector3 dir = Vector3.up;
        SharkFinProjectile projInst = Instantiate(circleProj);
        projInst.transform.position = transform.position + dir * 2f;
        projInst.transform.forward = dir;
        projInst.SetStats("Player", 10, e);
        projInst.SetTranformCenter(transform);

        Helper.Wait(circleAttackCooldown, () => circleProjReady = true);
    }

    void FireRegularProj()
    {
        regularProjReady = false;
        BasicBullet bul = Instantiate(squidProj);
        bul.SetStats("Player", 5, new Skill.SkillEffectors());
        bul.transform.position = transform.position;
        bul.transform.forward = (player.transform.position - transform.position).normalized;

        Helper.Wait(attackCooldown, () => regularProjReady = true);
    }

    void FixedUpdate()
    {
        var newX = Mathf.Cos(Time.time);
        var newY = Mathf.Sin(Time.time);

        var dir = player.transform.position - transform.position;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.Lerp(GetComponent<Rigidbody2D>().linearVelocity, dir + new Vector3(newX, newY, 0) * followRadius, Time.fixedDeltaTime * 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            followRadius = 10f;
            e.AreaOfEffect = 300;
        }
    }

    enum SquidState
    {
        Circle,
        Attack
    }
}
