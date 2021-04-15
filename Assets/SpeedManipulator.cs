using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManipulator : MonoBehaviour
{
    private ThirdPersonMovement moveController;
    [SerializeField]
    private GameObject ringsPreFab;
    private GameObject rings;

    private bool hasBuff = false;
    private bool hasSlow = false;
    private float currentBuffPower = 0f;
    private IEnumerator movementBuffCoroutine;
    private IEnumerator buffAnimationCoroutine;
    private IEnumerator movementSlowCoroutine;
    private IEnumerator slowAnimationCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<ThirdPersonMovement>();
    }

    public void ActivateBuff(float multiplier, float buffDuration)
    {
        if (multiplier < 1)
        {
            ActivateDebuff(multiplier, buffDuration);
            return;
        }

        if (hasBuff && currentBuffPower > multiplier + .1f)
        {
            //dosomething spell failed to take effect
            return;
        }

        if(hasBuff)
        {
            StopCoroutine(movementBuffCoroutine);
            StopCoroutine(buffAnimationCoroutine);
            Destroy(rings);
            moveController.StopSpeedEarly();
        }

        hasBuff = true;
        currentBuffPower = multiplier;
        moveController.StartSpeedMod(multiplier);

        rings = Instantiate(ringsPreFab, transform.position + 1.2f * Vector3.up, transform.rotation);
        rings.transform.parent = transform;

        buffAnimationCoroutine = BuffDuration(buffDuration, rings);
        movementBuffCoroutine = moveController.StopSpeedMod(buffDuration);
        StartCoroutine(movementBuffCoroutine);
        StartCoroutine(buffAnimationCoroutine);
    }

    private void ActivateDebuff(float multiplier, float debuffDuration)
    {
        if(hasSlow)
        {
            StopCoroutine(movementSlowCoroutine);
            StopCoroutine(slowAnimationCoroutine);
        }
        hasSlow = true;

        movementSlowCoroutine = moveController.StopSlowDebuff(debuffDuration);
        slowAnimationCoroutine = SlowDuration(debuffDuration);

        moveController.StartSpeedMod(multiplier);
        StartCoroutine(movementSlowCoroutine);
        StartCoroutine(slowAnimationCoroutine);
    }

    private IEnumerator BuffDuration(float duration, GameObject rings)
    {
        yield return new WaitForSeconds(duration);
        hasBuff = false;
        Destroy(rings);
    }

    private IEnumerator SlowDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        hasSlow = false;
    }
}
