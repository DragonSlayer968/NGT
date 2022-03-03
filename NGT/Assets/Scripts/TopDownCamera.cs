using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{

    [SerializeField] Transform observeable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;

    Rigidbody _obserableRigidBody;


    void Start()
    {
        _obserableRigidBody = observeable.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (observeable == null)
            return;

        Vector3 targetPosition = observeable.position + Vector3.up * cameraHeight +
                                 _obserableRigidBody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);


    }
}
