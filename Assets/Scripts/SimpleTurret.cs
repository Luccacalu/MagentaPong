using System.Collections;
using UnityEngine;

public class SimpleTurret : MonoBehaviour
{
    [SerializeField] private float maxAngle = 150f;
    [SerializeField] private float minAngle = 30f;
    [SerializeField] private float rotationSpeed = 30f;

    [SerializeField] public Transform firePoint;
    [SerializeField] public float bulletWaitTime = 5f;
    [SerializeField] public GameObject bulletPrefab;

    private bool canShoot = true;
    private float currentAngle;

    void Shoot()
    {
        if (canShoot)
        {
            float radians = (currentAngle + 90f) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            Vector2 normalizedDirection = direction.normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = normalizedDirection;
            canShoot = false;
            StartCoroutine(bulletWaitTimer());
        }
    }

    IEnumerator bulletWaitTimer()
    {
        yield return new WaitForSeconds(bulletWaitTime);
        canShoot = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        turnTurret();
        Shoot();
    }

    private void FixedUpdate()
    {
        
    }

    void turnTurret()
    {
        currentAngle = Mathf.PingPong((Time.time * rotationSpeed), (maxAngle - minAngle)) + minAngle;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
