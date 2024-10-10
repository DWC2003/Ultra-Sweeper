using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Projectile
{
    private int speed;
    private Vector2 position;
    private Vector2 startingPosition;
    private Vector2 angle;
    private int damage;
    private bool used;
    private bool explosive;

    public Projectile(bool explodes, int dmg, Vector2 start, Vector2 trajectory)
    {
        speed = 40;
        position = start;
        startingPosition = start;
        angle = trajectory;
        damage = dmg;
        used = false;
        explosive = explodes;
    }

    public Vector2 getPos()
    {
        return position;
    }

    public int getDamage()
    {
        return damage;
    }

    public void setUsed(bool used)
    {
        this.used = used;
    }

    public bool isUsed()
    {
        return used;
    }

    public bool isExplosive()
    {
        return explosive;
    }

    public Vector2 getStartPos()
    {
        return startingPosition;
    }

    public void Update()
    {
        angle.Normalize();
        position += angle * speed;
    }
}
