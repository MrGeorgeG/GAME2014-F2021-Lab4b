using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public BulletType type;

    [Header("Bullet Movement")]
    public Vector3 bulletVelocity;
    public Bounds bulletBounds;

    //private BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        //bulletManager = GameObject.FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += bulletVelocity;
    }

    private void CheckBounds()
    {
        //check bottom bounds
        if(transform.position.y < bulletBounds.max)
        {
            //Destroy(this.gameObject);

            BulletManager.Instance().ReturnBullet(this.gameObject, type);
        }

        //check top bounds
        if (transform.position.y > bulletBounds.min)
        {
            BulletManager.Instance().ReturnBullet(this.gameObject, type);
        }
    }
}