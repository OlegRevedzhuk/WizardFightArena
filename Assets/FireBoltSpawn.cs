using UnityEngine;

public class FireBoltSpawn : MonoBehaviour
{
    public GameObject fireBoltPreFab;
    public Transform cam;
    private FireBolt bolt;

    public int key = 0;
    private float distanceIfNoTarget = 1000f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(key))
            CastSpell();
    }

    void CastSpell()
    {
        RaycastHit hit;
        Vector3 target;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            target = hit.point;
        else
            target = cam.transform.position + cam.transform.forward * distanceIfNoTarget;

        GameObject spell = Instantiate(fireBoltPreFab, transform.position, cam.transform.rotation);
        Rigidbody rb = spell.GetComponent<Rigidbody>();
        rb.AddForce((target - transform.position).normalized * fireBoltPreFab.GetComponent<FireBolt>().missleSpeed, ForceMode.VelocityChange);
    }
}
