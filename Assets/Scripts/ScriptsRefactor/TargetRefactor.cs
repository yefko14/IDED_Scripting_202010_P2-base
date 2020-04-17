using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRefactor : MonoBehaviour, IPool
{
    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    [SerializeField] float lifeTime;
    PlayerRefactor player;
    int damage = 1;
    Rigidbody rb;
    Vector3 Initial;

    public void Instantiate()
    {
        player = FindObjectOfType<PlayerRefactor>();
        currentHP = maxHP;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Initial = transform.position;
    }
    public void Spawn(Vector3 position)
    {
        transform.position = position;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        Invoke("Destroy", lifeTime);
    }
    public void Destroy()
    {
        transform.position = Initial;
        rb.isKinematic = true;
    }
    void Hit()
    {
        if(currentHP > 0)
        {
            currentHP--;
            if (currentHP <= 0)
            {
                player.PlusPoints(scoreAdd);
                Destroy();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;

        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            collision.gameObject.GetComponent<Bullet>().Destroy();
            Hit();
        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {
            Destroy();
            player.ApplyDamage(damage);
        }
    }
}
