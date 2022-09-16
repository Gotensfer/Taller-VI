using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FlyingStates_Module))]
public class FlyRestraint_Module : MonoBehaviour
{
    // Datos y referencias generales
    Rigidbody2D rb;
    const float airDensity = 1.225f;

    // Variables con relación a la caida
    public float fallVelocityLimit;
    Vector2 airResistanceForce_Fall;
    Vector2 fallVelocityVector;  
    float airDragCoefficient_Fall = 0;

    // Variables con relación al avance horizontal
    public float forwardVelocityLimit;
    public Vector2 standardForwardForce;
    Vector2 forwardVelocityVector;
    Vector2 airResistanceForce_Forward;
    float airDragCoefficient_Forward = 0;

    FlyingStates_Module flyingStates_Module;

    private void Start()
    {
        forwardVelocityLimit = LaunchData.maxVelocity;

        rb = GetComponent<Rigidbody2D>();
        flyingStates_Module = GetComponent<FlyingStates_Module>();

        // Cálculo de coeficientes de resistencia
        airDragCoefficient_Fall = -(2f * rb.mass * Physics2D.gravity.y) / (fallVelocityLimit * fallVelocityLimit * airDensity * 1);
        airDragCoefficient_Forward = -(2f * rb.mass * standardForwardForce.x) / (forwardVelocityLimit * forwardVelocityLimit * airDensity * 1);

        fallVelocityVector = new Vector2(0, 0);
    }

    private void FixedUpdate()
    {
        // Cálculo de la fuerza de resistencia en caida
        fallVelocityVector.y = rb.velocity.y >= 0 ? 0: rb.velocity.y;
        airResistanceForce_Fall = 0.5f * airDensity * airDragCoefficient_Fall * fallVelocityVector * fallVelocityVector; // Gracias https://www.quora.com/How-do-you-calculate-air-resistance-for-a-freely-falling-body
        rb.AddForce(airResistanceForce_Fall);

        // No aplicar fuerzas de resistencia al movimiento a menos de que se esté volando de forma estándar
        if (flyingStates_Module.PlayerState != PlayerStates.standardFlying) return;

        //Calculo de la fuerza de resistencia al movimiento
        rb.AddForce(standardForwardForce);
        forwardVelocityVector.x = rb.velocity.x <= 0 ? 0 : rb.velocity.x;
        airResistanceForce_Forward = 0.5f * airDensity * airDragCoefficient_Forward * forwardVelocityVector * forwardVelocityVector;
        rb.AddForce(airResistanceForce_Forward);
    }
}
