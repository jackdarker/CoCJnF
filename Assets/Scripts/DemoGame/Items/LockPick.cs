using System;
using UnityEngine;

public class LockPick : InventoryItem {
    public const string UID = "Lockpick";
    public LockPick() : base(UID) {
        m_Name = UID;
        SetDescription("A Lockpick that can be used to pick simple locks.");
    }

}

