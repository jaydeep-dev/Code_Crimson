using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RaycastWeapon : MonoBehaviour
{
    [System.Serializable]
    public class BulletData
    {
        public float time;
        public float damage;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;

        public void ResetData()
        {
            time = 0f;
            damage = 0f;
            initialPosition = Vector3.zero;
            initialVelocity = Vector3.zero;

            if (tracer)
            {
                tracer.Clear();
                tracer.gameObject.SetActive(false);
            }
        }
    }

    [Header("Weapon Settings")]
    public bool isFiring = false;
    public Transform raycastOrigin;
    [SerializeField] private float fireRate;
    [SerializeField] private float weaponDamage;
    [HideInInspector] public Transform raycastTarget;
    public TrailRenderer tracerEffect;
    private float currentTime;

    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed = 1000f;
    [SerializeField] private float bulletDrop = 0f;
    [SerializeField] private List<BulletData> bulletDataList = new List<BulletData>();

    private Queue<BulletData> bulletsPool = new Queue<BulletData>();

    private Ray ray;
    private RaycastHit hitinfo;
    private const float MaxBulletLifeTime = 5f;

    private void Update()
    {
        if (!isFiring)
            return;

        var rate = 1f / fireRate;
        currentTime += Time.deltaTime;

        if (currentTime >= rate)
        {
            currentTime = 0f;

            Vector3 velocity = (raycastTarget.position - raycastOrigin.position).normalized * bulletSpeed;
            var bullet = GetBullet();
            SetupBullet(bullet, raycastOrigin.position, velocity);
            //bulletsPool.Enqueue(bullet);
            //bulletDataList.Add(bullet);
        }
    }

    Vector3 GetPosition(BulletData bulletData)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return bulletData.initialPosition + (bulletData.initialVelocity * bulletData.time) + (.5f * bulletData.time * bulletData.time * gravity);
    }

    BulletData GetBullet()
    {
        BulletData bullet;

        if (bulletsPool.Count == 0)
        {
            bullet = new BulletData();
            bullet.tracer = Instantiate(tracerEffect);
            bullet.tracer.name = "Bullet " + bulletDataList.Count.ToString();
            bulletDataList.Add(bullet);
        }
        else
        {
            bullet = bulletsPool.Dequeue();
        }
        return bullet;
    }

    private void SetupBullet(BulletData bullet, Vector3 pos, Vector3 velocity)
    {
        bullet.time = 0f;
        bullet.damage = weaponDamage;
        bullet.initialPosition = pos;
        bullet.initialVelocity = velocity;
        bullet.tracer.transform.position = pos;
        bullet.tracer.gameObject.SetActive(true);
    }

    public void UpdateBullet(float deltaTime)
    {
        SimulateBullet(deltaTime);
        RecycleBullets();
    }

    private void SimulateBullet(float deltaTime)
    {
        bulletDataList.ForEach(x =>
        {
            if (x.tracer.gameObject.activeSelf)
            {
                Vector3 p0 = GetPosition(x);
                x.time += deltaTime;
                Vector3 p1 = GetPosition(x);
                RaycastSegment(p0, p1, x);
            }
        });
    }

    private void RaycastSegment(Vector3 start, Vector3 end, BulletData bulletData)
    {
        Vector3 dir = end - start;
        float dist = dir.magnitude;
        ray.origin = start;
        ray.direction = dir;
        if (Physics.Raycast(ray, out hitinfo, dist))
        {
            bulletData.tracer.transform.position = hitinfo.point;
            bulletData.time = MaxBulletLifeTime;
        }
        else
        {
            bulletData.tracer.transform.position = end;
        }
    }

    private void RecycleBullets()
    {
        bulletDataList.ForEach(x =>
        {
            if (x.time > MaxBulletLifeTime)
            {
                x.ResetData();
                bulletsPool.Enqueue(x);
            }
        });
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring= false;
    }

    private void OnDisable()
    {
        foreach (var bullet in bulletDataList)
        {
            if (bullet.tracer)
                Destroy(bullet.tracer.gameObject);
        }

        bulletDataList.Clear();
        bulletsPool.Clear();
    }
}
