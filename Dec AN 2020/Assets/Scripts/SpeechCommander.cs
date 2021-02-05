using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; //Allows functions to be referenced as Actions
using System.Linq; // To use the "ToArray()" function
using UnityEngine.Windows.Speech; //Speech recognition library

public class SpeechCommander : MonoBehaviour
{

    private Dictionary<string, Action> m_keywordActions = new Dictionary<string, Action>();

    private KeywordRecognizer m_keywordRecognizer;

    // Start is called before the first frame update
    void Start()
    {
        m_keywordActions.Add("Hello", Test);

        m_keywordActions.Add("Bye", GoodBye);

        m_keywordRecognizer = new KeywordRecognizer(m_keywordActions.Keys.ToArray());
        m_keywordRecognizer.OnPhraseRecognized += WordRecognized;
        m_keywordRecognizer.Start();
    }

    private void OnDisable()
    {
        m_keywordRecognizer.Stop();
    }

    void WordRecognized(PhraseRecognizedEventArgs args)
    {
        m_keywordActions[args.text].Invoke();
    }

    void Test()
    {
        Debug.Log("HELLO WORLD!");
    }

    void GoodBye()
    {
        Debug.Log("BYE BYE!");
    }
}
