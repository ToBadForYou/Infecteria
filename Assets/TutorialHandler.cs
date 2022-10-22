using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialTextMesh;

    public Dictionary<EventType, string> events = new Dictionary<EventType, string>()
    {
        {EventType.Infected, "You just infected your first cell, congratulations! Now you have two choices, either you can absorb the cell or you can make it into a factory."},
        {EventType.Absorbed, "You just absorbed your first cell, congratulations! When you absorb a cell, it dies and your health gets reset."},
        {EventType.MadeFactory, "Congratulations, you just made your first factory! Factories can do many things, such as autoinfect cells and produce units. Upgrade your factories to make things easier."},
        {EventType.EnteredCell, "You just entered an cell! In order to infect an cell you have to stay within the cell for a short period of time. You will notice when it's infected. Good luck!"},
        {EventType.Microbacteria, "You just produced your first microbacteria! These are your friends. You can select them by holding left mouse button and draging over them."},
        {EventType.ScoutDetected, "You just got detected by a scout. This is a bad thing. The scout will tell the body organs about you and start to produce antibodies."},
        {EventType.DetectorDetected, "You just got detected by a detector. This is a bad thing. The detector produces some antibacteria to keep you away and also send out a scout, you will learn more about scouts soon."},
        {EventType.UnitSelected, "Some text about unit selection"}
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
    }
}

public enum EventType {
    Infected,
    Absorbed,
    EnteredCell,
    MadeFactory,
    UnitSelected,
    Microbacteria,
    ScoutDetected,
    DetectorDetected
}