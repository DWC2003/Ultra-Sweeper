using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Tile
{
    private bool swept;
    private bool hasMine;
    private int adjMines;
    private bool flagged;
    private int x;
    private int y;
    private Texture2D sprite;

    public Tile(Texture2D startingSprite, int xpos, int ypos)
    {
        hasMine = false;
        swept = false;
        flagged = false;
        x = xpos;
        y = ypos;
        sprite = startingSprite;
        adjMines = 0;
    }

    public Texture2D getSprite()
    {
        return sprite;
    }

    public void setSprite(Texture2D newSprite)
    {
        sprite = newSprite; 
    }

    public void setMine(bool mine)
    {
        hasMine = mine;
    }

    public bool isMine()
    {
        return hasMine;
    }

    public void incMines()
    {
        adjMines++; 
    }

    public int getAdjMines()
    {
        return adjMines;
    }

    public void setAdjMines(int num)
    {
        adjMines = num;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void setFlag(bool flagging)
    {
        flagged = flagging;
    }

    public bool getFlag()
    {
        return flagged;
    }

    public bool getSwept()
    {
        return swept;
    }

    public void setSwept(bool swept)
    {
        this.swept = swept;
    }

}