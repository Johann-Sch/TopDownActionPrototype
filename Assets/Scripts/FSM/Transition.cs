using System;
using UnityEngine.Events;

public class Transition<TOwner> where TOwner : Controller
{
    public BaseState<TOwner> ToState { get; }
    public Func<bool> Condition { get; }
    public UnityAction TransifionEffect { get; }

    public Transition(BaseState<TOwner> toState, Func<bool> condition, UnityAction transitionEffect)
    {
        ToState = toState;
        Condition = condition;
        TransifionEffect = transitionEffect;
    }
        
}