using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Base class of objects placed in the scene that the player can interact with: a chest that can be looted, an NPC that can be talked to
/// </summary>
public class BaseInteractable : MonoBehaviour {
    public Transform interactionLocation;                   // The position and rotation the player should go to in order to interact with this Interactable.
    // All the different Conditions and relevant Reactions that can happen based on them.
    public ReactionCollection ReactionCollection = new ReactionCollection();
    public ReactionCollection defaultReactionCollection = new ReactionCollection();    // If none of the ConditionCollections are reacted to, this one is used.

    void Start() {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerClickDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
        ReactionCollection.GameObject = this;
        defaultReactionCollection.GameObject = this;
        OnStart();
    }
    public virtual void OnStart() { }
    public void OnPointerClickDelegate(PointerEventData data) {
        Debug.Log("OnPointerDownDelegate called.");
        Interact();
    }

    // This is called when the player arrives at the interactionLocation.
    public void Interact() {    //
        if(!ReactionCollection.React()) {
            defaultReactionCollection.React();
        }
    }
}