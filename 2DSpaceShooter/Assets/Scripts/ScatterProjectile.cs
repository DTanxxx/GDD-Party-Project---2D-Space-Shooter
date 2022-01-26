using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterProjectile : Projectile
{
    [SerializeField] private float timeBeforeScattering = 1.0f;
    [SerializeField] private int numberOfScatters = 5;
    [SerializeField] private GameObject scatterProjectile = null;
    [SerializeField] private float scatteredProjectileSpeedReductionFactor = 2.0f;

    public bool scatters = true;

    protected IEnumerator Start()
    {
        if (scatters)
        {
            yield return new WaitForSeconds(timeBeforeScattering);
            Scatter();
            Destroy(gameObject);
        } 
    }

    private void Scatter()
    {       
        for (int i = 0; i < numberOfScatters; ++i)
        {
            var scatterProjectileInstance = Instantiate(scatterProjectile,
                transform.position, transform.rotation);
            scatterProjectileInstance.GetComponent<Projectile>().SetDamage(damage);
            if (scatterProjectileInstance.GetComponent<ScatterProjectile>())
            {
                scatterProjectileInstance.GetComponent<ScatterProjectile>().scatters = false;
            }
            var instanceRigidbody = scatterProjectileInstance.GetComponent<Rigidbody2D>();
            var rigidBody = GetComponent<Rigidbody2D>();
            instanceRigidbody.velocity = rigidBody.velocity / scatteredProjectileSpeedReductionFactor;

            if (i > 0)
            {
                var angle = i * 2 * Mathf.PI / numberOfScatters;
                var xComponent = Mathf.Cos(angle) * rigidBody.velocity.x - Mathf.Sin(angle) * rigidBody.velocity.y;
                var yComponent = Mathf.Sin(angle) * rigidBody.velocity.x + Mathf.Cos(angle) * rigidBody.velocity.y;
                instanceRigidbody.velocity = new Vector2(xComponent / scatteredProjectileSpeedReductionFactor, 
                    yComponent / scatteredProjectileSpeedReductionFactor);
            }
        }
    }
}
