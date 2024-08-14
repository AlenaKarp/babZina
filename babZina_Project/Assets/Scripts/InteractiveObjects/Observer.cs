//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Runtime.StatefulEvent;
using UnityTools.UnityRuntime.Timers;

public class Observer : MonoBehaviour
{
    [Serializable]
    public struct StateParameters
    {
        public int id;
        public float time;
        public bool deadState;
    }

    public IStatefulEvent<bool> IsDeadState => deadState;
    public IStatefulEvent<int> StateID => stateID;

    public event Action<int> OnDisappointState = (id) => { };

    [SerializeField] private List<StateParameters> normalStates;
    [SerializeField] private int dissapointStateID;

    private StateParameters currentState;
    private bool isDissapointed = false;
    private StatefulEventInt<bool> deadState = StatefulEventInt.Create(false);
    private StatefulEventInt<int> stateID = StatefulEventInt.Create(0);
    private bool isEnable = true;

    private void Start()
    {
        StateProcess();
    }

    private void OnEnable()
    {
        currentState = normalStates[0];
        deadState.Set(currentState.deadState);
        isEnable = true;
    }

    private void OnDisable()
    {
        isEnable = false;
    }

    private void StateProcess()
    {
        Timer.Instance.WaitUnscaled(currentState.time)
            .Done(() =>
            {
                if (TrySetNewState() == false)
                {
                    return;
                }

                StateProcess();
            });
    }

    private bool TrySetNewState()
    {
        if (isEnable == false)
        {
            return false;
        }

        if (isDissapointed == true)
        {
            stateID.Set(dissapointStateID);
            return false;
        }

        int currentIndex = GetIndex(currentState);
        int newIndex = currentIndex + 1;

        if (newIndex == normalStates.Count)
        {
            newIndex = 0;
        }

        SetState(newIndex);

        return true;
    }

    private int GetIndex(StateParameters stateParameters)
    {
        int counter = 0;

        foreach (StateParameters state in normalStates)
        {
            if(state.id == stateParameters.id)
            {
                return counter;
            }

            counter++;
        }

        throw new Exception("Wrong id in states");
    }

    private void SetState(int newStateIndex)
    {
        currentState = normalStates[newStateIndex];

        deadState.Set(currentState.deadState);
        stateID.Set(currentState.id);
    }

    internal void SetDissapointState()
    {
        OnDisappointState(dissapointStateID);
        isDissapointed = true;
    }
}
