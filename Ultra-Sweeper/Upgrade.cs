using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Upgrade
{
    private int weaponNumber;
    private String name;

    public Upgrade(int weaponNumber, String name)
    {
        this.weaponNumber = weaponNumber;
        this.name = name;
    }

    public int getWeaponNum()
    {
        return weaponNumber;
    }

    public String getName()
    {
        return name;
    }
}
