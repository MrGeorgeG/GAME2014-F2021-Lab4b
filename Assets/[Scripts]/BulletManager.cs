using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public Queue<GameObject> bulletPool;
    public int bulletNumber;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>();//Creates an empty Queue

        BuildBulletPool();
    }


    /// <summary>
    /// This method build a bullet pool of bulletNumber bullets
    /// </summary>
    private void BuildBulletPool()
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            var temp_bullet = Instantiate(bulletPrefab);
            temp_bullet.SetActive(false);
            temp_bullet.transform.parent = transform;
            bulletPool.Enqueue(temp_bullet);
        }
    }

    /// <summary>
    /// This method removes a bullet prefab from the bullet pool
    /// and returns a reference to it.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public GameObject GetBullet(Vector2 spawnPosition)
    {
        var temp_bullet = bulletPool.Dequeue();
        temp_bullet.transform.position = spawnPosition;
        temp_bullet.SetActive(true);
        return temp_bullet;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        bulletPool.Enqueue(returnedBullet);
    }
}
