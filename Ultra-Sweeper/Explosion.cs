using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Explosion
{
    private int dmg;
    private int activeTime;
    private bool exploded = false;
    private Vector2 position;
    private Texture2D sprite;

    public Explosion(int dmg, int activeTime, Texture2D texture, Vector2 pos)
    {
        this.dmg = dmg;
        this.activeTime = activeTime;
        this.sprite = texture;
        position = pos;
    }

    public void Update()
    {
        if (activeTime > 0)
        {
            activeTime--;
        }
    }

    public bool isExploded()
    {
        return exploded;
    }

    public void setExploded(bool state)
    {
        exploded = state;
    }

    public Vector2 getPos()
    {
        return position;
    }

    public int getDmg()
    {
        return this.dmg;
    }

    public int getActiveTime()
    {
        return activeTime;
    }

    public Texture2D getSprite()
    {
        return sprite;
    }
}