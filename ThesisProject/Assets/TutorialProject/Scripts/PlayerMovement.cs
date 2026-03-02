using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] RigidBody characterRB;
    [SerializeField] Vector3 movementInput;
    [SerializeField] Vector3 movementVector;
    private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
    }

    private void OnMovement(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        movementInput = new Vector3(inputVector.x, 0f, inputVector.y);
    }

    private void OnMovementStop()
    {
        movementInput = Vector3.zero;
        characterRB.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementInput != Vector3.zero)
        {
            movementVector =
                transform.right * movementInput.x +
                transform.forward * movementInput.z;

            movementVector.y = 0f;

            characterRB.velocity =
                movementVector * movementSpeed * Time.deltaTime;
        }
    }
}
