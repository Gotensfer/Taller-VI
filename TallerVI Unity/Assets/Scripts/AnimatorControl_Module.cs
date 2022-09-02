using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorControl_Module : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    #region Trigger Animations
    
    public void SetLaunchTrigger()
    {
        animator.SetTrigger("Launch");
    }
    
    public void SetAscendTrigger()
    {
        animator.SetTrigger("Ascend");
    }
    
    public void SetGlideTrigger()
    {
        animator.SetTrigger("Glide");
    }
    
    public void SetFallTrigger()
    {
        animator.SetTrigger("Fall");
    }
    
    public void SetBounceTrigger()
    {
        animator.SetTrigger("Bounce");
    }
    
    public void SetCrashTrigger()
    {
        animator.SetTrigger("Crash");
    }
    
    public void SetChiliTrigger()
    {
        animator.SetTrigger("Chili");
    }
    public void SetOnPidgeonTrigger()
    {
        animator.SetTrigger("OnPidgeon");
    }
    public void SetRocketTrigger()
    {
        animator.SetTrigger("Rocket");
    }
    public void SetMitosisTrigger()
    {
        animator.SetTrigger("Mitosis");
    }
    #endregion
    
}
