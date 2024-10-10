using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Enemy
{
    private int HP;
    private int speed;
    private int x;
    private int y;
    private int damage;
    private Random rnd = new Random();

    public Enemy(int hp)
    {
        this.HP = hp;
        speed = 8;
        x = rnd.Next(1400);
        y = 0;
        damage = 3;
    }

    public int Update()
    {
        if (y < 400)
        {
            int AI = rnd.Next(3);

            if (AI == 0)
            {
                y += speed;
            }
            else if (AI == 1)
            {
                y += speed / 2;
                x += speed / 4;
            }
            else if (AI == 2)
            {
                y += speed / 2;
                x -= speed / 4;
            }
            
            if (x < 0)
            {
                x = 0;
            }

            if (x > 1400)
            {
                x = 1400;
            }

            return 0;
        }
        else
        {
            return damage;
        }
    }

    public int GetHP()
    {
        return HP;
    }

    public void lowerHP(int damage)
    {
        HP -= damage;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }
}
