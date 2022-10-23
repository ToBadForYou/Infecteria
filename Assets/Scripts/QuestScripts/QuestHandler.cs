using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public List<Quest> quests;
    public Quest activeQuest = null;

    public SoundManager sm;

    Quest GetNextQuest() {
        if(quests.Count < 1){
            return null;
        }
        Quest quest = quests[0];
        quests.Remove(quest);
        return quest;
    }

    void Start() {
        activeQuest = GetNextQuest();
        activeQuest.isActive = true;
    }

    void Update() {
        if(activeQuest != null && activeQuest.isFinished) {
            activeQuest = GetNextQuest();
            sm.PlaySound();
            if(activeQuest != null) {
                activeQuest.InitQuest();         
                activeQuest.isActive = true;
            }
        }
    }
}
