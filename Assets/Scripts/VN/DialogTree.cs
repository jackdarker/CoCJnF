using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTree 
{
    public DialogTree() { }
    //public DialogResult ContinueDialog(int Choice) {
    //    DialogResult _ret = new DialogResult();

    //    return _ret;
    //}
    public DialogSetup getDialog() {
        return m_Set;
    }

    public void CreateDialogSetup() {
        m_SelectedChoice = 0;
        m_Set = new DialogSetup();
        m_Set.AddElement(new Choice("Continue"));
    }

    public void AddElement(DlgElement Value) {
        m_Set.AddElement(Value);
    }
    public void SetDone() {
        m_Set.m_Done = true;
    }

    public void SetDialogResult(int choice) {
        m_SelectedChoice = choice;
    }
    public int GetDialogResult() {
        return m_SelectedChoice;
    }
    protected int m_SelectedChoice = 0;
    protected DialogSetup m_Set;
    

    // defines a stage setup for dialog (what text to display and how, what images,...)
    public class DialogSetup {
        public DialogSetup() {
            m_Elements = new List<DlgElement>();
        }
        public void AddElement(DlgElement Value) {
            m_Elements.Add(Value);
        }
        public bool m_Done = false;
        public IList<DlgElement> m_Elements;
    }

    public class DlgElement {
        public Vector2 m_Position;
    }

    // a text line
    public class Say : DlgElement {
        public string m_Who;
        public string m_What;
        public string m_How;
    }

    //choice menu
    public class Choice : DlgElement {
        public string[] m_Text;
        public int[] m_Choice;
        public Choice() { }
        public Choice(string Text) {
            m_Text = new string[] { Text };
            m_Choice = new int[] { 0 };
        }
    }

    //Image
    public class Image : DlgElement {
        public string m_Resource;
    }
}
