using System;
using System.Collections;
using UnityEngine;

namespace Analytics
{
    internal class TrackAwaiter
    {
        private readonly MonoBehaviour _routineStarter;
        private readonly float _cooldown;

        internal Action awaitDoneAction;

        private Coroutine _coroutine;

        internal TrackAwaiter(float cooldown, MonoBehaviour behaviour)
        {
            _cooldown = cooldown;
            _routineStarter = behaviour;
        }

        internal void DoAwaitStart()
        {
            if (_coroutine == null)
                _coroutine = _routineStarter.StartCoroutine(startAwait());
        
        }

        private IEnumerator startAwait()
        {
            yield return new WaitForSeconds(_cooldown);
            _coroutine = null;
            awaitDoneAction.Invoke();
        }
    }
}