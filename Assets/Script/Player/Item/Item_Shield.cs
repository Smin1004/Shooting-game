using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : Item_Base
{
    protected override void TriggerEvent(Collider2D collision)
    {
        collision.GetComponent<Player>().Shield_On();
    }
}