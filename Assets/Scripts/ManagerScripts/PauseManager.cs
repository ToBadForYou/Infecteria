using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private static PauseManager _instance;
    public static PauseManager Instance{
        private set => _instance = value;
        get{
            if (_instance == null){
                _instance = (PauseManager)FindObjectOfType(typeof(PauseManager));
                if (_instance == null) // if non-existant
                    _instance = new GameObject("Pause Manager").AddComponent<PauseManager>();
            }
            return _instance;
        }
    }

    public PauseState CurrPauseState { get; private set; }
    public PauseState PrevPauseState { get; private set; }

    private void Awake(){
        CurrPauseState = PauseState.NONE;
        PrevPauseState = PauseState.NONE;
    }

    public void SetPauseState(PauseState newPauseState){
        PrevPauseState = CurrPauseState;
        CurrPauseState = newPauseState;
    }

    private void Start(){
        if (FindObjectOfType<PauseManager>() != this)
            Destroy(gameObject);
    }

    public enum PauseState { NONE, FULL }
}
