using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireNumber { One = 1, Two, Three, Four, Five}

public class FireBullet : MonoBehaviour {

    public FireNumber eFireNumber = FireNumber.One;
    [SerializeField]
    public Transform BulletGenerationPoint;

    [SerializeField]
    int BulletsNumber = 0;

    [SerializeField]
    float bulletSpeed = 10;

    [SerializeField]
    Bullet _bulletObject;

    Queue<Bullet> _bullets;

    private void Start()
    {
        _bullets = new Queue<Bullet>();
        GenerateBullets();
    }

    void GenerateBullets()
    {
        for(int i=0;i<BulletsNumber;i++)
        {
            Bullet _thisBullet = null;
            _thisBullet = Instantiate(_bulletObject);
            _thisBullet.StartPos = BulletGenerationPoint.position;
            _thisBullet.fireSystem = this;
            _bullets.Enqueue(_thisBullet);
            _thisBullet.thisBulletTransform.SetParent(transform);
        }
    }
    public void FireBullets(Vector3 pos)
    {
        if (_bullets.Count == 0)
            return;
        Bullet bul = _bullets.Dequeue();
        bul.thisBulletTransform.position = BulletGenerationPoint.position;
        bul.Fire(pos- BulletGenerationPoint.position, bulletSpeed);
    }
	

    internal void FillBulletInQueue(Bullet blt)
    {
        _bullets.Enqueue(blt);
    }
}
