using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextUI : MonoBehaviour
{
    private TMP_Text m_text; 
    // Start is called before the first frame update
    void Awake()
    {
        m_text = GetComponent<TMP_Text>();
    }

    public void UpdateUI(int Number)
    {
        m_text.text = Number.ToString();
    }

    public void UpdateUI(float Number)
    {
        m_text.text = Number.ToString();
    }
    public void UpdateUI(string Text)
    {
        m_text.text = Text;
    }
    public void UpdateUI(ref string Text)
    {
        m_text.text = Text;
    }
    
}
