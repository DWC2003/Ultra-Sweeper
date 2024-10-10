using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Player
{
    private bool defusalSkill;
    private bool observationSkill;
    private int observationCooldown;
    private int observationLvl;
    private int defuseCooldown;
    private int defuseLvl;

    public Player()
    {
        defusalSkill = false;
        defuseCooldown = 0;
        observationSkill = false;
        observationCooldown = 0;
    }

    public void Update()
    {
        if (defuseCooldown > 0)
        {
            defuseCooldown--;
        }
        if (observationCooldown > 0)
        {
            observationCooldown--;
        }
    }

    public int getDefuseCooldown()
    {
        return defuseCooldown;
    }

    public bool getDefuseSkill()
    {
        return defusalSkill;
    }

    public void setDefuseSkill(bool setState)
    {
        defusalSkill = setState;
    }

    public void setDefuseCooldown(int setCooldown)
    {
        defuseCooldown = setCooldown;
    }

    public int getDefuseLvl()
    {
        return defuseLvl;
    }

    public void incDefuseLvl()
    {
        defuseLvl++; 
    }

    public bool getObservationSkill()
    {
        return observationSkill;
    }

    public void setObservationSkill(bool setSkill)
    {
        observationSkill = setSkill;
    }

    public int getObservationCooldown()
    {
        return observationCooldown;
    }

    public void setObservationCooldown(int setCooldown)
    {
        observationCooldown = setCooldown;
    }


}