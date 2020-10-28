using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!


public class BulletPoolManager : MonoBehaviour
{

    //private static BulletPoolManager m_instance = null;

    //private BulletPoolManager()
    //{
    //    Start();
  
    //}

    //public static BulletPoolManager Instance()
    //{
    //    if (m_instance == null)
    //    {
    //        m_instance = new BulletPoolManager();
    //    }

    //    return m_instance;
    //}

    public GameObject bullet;
    public int MaxBullets = 10;

    //TODO: create a structure to contain a collection of bullets
    private Queue<GameObject> m_bulletpool;
    
        // Start is called before the first frame update
    void Start()
    {
        //bullet = Resources.Load("Bullet") as GameObject;
        // TODO: add a series of bullets to the Bullet Pool
        m_bulletpool = new Queue<GameObject>();
        buildBulletPool();
       
    }

    

    private void buildBulletPool()
    {
        for (var count = 0; count < MaxBullets; count++)
        {
            var tempbullet = Instantiate(bullet);
            tempbullet.SetActive(false);
            m_bulletpool.Enqueue(tempbullet);
        }
    }

    //TODO: modify this function to return a bullet from the Pool
    public void GetBullet(Vector3 position)
    {
        if (!isBulletPoolEmpty())
        {
        var tempbullet = m_bulletpool.Dequeue();
        tempbullet.transform.position = position;
        tempbullet.SetActive(true);

        }
        else
        {
            var tempbullet = Instantiate(bullet, position, Quaternion.identity);

            MaxBullets = +1;
        }
    

       // return tempbullet;
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        m_bulletpool.Enqueue(bullet);
    }

    public int ReturnSize()
    {
        return m_bulletpool.Count;
    }

    public bool isBulletPoolEmpty()
    {
        if (m_bulletpool.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
