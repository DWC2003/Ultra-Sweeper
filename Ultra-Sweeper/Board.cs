using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Ultra_Sweeper;


public class Board
{
    public static Tile[,] board;
    private Random rnd = new Random();
    private int mines;
    private bool filled;
    private int size;
    private Texture2D tile;
    private Texture2D mine;
    private Texture2D flag;
    private Texture2D one;
    private Texture2D two;
    private Texture2D three;
    private Texture2D four;
    private Texture2D five;
    private Texture2D six;
    private Texture2D seven;
    private Texture2D eight;

    public Board(int size, Texture2D tileSprite, Texture2D mineSprite, Texture2D flagS, Texture2D oneS, Texture2D twoS, Texture2D threeS, Texture2D fourS, Texture2D fiveS, Texture2D sixS, Texture2D sevenS, Texture2D eightS)
    {
        tile = tileSprite;
        mine = mineSprite;
        flag = flagS;
        one = oneS;
        two = twoS;
        three = threeS;
        four = fourS;
        five = fiveS;
        six = sixS;
        seven = sevenS;
        eight = eightS;
        mines = 50;
        this.size = size;
        board = new Tile[size, size];

        // Creates tiles
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j] = new Tile(tile, i*25+500, j*25+500);
            }
        }


        // Adds mines
        for (int i = 0; i < mines; i++)
        {
            while (true)
            {
                int column = rnd.Next(size);
                int row = rnd.Next(size);
                if (board[column, row].isMine() == true)
                {
                    continue;
                }
                else
                {
                    board[column, row].setMine(true);
                    break;
                }
            }
        }

        // Computes adjacent mines
        for (int c = 0; c < size; c++)
        {
            for (int r = 0; r < size; r++)
            {
                Tile current = board[c, r];
                if (current.isMine())
                {
                    continue;
                }

                for(int i = -1; i <= 1; i++)
                {
                    if (i == -1 && c == 0)
                    {
                        continue;
                    }
                    if (i == 1 && c == size-1)
                    {
                        continue;
                    }
                    for(int j = -1; j <= 1; j++)
                    {
                        if (j == -1 && r == 0)
                        {
                            continue;
                        }
                        if (j == 1 && r == size-1)
                        {
                            continue;
                        }
                        if (board[c + i, r + j].isMine() == true)
                        {
                            current.incMines();
                        }
                    }
                }

            }
        }


        filled = true;
    }

    public void Clear()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j].setMine(false);
                board[i, j].setSprite(tile);
                board[i, j].setAdjMines(0);
            }
        }
        filled = false;
    }

    public bool IsCleared()
    {
        int threshold = size ^ 2;
        int count = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j].getSwept() || board[i, j].isMine())
                {
                    count++;
                }
            }
        }
        if (count == size * size)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Refill()
    {
        if (!filled)
        {
            // Creates tiles
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = new Tile(tile, i * 25 + 500, j * 25 + 500);
                }
            }


            // Adds mines
            for (int i = 0; i < 50; i++)
            {
                while (true)
                {
                    int column = rnd.Next(size);
                    int row = rnd.Next(size);
                    if (board[column, row].isMine() == true)
                    {
                        continue;
                    }
                    else
                    {
                        board[column, row].setMine(true);
                        break;
                    }
                }
            }

            // Computes adjacent mines
            for (int c = 0; c < size; c++)
            {
                for (int r = 0; r < size; r++)
                {
                    Tile current = board[c, r];
                    if (current.isMine())
                    {
                        continue;
                    }

                    for (int i = -1; i <= 1; i++)
                    {
                        if (i == -1 && c == 0)
                        {
                            continue;
                        }
                        if (i == 1 && c == size - 1)
                        {
                            continue;
                        }
                        for (int j = -1; j <= 1; j++)
                        {
                            if (j == -1 && r == 0)
                            {
                                continue;
                            }
                            if (j == 1 && r == size - 1)
                            {
                                continue;
                            }
                            if (board[c + i, r + j].isMine() == true)
                            {
                                current.incMines();
                            }
                        }
                    }

                }
            }


            filled = true;
        }
    }

    public Tile[,] getBoard()
    {
        return board;
    }
    
    public void setMines(int num)
    {
        mines = num;
    }

    public int clicked(int xpos, int ypos, int input)
    {
        for(int c = 0; c < size; c++)
        {
            for(int r = 0; r < size; r++)
            {
                Tile current = board[c, r];
                if (xpos > current.getX() && ypos > current.getY() && xpos < current.getX() + 25 && ypos < current.getY() + 25)
                {
                    if (input == 2 && current.getSwept() == false)
                    {
                        if (current.getFlag() == true)
                        {
                            current.setSprite(tile);
                            current.setFlag(false);
                            mines++;
                        }
                        else
                        {
                            current.setSprite(flag);
                            current.setFlag(true);
                            mines--;
                        }
                    }
                    else if (input == 3 && current.getSwept() == false)
                    {
                        if (current.isMine())
                        {
                            current.setSprite(null);
                            current.setSwept(true);
                            mines--;
                            return -1;
                        }
                        else
                        {
                            clicked(xpos, ypos, 1);
                            return -2;
                        }
                    }
                    else if (current.isMine() && !current.getSwept())
                    {
                        current.setSwept(true);
                        current.setSprite(mine);
                        mines--;
                        Game1.HP -= 10;
                    }
                    else
                    {
                        if (current.getSwept())
                        {
                            continue;
                        }
                        current.setSwept(true);

                        int charge = 5;
                        switch (current.getAdjMines())
                        {
                            case 0:
                                {
                                    current.setSprite(null);

                                    for (int i = -1; i <= 1; i++)
                                    {
                                        if (i == -1 && c == 0)
                                        {
                                            continue;
                                        }
                                        if (i == 1 && c == size-1)
                                        {
                                            continue;
                                        }
                                        for (int j = -1; j <= 1; j++)
                                        {
                                            if (j == -1 && r == 0)
                                            {
                                                continue;
                                            }
                                            else if (j == 1 && r == size-1)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                charge += clicked(board[c + i, r + j].getX() + 5, board[c + i, r + j].getY() + 5, 1);
                                            } 
                                        }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    current.setSprite(one);
                                    break;
                                }
                            case 2:
                                {
                                    current.setSprite(two);
                                    break;
                                }
                            case 3:
                                {
                                    current.setSprite(three);
                                    break;
                                }
                            case 4:
                                {
                                    current.setSprite(four);
                                    break;
                                }
                            case 5:
                                {
                                    current.setSprite(five);
                                    break;
                                }
                            case 6:
                                {
                                    current.setSprite(six);
                                    break;
                                }
                            case 7:
                                {
                                    current.setSprite(seven);
                                    break;
                                }
                            case 8:
                                {
                                    current.setSprite(eight);
                                    break;
                                }
                        }
                        return charge;
                    }
                }
            }
        }
        return 0;
    }

    public void revealMines(int numMines)
    {
        int trueNumMines = numMines;
        if (mines < numMines)
        {
            trueNumMines = mines;
        }
        for (int i = 0; i < trueNumMines; i++)
        {
            int randMine = rnd.Next(1, mines+1);
            int mineIndex = 0;
            for (int c = 0; c < size; c++)
            {
                for (int r = 0; r < size; r++)
                {
                    if (board[c, r].isMine() && !board[c, r].getSwept() && !board[c, r].getFlag())
                    {
                        mineIndex++;
                        if (mineIndex == randMine)
                        {
                            board[c, r].setSprite(flag);
                            board[c, r].setFlag(true);
                            mines--;
                        }
                    }
                    else if (board[c, r].isMine() && board[c, r].getSwept() && board[c, r].getFlag())
                    {
                        mineIndex++;
                        if (mineIndex == randMine)
                        {
                            i--;
                        }
                    }
                }
            }
        }
    }
}
