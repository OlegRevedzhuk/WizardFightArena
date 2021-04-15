using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator player;
    private CharacterController playerController;
    private ThirdPersonMovement playerMoveScript;

    private int runSpeedHash;
    private int horizontalHash;
    private int verticalHash;
    private float speedToAnimationValueScaling = 16f;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerMoveScript = GetComponent<ThirdPersonMovement>();
        runSpeedHash = Animator.StringToHash("runSpeed");
        verticalHash = Animator.StringToHash("verticalMovement");
        horizontalHash = Animator.StringToHash("horizontalMovement");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 horizontalVelocity = new Vector3(playerController.velocity.x, 0, playerController.velocity.z);
        float value = horizontalVelocity.magnitude * speedToAnimationValueScaling;
        bool isMoving = playerMoveScript.IsMoving();

        player.SetFloat(horizontalHash, Input.GetAxis("Horizontal"));
        player.SetFloat(verticalHash, Input.GetAxis("Vertical"));

        if (!isMoving)
        {
            player.SetFloat(runSpeedHash, 0f);
            return;
        }

        player.SetFloat(runSpeedHash, value);
    }
}