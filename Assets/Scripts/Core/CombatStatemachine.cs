using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CombatStatemachine : StateMachine
{
    public Image m_BlackScreen;
    public ButtonList m_SelButtons;
    public InfoBox m_Info;
    public Text m_Message;

    public Transform m_PlayerLocation;
    public Transform m_EnemyLocation;

    private Battle m_Battle;
    private Wave m_Wave;

    private void Awake() {
        m_BlackScreen.fillAmount = 1;
        m_BlackScreen.gameObject.SetActive(true);
    }
    void Start()
    {
        m_Battle = new Battle();
        m_Wave = m_Battle.GetWave();
        OnFinish = delegate {
            ChangeState(new NextWaveState(this));
        };
        StartCoroutine(RemoveBlackScreen(OnFinish));  
    }
    void Update()
    {
        if (Input.GetButtonDown(InputController.k_Cancel))
        {
            CurrentState.OnCancle();
        }
        else if (Input.GetButtonDown(InputController.k_Submit))
        {
            CurrentState.OnSubmit();
        }
        else if (Input.GetButtonDown(InputController.k_Use))
        {
            //  CurrentState.OnSubmit();
        }
        else
        {
            int horizontal = 0;     //Used to store the horizontal move direction.
            int vertical = 0;       //Used to store the vertical move direction.
                                    //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            //Check if moving horizontally, if so set vertical to zero.
            if (horizontal != 0)
            {
                vertical = 0;
            }
            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                CurrentState.MoveRelative(new Vector3(horizontal, 0, vertical), null);

            }
        }

    }

    public Wave GetWave() {
        return m_Wave;
    }
    public void SpawnEnemy() {
        //BaseMonster _Prefab =new Tiger() ; 
        //Instantiate(_Prefab, /*m_EnemyLocation.transform.position, m_EnemyLocation.transform.rotation,*/ m_EnemyLocation);
        //Wave _wave=m_Battle.GetWave();
        
    }
    Action OnFinish;
    IEnumerator RemoveBlackScreen(Action OnFinish) {
        while (m_BlackScreen.fillAmount > 0) {
            m_BlackScreen.fillAmount -= 1.0f / 2.0f * Time.deltaTime;
            yield return null;
        }
        m_BlackScreen.gameObject.SetActive(false);
        if (OnFinish != null) OnFinish();
    }
}
