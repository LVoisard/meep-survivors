using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class Helper
{
    public static IEnumerator StartCooldown(float time, Action cooldownFinishedCallback)
    {
        yield return new WaitForSeconds(time);
        cooldownFinishedCallback?.Invoke();
    }

}