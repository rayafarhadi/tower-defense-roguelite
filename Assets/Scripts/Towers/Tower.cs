using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;
    private EnemyStats targetStats;

    [Header("General Stats")]
    public float range = 15f;

    [Header("Bullet Properties (default)")]
    public float fireRate = 1f;
    private float fireInterval = 0f;
    public GameObject bulletPrefab;

    [Header("Laser Properties")]
    public bool useLaser = false;
    public int dot = 30;
    public float slowPercent = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    public Light laserLight;
    public ParticleSystem firePointGlow;
    public Light firePointLight;

    [Header("Attributes")]
    public string enemyTag = "Enemy";
    public Transform pivotPoint;
    public float rotationSpeed = 10f;
    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy.transform;
            targetStats = closestEnemy.GetComponent<EnemyStats>();
        }
        else
        {
            target = null;
            targetStats = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    laserLight.enabled = false;
                    firePointGlow.Stop();
                    firePointLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireInterval <= 0f)
            {
                Shoot();
                fireInterval = 1f / fireRate;
            }

            fireInterval -= Time.deltaTime;
        }


    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void Laser()
    {
        targetStats.TakeDamage(dot * Time.deltaTime);
        targetStats.Slow(slowPercent);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
            laserLight.enabled = true;
            firePointGlow.Play();
            firePointLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        laserEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserEffect.transform.position = target.position + dir.normalized;

        firePointGlow.transform.position = firePoint.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
