using System;
using System.Collections;
using UnityEngine;

#region Instructions
/*
 * 
 * 
 * Implement the function 'Do' below so that it can be called from any context.
 * You want to pass it a function and a float 'delay'. After 'delay' seconds, the function is to be executed.
 * You can create as many additional functions as you need.
 * Assume that this class needs to be a 'MonoBehaviour', so don't change that.
 * 
 * 
 */
#endregion

public class DelayedExecution : MonoBehaviour
{
    public delegate void DialogDelegate();
    public DialogDelegate FunctiontoCall;
    bool isCoroutineExecuting;
    void Start()
    {
        FunctiontoCall = ReturnDialog;
        Do(FunctiontoCall, 2.0f);
    }
    private void ReturnDialog()
    {
		Debug.Log("But then i took an arrow in the knee!");
    }

	 public void Do(DialogDelegate function,float delay)
     {
		Debug.Log("I used to be an adventurer like you");
        StartCoroutine(ExecuteAfterTime(2.0f, () =>
        {
            function();
        }));    

     }
    IEnumerator ExecuteAfterTime(float time, DialogDelegate function)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        function();
        isCoroutineExecuting = false;
    }
}

