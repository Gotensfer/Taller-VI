using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyRestraint_Module : MonoBehaviour
{


    // Datos y referencias generales
    Rigidbody2D rb;
    const float airDensity = 1.225f;

    // Variables con relación a la caida
    [SerializeField] float fallVelocityLimit;
    Vector2 airResistanceForce_Fall;
    Vector2 fallVelocityVector;  
    float airDragCoefficient_Fall = 0;

    // Variables con relación al avance horizontal
    [SerializeField] float forwardVelocityLimit;
    [SerializeField] Vector2 standardForwardForce;
    Vector2 forwardVelocityVector;
    Vector2 airResistanceForce_Forward;
    float airDragCoefficient_Forward = 0;


    private void Start()
    {
        // Solo se limita la velocidad al caer, mas no al subir o elevarse
        rb = GetComponent<Rigidbody2D>();

        airDragCoefficient_Fall = -(2f * rb.mass * Physics2D.gravity.y) / (fallVelocityLimit * fallVelocityLimit * airDensity * 1);
        airDragCoefficient_Forward = -(2f * rb.mass * standardForwardForce.x) / (forwardVelocityLimit * forwardVelocityLimit * airDensity * 1);

        fallVelocityVector = new Vector2(0, 0);
    }

    bool flyingStandard;

    private void FixedUpdate()
    {
        fallVelocityVector.y = rb.velocity.y >= 0 ? 0: rb.velocity.y;
        airResistanceForce_Fall = 0.5f * airDensity * airDragCoefficient_Fall * fallVelocityVector * fallVelocityVector; // Gracias https://www.quora.com/How-do-you-calculate-air-resistance-for-a-freely-falling-body
        rb.AddForce(airResistanceForce_Fall);

        if (!flyingStandard) return;
        rb.AddForce(standardForwardForce);
        forwardVelocityVector.x = rb.velocity.x <= 0 ? 0 : rb.velocity.x;
        airResistanceForce_Forward = 0.5f * airDensity * airDragCoefficient_Forward * forwardVelocityVector * forwardVelocityVector;
        rb.AddForce(airResistanceForce_Forward);
    }

    public void AllowStandardForwardForce()
    {
        flyingStandard = true;
    }

    public void DeactivateStandardForwardForce()
    {
        flyingStandard = false;
    }
}
