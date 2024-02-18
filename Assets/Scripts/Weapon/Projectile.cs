using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float explosionRadius;
    [FormerlySerializedAs("explosionEffect")] [SerializeField] private float explosionForceRb;
    [SerializeField] private float explosionForceCc;
    [SerializeField] private float damage;

    private Vector3 velocity;

    private ProjectileWeapon shooter;
    private Rigidbody proRigidbody;

    private void OnEnable()
    {
        proRigidbody = GetComponent<Rigidbody>();
    }

    public void Shot(ProjectileWeapon shooter, Vector3 pos, Vector3 dir, float damage)
    {
        this.shooter = shooter;
        transform.position = pos;
        transform.LookAt(pos + dir);
        velocity = dir * maxSpeed;
        this.damage = damage;
    }

    private void Update()
    {
        proRigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = Physics.OverlapSphere(transform.position, explosionRadius);
        
        PushObjectsBack(hit);
        ActivateParticle();
        
        Destroy(gameObject);
    }

    private void ActivateParticle()
    {
        var particleObject = Instantiate(explosionParticle.gameObject, transform.position, Quaternion.identity);
        var particle = particleObject.GetComponent<ParticleSystem>();
        var shape = particle.shape;
        shape.radius = explosionRadius;
    }

    private void PushObjectsBack(Collider[] objects)
    {
        foreach (var hit in objects)
        {
            if (hit.TryGetComponent(out IDamageable entity))
            {
                var distance = Vector3.Distance(transform.position, hit.transform.position);
                entity.OnHit(new ((1 - distance / explosionRadius) * damage, distance, shooter.transform, shooter));
            }
            
            var rb = hit.attachedRigidbody;
            if (rb == null)
            {
                var cc = hit.GetComponent<CharacterController>();
                if (cc == null) continue;
                PushCharacterControllers(cc, hit);
            }
            else
            {
                PushRigidbody(rb, hit); 
            }
        }
    }

    private void PushCharacterControllers(CharacterController cc, Collider hit)
    {
        var deltaPos = transform.position - hit.transform.position;
        var dir = -deltaPos.normalized;
            
        var distance = Vector3.Distance(transform.position, hit.transform.position);

        var effect = 1 - distance / explosionRadius;
        var force = explosionForceCc * effect;

        cc.Move(dir * force);
    }

    private void PushRigidbody(Rigidbody rb, Collider hit)
    {
        var deltaPos = transform.position - hit.transform.position;
        var dir = -deltaPos.normalized;
            
        var distance = Vector3.Distance(transform.position, hit.transform.position);

        var effect = 1 - distance / explosionRadius;
        var force = explosionForceRb * effect;
            
        rb.AddForce(dir * force);
    }
}
