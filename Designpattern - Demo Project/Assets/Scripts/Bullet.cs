using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamagable
{

    private Vector3 _dir;
    private float _speed;
    private int _dmg;
    private BulletType _bulletType;
    private Renderer _myRenderer;

    public enum BulletType
    {
        hostile = 0,
        friendly = 1
    }

    /// <summary>
    /// Instantiates a new bullet
    /// </summary>
    /// <param name="pos">Position, where to instantiate the Bullet</param>
    /// <param name="dir">Direction in which the bullet should move</param>
    /// <param name="speed">speed of the Bullet</param>
    /// <param name="dmg">Damage the bullet should deal</param>
    /// <param name="bulletType">Determines if the bullet is friendly or hostile</param>
    public static Bullet InstantiateBullet(Vector3 pos, Vector3 dir, float speed, int dmg, BulletType bulletType)
    {
        GameObject bulletPrefab = Resources.Load<GameObject>("BulletPrefab");
        Bullet bullet = GameObject.Instantiate(bulletPrefab, pos, Quaternion.identity).GetComponent<Bullet>();
        bullet._dir = dir;
        bullet._speed = speed;
        bullet._dmg = dmg;
        bullet._BulletType = bulletType;
        return bullet;
    }

    private void Awake()
    {
        _myRenderer = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        transform.position += _dir * _speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagedObj = other.GetComponent<IDamagable>();
        if (damagedObj != null)
        {
            damagedObj.ReceiveDamage(_dmg);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }



    /// <summary>
    /// Called when hit by another bullet
    /// </summary>
    public void ReceiveDamage(int dmg)
    {
        Destroy(this);
    }


    private BulletType _BulletType
    {
        get { return _bulletType; }
        set
        {
            _bulletType = value;
            if (_bulletType == BulletType.friendly)
            {
                gameObject.layer = LayerMask.NameToLayer("FriendlyBullet");
                GetComponent<Renderer>().material.color = Color.cyan;
            }
            else if (_bulletType == BulletType.hostile)
            {
                gameObject.layer = LayerMask.NameToLayer("HostileBullet");
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
