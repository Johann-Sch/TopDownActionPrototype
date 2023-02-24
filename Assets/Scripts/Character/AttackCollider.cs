using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public float damage = 1f;
    
    [field: SerializeField] public Character Self { get; protected set; }

    protected virtual void Awake()
    {
        if (!Self)
            if ((Self = GetComponentInParent<Character>()) == null)
                Destroy(this);
    }
    
    protected virtual void OnTriggerStay(Collider collider)
    {
        Character character = collider.GetComponent<Character>();
            
        if (character && character != Self)
            character.CooldownedLife.TryDamage(damage);
    }
}
