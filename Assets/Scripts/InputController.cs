using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool GetInput;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Output")
        {
            
            LLcomponent script = collision.GetComponentInParent<LLcomponent>();
            if (script.IsOutPutting)
            {
                GetInput = true;
            }
            else
            {
                GetInput = false;
            }
        }


    }
    private void Update()
    {
        Debug.Log(GetInput);
    }
}
