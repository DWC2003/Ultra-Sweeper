using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Weapon
{
    private int damage;
    private String type;
    private Vector2 gunpos;
    private int charge;
    private int chargeAdd;
    private int fireDelay;
    private int fireRate;

    public Weapon(Vector2 pos, int startCharge, String type)
    {
        gunpos = pos;
        this.type = type;
        fireRate = 50;
        if (this.type.Equals("Explosive"))
        {
            fireRate += 10;
        }
        damage = 10;
        charge = startCharge;
        chargeAdd = 0;
    }

    public Vector2 getGunPos()
    {
        return gunpos;
    }

    public int getCharge()
    {
        return charge;
    }

    public int getDamage()
    {
        return damage;
    }

    public void incDamage(int incDmg)
    {
        damage += incDmg;
    }

    public int getFireRate()
    {
        return fireRate;
    }

    public void incFireRate(int incFireRate)
    {
        fireRate -= incFireRate;
    }


    public void setCharge(int newCharge)
    {
        charge = newCharge;
    }

    public void chargeUp(int chargeInc)
    {
        charge += (chargeInc + chargeAdd);
    }

    public void incChargeAdd(int incChargeAdd)
    {
        chargeAdd += incChargeAdd;
    }

    public int getChargeAdd()
    {
        return chargeAdd;
    }

    public String getType()
    {
        return type;
    }

    public bool depleteCharge(int chargeDec)
    {
        if (fireDelay == 0)
        {
            charge -= chargeDec;
            fireDelay = fireRate;
            return true;
        }
        else
        {
            fireDelay--;
            return false;
        }
    }
}

