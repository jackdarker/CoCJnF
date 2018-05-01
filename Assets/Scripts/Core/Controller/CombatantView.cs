using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantView : MonoBehaviour
{

    public Text nameLabel;
    public Text damageLabel;
    public RectTransform hitPoints;
    public RectTransform energy;
    public Image avatarImage;
    //Wave.Combatant 
    BaseMonster combatant;

    public void Display(BaseMonster combatant)
    {
        this.combatant = combatant;
        nameLabel.text = "combatant.CurrentPokemon.Name";
        //??Poses pose = combatant.mode == ControlModes.Player ? Poses.Back : Poses.Front;
       //?? avatarImage.sprite = combatant.CurrentPokemon.GetAvatar(pose);
        hitPoints.localScale = new Vector3(/*combatant.CurrentPokemon.HPRatio*/1, 1, 1);
        energy.localScale = new Vector3(/*combatant.CurrentPokemon.EnergyRatio*/1, 1, 1);
    }

    public IEnumerator UpdateHitPointsProcess(int lastDamage)
    {
        damageLabel.rectTransform.anchoredPosition = Vector2.zero;
        damageLabel.text = lastDamage.ToString();
        damageLabel.rectTransform.AnchorTo(new Vector3(0, 25, 0), 0.5f, EasingEquations.EaseOutExpo);
        Tweener tweener = hitPoints.ScaleTo(new Vector3(/*combatant.CurrentPokemon.HPRatio??*/1, 1, 1));
        while (tweener != null)
            yield return null;
        damageLabel.text = "";
    }

    public IEnumerator UpdateEnergyProcess()
    {
        Tweener tweener = energy.ScaleTo(new Vector3(/*combatant.CurrentPokemon.EnergyRatio??*/1, 1, 1));
        while (tweener != null)
            yield return null;
    }
}
