using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDeath : MonoBehaviour
{
    public string targetTag;
    public InimigoCode Inimigo;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == targetTag)
        {
            Inimigo.dead(collision.contacts[0].point);
        }
    }
}
