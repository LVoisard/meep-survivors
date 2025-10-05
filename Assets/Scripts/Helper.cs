using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class Helper
{

    private class CoroutineRunner : MonoBehaviour { }

    private static CoroutineRunner runner;

    private static void EnsureRunnerExists()
    {
        if (runner == null)
        {
            var go = new GameObject("Coroutine Runner");
            runner = go.AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(go);
        }
    }

    public static Coroutine Wait(float seconds, System.Action callback)
    {
        EnsureRunnerExists();
        return runner.StartCoroutine(WaitCoroutine(seconds, callback));
    }

    private static IEnumerator WaitCoroutine(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }


}