using System;
using UnityEngine;

public class WiseManGoogles : InventoryItem {
    public const string UID = "WiseManGoogles";
    public WiseManGoogles() : base(UID) {
        m_Name = UID;
        SetDescription("Googles that make you look smart.");
    }
    public override bool IsQuestItem() {
        return true;
    }

}

