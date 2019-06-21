using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VehicleType { Bike = 0, Car, Jeep, Truck, Tank, BigTank, JetPlan, Helicopter}
public enum VehiclePos { Top, Middle, Bottom }

public class Vehicle : MonoBehaviour {

    [SerializeField]
    protected float speed = 0;
    [SerializeField]
    protected Color vehicleColor;
    [SerializeField]
    protected VehicleType aiVehicleType;
    [SerializeField]
    protected VehiclePos aiVehiclePos;
    [SerializeField]
    protected SpriteRenderer thisSpriteRender;

    [SerializeField]
    protected Transform thisTransform;

    [SerializeField]
    protected GameObject thisObject;

    [SerializeField]
    protected Collider2D thisCollider;

    [SerializeField]
    protected Rigidbody2D thisRdBody;

    protected virtual void Awake()
    {
        thisRdBody = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();
        thisObject = GetComponent<GameObject>(); ;
        thisSpriteRender = GetComponent<SpriteRenderer>();
        thisTransform = GetComponent<Transform>();
    }
}
