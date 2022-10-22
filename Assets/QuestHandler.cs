using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public List<Quest> quests;
    public Quest activeQuest = null;

    Quest GetNextQuest() {
        Quest quest = quests[0];
        quests.Remove(quest);
        return quest;
    }

    void Start() {
        activeQuest = GetNextQuest();
        activeQuest.isActive = true;
    }

    void Update() {
        if(activeQuest.isFinished) {
            activeQuest = GetNextQuest();
            if(!activeQuest) { // No more quests
                // Game is over, player wins
            }
            activeQuest.isActive = true;
        }
    }
}
