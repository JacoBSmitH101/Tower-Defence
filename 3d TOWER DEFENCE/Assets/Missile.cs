using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 prevPosition;
     private Transform target;
    private GameObject targetGO;
    
    [Header("Bullet Options:")]
    [SerializeField] float speed = 10f;
    [SerializeField] float standardBulletDamage = 1f;
    [SerializeField] GameObject impactParticles;
    
    
    public void Seek(Transform _target, float damage)
    {
        target = _target;
        standardBulletDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Vector3 deltaPosition = transform.position - prevPosition;

        if (deltaPosition != Vector3.zero) {
            // Same effect as rotating with quaternions, but simpler to read
            transform.forward = deltaPosition;
        }
        // Recording current position as previous position for next frame
        prevPosition = transform.position;
        transform.rotation = Quaternion.LookRotation(transform.position - prevPosition);
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        float giveDamage = standardBulletDamage;
        int randomNumber = Random.Range(1, 35);
        if (randomNumber == 1) 
        {
            giveDamage = 100f;
        }

        
        Enemy tempEnemy = target.GetComponent<Enemy>();
        if (tempEnemy == null) {
            FastEnemy newtempEnemy = target.GetComponent<FastEnemy>();
            if (newtempEnemy == null) {
                StrongEnemy newStrong = target.GetComponent<StrongEnemy>();
                newStrong.takeDamage(giveDamage);
                Destroy(gameObject);
                return;
            }
            newtempEnemy.takeDamage(giveDamage);
            Destroy(gameObject);
            return;
        }
        tempEnemy.takeDamage(giveDamage);
        Destroy(gameObject);
        return;
    }
    private void OnDestroy() {
        FindObjectOfType<SoundManager>().Play("missileImpact");
        
        Instantiate(impactParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        
    }
}
