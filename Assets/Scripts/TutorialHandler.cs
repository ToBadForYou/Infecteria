using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialTextMesh;

    public Dictionary<EventType, string> events = new Dictionary<EventType, string>()
    {
        {EventType.Infected, "You just infected your first cell, congratulations! Now you have two choices, either you can absorb the cell (E) or you can make it into a factory (Q)."},
        {EventType.Absorbed, "You just absorbed your first cell, congratulations! Absorbing cells will grant you DNA points and recover lost health."},
        {EventType.MadeFactory, "Congratulations, you just made your first factory! Factories can do many things, such as autoinfect cells, build structures and produce units. You may also upgrade it to unlock more building slots."},
        {EventType.EnteredCell, "You just entered a cell! By staying inside you will increase its infection level until it has been fully infected."},
        {EventType.Microbacteria, "You just produced your first microbacteria! These are your friends. You can select them by holding left mouse button and draging over them."},
        {EventType.ScoutDetected, "A scout has spotted you! Deal with it before it can report your presence to the heart!"},
        {EventType.DetectorDetected, "A detector has spotted you! A few antibodies has been deployed to deal with you, but most importantly a scout which will inform the heart about your presence unless you deal with it."},
        {EventType.CollectedSugar, "You just obtained your first sugar! Sugar can be used to upgrade factories and build structures inside your factories."},
        {EventType.CollectedDNA, "You just obtained your first DNA Point! DNA Points can be used in the SkillTree (T) to mutate your bacteria."},
        {EventType.UnitSelected, "Some text about unit selection"},
    };

    public void TriggerEvent(EventType type) {
        transform.GetChild(0).gameObject.SetActive(true);
        PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);
        tutorialTextMesh.text = events[type];
        events.Remove(type);
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.Return) && PauseManager.Instance.CurrPauseState == PauseManager.PauseState.FULL) {
            transform.GetChild(0).gameObject.SetActive(false);
            PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
        }
        else if(Input.GetKeyUp(KeyCode.X) && PauseManager.Instance.CurrPauseState == PauseManager.PauseState.FULL) {
            PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
            Destroy(gameObject);
        }
    }
}

public enum EventType {
    Infected,
    Absorbed,
    EnteredCell,
    MadeFactory,
    UnitSelected,
    CollectedDNA,
    Microbacteria,
    ScoutDetected,
    CollectedSugar,
    DetectorDetected
}