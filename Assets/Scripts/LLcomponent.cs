using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLcomponent : MonoBehaviour
{
    [SerializeField] private Transform Input;
    [SerializeField] private Transform Output;
    [SerializeField] private float SphereRadius;
    [SerializeField] private LayerMask ConductLayer;
    [SerializeField] private LLComponentType LLCtype;
    [SerializeField] private float TimerTime;
    public bool IsContacting
    {
        get { return isContacting; }
    }
    public bool IsOutPutting
    {
        get { return isOutPutting; }
    }
    private bool GetInput;
    private bool isContacting;
    private Timer timer;
    private enum LLComponentType
    {
        NormallyOpen,
        NormallyClosed,
        Timer,
    }
    private bool isOutPutting;

    private void Start()
    {
        timer = gameObject.GetComponent<Timer>();
        timer.Duration = TimerTime;
    }
    private void Update()
    {
        GetInput = Input.parent.GetComponent<InputController>().GetInput;
        ProvideOutPut();
    }
    private void CheckContact()
    {
        if (Physics.CheckSphere(Input.position, SphereRadius, ConductLayer) && GetInput)
            isContacting = true;
        else
            isContacting = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Input.position, SphereRadius);
        if (isOutPutting)
            Gizmos.DrawWireSphere(Output.position, SphereRadius);
    }
    private void CheckInputNO()
    {

        if (isContacting)
        {
            isOutPutting = true;
        }
        else
        {
            isOutPutting = false;
        }

    }
    private void CheckInputNC()
    {
        if (isContacting)
        {
            isOutPutting = false;
        }
        else
        {
            isOutPutting = true;
        }
    }
    private void CheckInputTimer()
    {
        if (isContacting)
        {
            Invoke("TimerHelper", TimerTime);
        }
        else
        {
            CancelInvoke();
            isOutPutting = false;
        }
    }
    private void TimerHelper()
    {
       // if(isContacting)
        isOutPutting = true;
    }
    private void ProvideOutPut()
    {
        CheckContact();
        if(LLCtype == LLComponentType.NormallyClosed)
        {
            CheckInputNC();
        }
        else if (LLCtype == LLComponentType.NormallyOpen)
        {
            CheckInputNO();
        }
        else if(LLCtype == LLComponentType.Timer)
        {
            CheckInputTimer();
        }
    }

}
