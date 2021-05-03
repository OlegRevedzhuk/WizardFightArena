using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    [SerializeField] private float missleSpeed = 15f;
    [SerializeField] private GameObject misslePreFab;
    private PlayerAttributes caster;
    private GameObject target;

    private Rigidbody rb;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FindTarget());
    }

    private void OnCollisionEnter()
    {
        Destroy(this.gameObject);
    }
   
    private void FixedUpdate()
    {
        if (target != null)
        {
            rb.AddForce((target.transform.position - rb.position).normalized * 20f, ForceMode.Acceleration);

            dir = rb.velocity.normalized;
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
    }
    /*
    void DamageAndDestroy()
    {
        spellInstance = Instantiate(explosionPreFab, transform.position, transform.rotation);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        col.enabled = false;
        countdown = 0f;
        hasExploded = true;
        if (trail != null)
            trail.Stop();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach ( Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                // add damage and other things to desired nearby objects
            }
        }
       
    }
 */
    private IEnumerator FindTarget()
    {
        yield return 5;

        float distance = 1000000f;

        foreach (PlayerAttributes player in PlayerList.playerList)
        {
            if(player != caster)
            {
                float dist = Vector3.Distance(player.gameObject.transform.position, transform.position);

                if(dist < distance)
                {
                    target = player.gameObject;
                    distance = dist;
                }
            }
        }
    }

    public float GetSpeed()
    {
        return missleSpeed;
    }

    public void SetCaster(PlayerAttributes cast)
    {
        caster = cast;
    }
}
