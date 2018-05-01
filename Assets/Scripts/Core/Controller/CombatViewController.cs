using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ECS;

public class CombatViewController : BaseViewController
{

    public Action didCompleteMove;

    public CombatantView playerCombatant;
    public CombatantView computerCombatant;
    public Text moveCallout;
    public AudioSource battleCrySource;

    
    void OnEnable()
    {
        Display();
    }

    public void Display()
    {
        foreach (BaseMonster combatant in battle.GetWave().combatants)
        {
            GetView(combatant).Display(combatant);
        }
    }

    public void ApplyMove()
    {
        StartCoroutine(ApplyMoveProcess());
    }

    IEnumerator ApplyMoveProcess()
    {
        yield return StartCoroutine(ShowAttackProcess());
        yield return StartCoroutine(ShowResultProcess());
        if (didCompleteMove != null)
            didCompleteMove();
    }

    IEnumerator ShowResultProcess()
    {
        var combatantView = GetView(battle.GetWave().m_Actor);
        //??StartCoroutine(combatantView.UpdateHitPointsProcess(battle.lastDamage));

        combatantView = GetView(battle.GetWave().combatants[0]);
        yield return StartCoroutine(combatantView.UpdateEnergyProcess());
    }

    IEnumerator ShowAttackProcess()
    {
        var move = battle.GetWave().move;
        var combatant = battle.GetWave().combatants[0];
        moveCallout.text = move.name;
        //battleCrySource.clip = battle.combatants[0].CurrentPokemon.GetBattleCry();
        //battleCrySource.Play();
        CombatantView view = GetView(combatant);
        RectTransform avatar = view.avatarImage.rectTransform;
        Tweener tweener = avatar.ScaleTo(new Vector3(1.5f, 1.5f, 1.5f), 0.25f, EasingEquations.EaseOutCubic);
        while (tweener != null)
            yield return null;
        tweener = avatar.ScaleTo(Vector3.one, 0.5f, EasingEquations.EaseOutBounce);
        while (tweener != null)
            yield return null;
        yield return new WaitForSeconds(0.5f);
        moveCallout.text = "";
    }

    CombatantView GetView(BaseMonster combatant)
    {
        return combatant.IsControlledByPlayer() ? playerCombatant : computerCombatant;
    }
}
