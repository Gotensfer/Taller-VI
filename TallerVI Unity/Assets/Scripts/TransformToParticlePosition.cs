using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToParticlePosition : MonoBehaviour
{
    [SerializeField] ParticleSystem parentOfTargetParticle;

    enum VFXState
    {
        inactive,
        active
    }

    VFXState currentVFXState = VFXState.inactive;
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1];
    Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        switch (currentVFXState)
        {
            case VFXState.inactive:

                if (parentOfTargetParticle.isPlaying)
                {
                    currentVFXState = VFXState.active;
                }

                break;

            case VFXState.active:

                parentOfTargetParticle.GetParticles(particles);
                transform.localPosition = particles[0].position;

                if (parentOfTargetParticle.isStopped)
                {
                    transform.localPosition = originalPosition;
                    currentVFXState = VFXState.inactive;
                }

                break;
        }
    }
}
