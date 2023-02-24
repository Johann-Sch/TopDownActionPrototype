using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider collider)
    {
        Character player = collider.GetComponent<Character>(); 
        
        player.CooldownedLife.TryDamage();
        player.Respawn();
    }
}
