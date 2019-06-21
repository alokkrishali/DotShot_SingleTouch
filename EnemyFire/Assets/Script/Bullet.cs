using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    Rigidbody2D rd2DBullet;
    public Vector2 StartPos;
    public Transform thisBulletTransform;
    public FireBullet fireSystem;
    Coroutine BulletFire;
    public GameObject hitEffect;


    void Awake () {
        rd2DBullet = GetComponent<Rigidbody2D>();
        thisBulletTransform = GetComponent<Transform>();
        fireSystem = GetComponentInParent<FireBullet>();
    }

    void MakeBulletVisible()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            fireSystem.FillBulletInQueue(this);
            BulletHitEffect();
            DeactiveBullet();
        }
    }
    
    public void BulletHitEffect()
    {
        GameObject hitFire = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitFire, 2);
    }

    public void Fire(Vector2 direction, float Speed)
    {
        StartPos.x = -15;
        rd2DBullet.velocity = direction.normalized * Speed;
        if (BulletFire != null)
            StopCoroutine(BulletFire);
        BulletFire = StartCoroutine("ResetBullet");
    }

    IEnumerator ResetBullet()
    {
        yield return new WaitForSeconds(1);
        DeactiveBullet();
    }

    void DeactiveBullet()
    {
        rd2DBullet.velocity = Vector2.zero;
        thisBulletTransform.position = StartPos;
        fireSystem.FillBulletInQueue(this);
    }
}
