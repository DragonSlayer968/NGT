using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] float turnSpeed = 5.0f;
    [SerializeField] float acceleration = 8.0f;

    float elapsedTime;
    float timeLimit = 3.0f;

    Quaternion targetRotation;
    Rigidbody _rigidbody;

    Vector3 lastPosition;
    float _sideSlipAmount = 0;

    public float SideSlipAmount
    {
        get
        {
            return _sideSlipAmount;
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SetRotationPoint();
        SetSideSlip();
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit)
        {
            elapsedTime = 0;
            turnSpeed = Random.Range(15.0f, 100.0f);
            acceleration = Random.Range(800.0f, 1600.0f);
        }
    }

    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;

        _sideSlipAmount = movement.x * 20;
    }


   private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if(plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    private void FixedUpdate()
    {
        float speed = _rigidbody.velocity.magnitude / 1000;

        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : 
                       Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidbody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                    turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.fixedDeltaTime);

    }
}

