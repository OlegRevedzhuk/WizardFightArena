using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    public enum SpellLevel
    {
        minor,
        normal,
        major
    }

    public enum SpellType
    {
        fireBolt,
        magicMissle,
    }

    [SerializeField] private GameObject[] fireBoltPreFabs;
    [SerializeField] private GameObject[] magicMisslePreFabs;
    [SerializeField] private PlayerAttributes caster;
    [SerializeField] private Transform cam;
    
    private GameObject[][] projectilePreFabs = new GameObject[2][];
    private float[][] missleSpeed = new float[3][];

    private float distanceIfNoTarget = 1000f;

    void Start()
    {
        missleSpeed[(int)SpellType.fireBolt] = new float[3];
        missleSpeed[(int)SpellType.magicMissle] = new float[1];

        projectilePreFabs[(int)SpellType.fireBolt] = fireBoltPreFabs;
        projectilePreFabs[(int)SpellType.magicMissle] = magicMisslePreFabs;

        missleSpeed[(int)SpellType.fireBolt][(int)SpellLevel.minor] 
            = projectilePreFabs[(int)SpellType.fireBolt][(int)SpellLevel.minor].GetComponent<FireBolt>().GetSpeed();

        missleSpeed[(int)SpellType.fireBolt][(int)SpellLevel.normal] 
            = projectilePreFabs[(int)SpellType.fireBolt][(int)SpellLevel.normal].GetComponent<FireBolt>().GetSpeed();

        missleSpeed[(int)SpellType.fireBolt][(int)SpellLevel.major] 
            = projectilePreFabs[(int)SpellType.fireBolt][(int)SpellLevel.major].GetComponent<FireBolt>().GetSpeed();

        missleSpeed[(int)SpellType.magicMissle][(int)SpellLevel.minor] 
            = projectilePreFabs[(int)SpellType.magicMissle][(int)SpellLevel.minor].GetComponent<MagicMissle>().GetSpeed();
    }

    public void LaunchSpell(int spellType, int spellLevel)
    {
        RaycastHit hit;
        Vector3 target;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            target = hit.point;
        else
            target = cam.transform.position + cam.transform.forward * distanceIfNoTarget;

        GameObject spell = Instantiate(projectilePreFabs[spellType][spellLevel], transform.position, cam.rotation);
        Rigidbody rb = spell.GetComponent<Rigidbody>();
        rb.AddForce((target - rb.position).normalized * missleSpeed[spellType][spellLevel], ForceMode.VelocityChange);
        if(spellType == (int) SpellType.magicMissle)
        {
            spell.GetComponent<MagicMissle>().SetCaster(caster);
        }
    }
}
