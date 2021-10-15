using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Singleton*/
[System.Serializable]
public class BulletManager
{
    //Step 1. create a private static instance
    private static BulletManager instance = null;

    //Step 2. make our default constructor private
    private BulletManager()
    {
        Initialize();
    }

    //Step 3. make a public static creational method for class access
    public static BulletManager Instance()
    {
        if (instance == null)
        {
            instance = new BulletManager();
        }
        return instance;
    }

    public Queue<GameObject> enemyBulletPool;
    public Queue<GameObject> playerBulletPool;

    public int enemyBulletNumber;
    public int playerBulletNumber;

    // Start is called before the first frame update
    private void Initialize()
    {
        enemyBulletPool = new Queue<GameObject>();//Creates an empty Enemy Bullet Queue
        playerBulletPool = new Queue<GameObject>();//Creates an empty Player Bullet Queue
    }

    private void AddBullet(BulletType type = BulletType.ENEMY)
    {
        var temp_bullet = BulletFactory.Instance().createBullet(type);

        switch(type)
        {
            case BulletType.ENEMY:
                enemyBulletPool.Enqueue(temp_bullet);
                enemyBulletNumber++;
                break;
            case BulletType.PLAYER:
                playerBulletPool.Enqueue(temp_bullet);
                playerBulletNumber++;
                break;
        }
    }

    /// <summary>
    /// This method removes a bullet prefab from the bullet pool
    /// and returns a reference to it.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public GameObject GetBullet(Vector2 spawnPosition, BulletType type = BulletType.ENEMY)
    {
        GameObject temp_bullet = null;

        switch (type)
        {
            case BulletType.ENEMY:
                if (enemyBulletPool.Count < 1)
                {
                    AddBullet();
                }

                temp_bullet = enemyBulletPool.Dequeue();
                temp_bullet.transform.position = spawnPosition;
                temp_bullet.SetActive(true);
                break;

            case BulletType.PLAYER:
                if (playerBulletPool.Count < 1)
                {
                    AddBullet(BulletType.PLAYER);
                }

                temp_bullet = playerBulletPool.Dequeue();
                temp_bullet.transform.position = spawnPosition;
                temp_bullet.SetActive(true);
                break;
        }

        return temp_bullet;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet, BulletType type = BulletType.ENEMY)
    {
        returnedBullet.SetActive(false);

        switch (type)
        {
            case BulletType.ENEMY:
                enemyBulletPool.Enqueue(returnedBullet);
                break;
            case BulletType.PLAYER:
                playerBulletPool.Enqueue(returnedBullet);
                break;
        }
    }
}
