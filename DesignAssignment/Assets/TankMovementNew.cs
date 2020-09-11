using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovementNew : MonoBehaviour
{
    public float speed;
    public float FloatingDistance;
    public InputAction arrowKeys;
    public InputAction shoot;
    public GameObject TankBarrel;
    public InputMaster controls;
    void Awake()
    {
        shoot.performed += _ => Shoot();
    }

    public void Tet()
    {
        print("TESTE");
    }

    void OnEnable()
    {
        arrowKeys.Enable();
        shoot.Enable();
    }
    void OnDisable()
    {
        shoot.Disable();
        arrowKeys.Disable();
    }
    public void Shoot()
    {
        TankBarrel.GetComponent<BarrelScript>().StartRayCast = true;
    }
    public void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            GameObject currentFloor = hit.transform.gameObject;
            var distance = hit.distance;
            //print($"Distance = {distance}");
            if (distance != FloatingDistance)
            {
                var change = FloatingDistance - distance;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + change, this.transform.position.z);
                var rotationVector = currentFloor.transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        var readKey = arrowKeys.ReadValue<Vector2>();
        var move = new Vector3(readKey.x,0,readKey.y);
        transform.Translate(move.normalized * speed, Space.World);
    }
}
