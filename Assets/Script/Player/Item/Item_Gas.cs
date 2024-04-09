using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Gas : Item_Base
{
    protected override void TriggerEvent(Collider2D collision)
    {
        collision.GetComponent<Player>().Repair_Gas(50);
    }
}