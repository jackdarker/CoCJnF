using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour {
    public GameObject m_Panel;


    // Start is called before the first frame update
    protected virtual void Start() {

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    protected void OnCancle(object sender, EventArgs e) {
        Hide();
    }
    public virtual void Display() {
        Show();
    }
    public void Show() {
        m_Panel.gameObject.SetActive(true);
    }
    public void Hide() {
        m_Panel.gameObject.SetActive(false);
    }
}
