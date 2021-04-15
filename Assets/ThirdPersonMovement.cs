using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    private CharacterController controller;
    private float horizontal = 0f;
    private float vertical = 0f;
    private float speed = 3f;
    private bool isMoving;

    private float currentSpeedBuff;
    private float currentSlowDebuff = 1f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y + 1.5f, 0f);

        if (direction.magnitude > 0.1f)
        {
            isMoving = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
            isMoving = false;
    }

    public void StartSpeedMod(float multiplier)
    {
        speed *= multiplier;

        if (multiplier > 1f)
            currentSpeedBuff = multiplier;
        else
            currentSlowDebuff *= multiplier;
    }

    public IEnumerator StopSpeedMod(float buffDuration)
    {
        yield return new WaitForSeconds(buffDuration);
        
        speed /= currentSpeedBuff;
    }

    public IEnumerator StopSlowDebuff(float slowDuration)
    {
        yield return new WaitForSeconds(slowDuration);

        speed /= currentSlowDebuff;
        currentSlowDebuff = 1f;
    }
    public void StopSpeedEarly()
    {
        speed /= currentSpeedBuff;
    }

    public void StopSlowEarly()
    {
        speed /= currentSlowDebuff;
        currentSlowDebuff = 1f;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
