using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats
{
    // FIELDS
    private int health;
    private int currentHealth;
    private int damage;
    private int attackSpeed;
    private float attackTimer;
    private float range;
    private bool aggressive;

    public UnitStats(int hp, int currentHp, int dmg, int speed, float time, float r, bool state) {
        SetHealth(hp);
        SetCurrentHealth(currentHp);
        SetDamage(dmg);
        SetAttackSpeed(speed);
        SetAttackTimer(time);
        SetRange(r);
        SetAggressive(state);
    }

    // SETTERS
    public void SetDamage(int dmg) { damage = dmg; }
    public void SetHealth(int hp) { health = hp; }
    public void SetCurrentHealth(int hp) { currentHealth = hp; }
    public void DecreaseCurrentHealth(int amount) { currentHealth -= amount; }
    public void SetAttackSpeed(int speed) { attackSpeed = speed; }
    public void SetAttackTimer(float time) { attackTimer = time; }
    public void DecreaseAttackTimer(float amount) { attackTimer -= amount; }
    public void SetRange(float r) { range = r; }
    public void SetAggressive(bool state) { aggressive = state; }

    // GETTERS
    public int GetDamage() { return damage; }
    public int GetHealth() { return health; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetAttackSpeed() { return attackSpeed; }
    public float GetAttackTimer() { return attackTimer; }
    public float GetRange() { return range; }
    public bool IsAggressive() { return aggressive; }
}
