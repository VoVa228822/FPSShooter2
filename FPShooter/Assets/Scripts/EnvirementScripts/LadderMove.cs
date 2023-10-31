using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMove : MonoBehaviour
{
    public float climbSpeed = 5f; // Adjust this value as needed

    private bool isClimbing = false;

    Rigidbody rb;
    private void Start()
    {
            rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;

            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;

            rb.isKinematic = false;
        }
    }

    private void Update()
    {
        if (isClimbing)
        {

            float verticalInput = Input.GetAxis("Vertical"); // You can use a different input method

            Vector3 climbDirection = new Vector3(0f, verticalInput, 0f);
            transform.Translate(climbDirection * climbSpeed * Time.deltaTime);
        }
    }
}
