using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlowController : MonoBehaviour
{

    // General
    /*public DialogViewController dialogViewController;
    public GameViewController gameViewController;

    // Intro
    public IntroLogoViewController introLogoViewController;
    public IntroTitleViewController introTitleViewController; */
    public IntroMenuViewController introMenuViewController;

    // Setup - Create New
    public NewGameViewController playerConfigureViewController;/*

    // Play
    public GetSetViewController getSetViewController;
    public RollViewController rollViewController;
    public TeamViewController teamViewController;*/

    // Journey
    public ExploreViewController exploreViewController;/*
    public RandomEncounterViewController randomEncounterViewController;
    public GymEncounterViewController gymEncounterViewController;
    public AttackTeamViewController attackTeamViewController;

    // Battle
    public CombatViewController combatViewController;
    public CommandViewController commandViewController;
    public CaptureViewController captureViewController;
    public VictoryViewController victoryViewController;
    public DefeatViewController defeatrViewController;*/

    // Music
    //public MusicController musicController;

    // Other
    [SerializeField] DataController dataController;
    Game game
    {
        get { return dataController.game; }
        set { dataController.game = value; }
    }
   /* Battle battle
    {
        get { return dataController.battle; }
        set { dataController.battle = value; }
    }
    Board board
    {
        get { return dataController.board; }
    }*/

    StateMachine stateMachine = new StateMachine();

    void Start()
    {
        dataController = DataController.instance;
        dataController.Load(delegate {
            stateMachine.ChangeState(IntroState);
        });
    }
}