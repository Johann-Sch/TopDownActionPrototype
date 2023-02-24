using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseState<TOwner> where TOwner : Controller
{
    public FSM<TOwner> FSM { get; }
    public TOwner Owner => FSM.Owner;
    
    protected List<Transition<TOwner>> m_transitions = new List<Transition<TOwner>>();

    public BaseState(FSM<TOwner> fsm)
    {
        FSM = fsm;
    }

    public void AddTransition(BaseState<TOwner> toState, Func<bool> condition, UnityAction transitionEffect = null)
    {
        if (toState != null && condition != null)
            m_transitions.Add(new Transition<TOwner>(toState, condition, transitionEffect));
    }

    public bool CheckTransitions()
    {
        foreach (Transition<TOwner> transition in m_transitions)
            if (transition.Condition())
            {
                transition.TransifionEffect?.Invoke();
                transition.ToState.FSM.ChangeState(transition.ToState);

                return true;
            }

        return false;
    }

    public virtual void Enter() {}
    public virtual void Update(float dt) {}
    public virtual void Exit() {}
}