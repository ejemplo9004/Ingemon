using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExecuter : MonoBehaviour
{
    private Queue<AnimationRequest> queue;
    private bool isRunning;

    private void Start()
    {
        queue = new Queue<AnimationRequest>();
    }

    public void RequestAnAnimation(AnimationRequest request)
    {
        queue.Enqueue(request);
        if (isRunning) return;
        StartCoroutine(AnimateCoroutine());
    }

    private IEnumerator AnimateCoroutine()
    {
        isRunning = true;
        AnimationRequest request;
        WaitForSeconds wait = new WaitForSeconds(1);
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        while (queue.Count > 0)
        {
            request = queue.Dequeue();
            request.ExecuteAnimation(info);
            if (queue.Count > 0)
            {
                if (request.AreActors(queue?.Peek().GetActors()))
                {
                    yield return wait;
                }
            }
            else
            {
                yield return wait;
            }
        }
        isRunning = false;
        yield return null;
    }
}
