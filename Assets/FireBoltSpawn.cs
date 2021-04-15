using UnityEngine;

public class FireBoltSpawn : MonoBehaviour
{
    public enum SpellLevel
    {
        minor,
        normal,
        major
    }

    [SerializeField]
    private GameObject[] fireBoltPreFabs;

    [SerializeField]
    private Transform cam;

    private FireBolt bolt;

    private float distanceIfNoTarget = 1000f;

    public void LaunchSpell(SpellLevel spellLevel)
    {
        RaycastHit hit;
        Vector3 target;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            target = hit.point;
        else
            target = cam.transform.position + cam.transform.forward * distanceIfNoTarget;

        GameObject spell = Instantiate(fireBoltPreFabs[(int) spellLevel], transform.position, cam.transform.rotation);
        Rigidbody rb = spell.GetComponent<Rigidbody>();
        rb.AddForce((target - transform.position).normalized * fireBoltPreFabs[(int) spellLevel].GetComponent<FireBolt>().missleSpeed, ForceMode.VelocityChange);
    }
}
