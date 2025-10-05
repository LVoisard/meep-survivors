using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private BasicBullet shotPrefab;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    private bool ready = true;
    public void Update()
    {
        if (ready)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        ready = false;
        BasicBullet bullet = Instantiate(shotPrefab);
        bullet.transform.position = transform.position + transform.forward;
        bullet.transform.forward = GetComponent<Rigidbody2D>().linearVelocity.normalized;
        bullet.SetStats("Player", damage, new Skill.SkillEffectors());
        Helper.Wait(cooldown, () => ready = true);
    }

}
