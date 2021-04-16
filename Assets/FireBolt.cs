using UnityEngine;

public class FireBolt : MonoBehaviour
{
    [SerializeField]
    private float spellMaxDuration = 2f;
    //add damage health
    [SerializeField]
    private float explosionRadius = 4f;
    [SerializeField]
    private float fizzleDuration = 2.25f;
    [SerializeField]
    private float missleSpeed = 55f;

    [SerializeField]
    private GameObject explosionPreFab;
    [SerializeField]
    private ParticleSystem trail;
    private GameObject spellInstance;

    private Collider col;
    private Rigidbody rb;

    private float countdown;
    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = spellMaxDuration;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown < 0f && hasExploded == false)
        {
            Explode();
        }

        if (countdown < -fizzleDuration)
        {
            Destroy(spellInstance);
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        spellInstance = Instantiate(explosionPreFab, transform.position, transform.rotation);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        col.enabled = false;
        countdown = 0f;
        hasExploded = true;
        if(trail != null)
            trail.Stop();
        /*
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach ( Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                // add damage and other things to desired nearby objects
            }
        }
        */
    }

    public float GetSpeed()
    {
        return missleSpeed;
    }
}
