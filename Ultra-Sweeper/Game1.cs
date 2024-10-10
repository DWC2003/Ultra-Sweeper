using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Ultra_Sweeper
{
    public class Game1 : Game
    {
        Board board;
        Player player;

        int cooldown = 0;
        int spawnDelay = 0;
        int spawnRate = 400;
        int movementRate = 0;
        int boardsCleared = 0;
        int timer = 0;
        int difficultylvl = 1;
        int bombs = 0;
        Random rnd = new Random();

        Upgrade[] upgradeSelection = new Upgrade[3];
        Upgrade[] skillSelection = new Upgrade[3];
        Upgrade[] weaponSelection = new Upgrade[3];
        List<String> possibleUpgrades = new List<String>();
        List<String> possibleSkills = new List<String>();
        List<String> possibleWeapons = new List<String>();

        List<Projectile> projectiles = new List<Projectile>();
        List<Explosion> explosions = new List<Explosion>();
        List<Weapon> weapons = new List<Weapon>();

        int size;
        Vector2 aim;

        bool mainMenu = true;
        bool gameActive = false;
        bool pauseActive = false;
        bool lvlUpActive = false;
        int shift = 0;

        List<Enemy> robot;
        Texture2D explosion;
        Texture2D background;
        Texture2D projectile;
        Texture2D pause;
        Texture2D robotEnemy;
        Texture2D gun;
        Texture2D border;
        Texture2D tile;
        Texture2D mine;
        Texture2D flag;
        Texture2D one;
        Texture2D two;
        Texture2D three;
        Texture2D four;
        Texture2D five;
        Texture2D six;
        Texture2D seven;
        Texture2D eight;
        Texture2D title;

        SpriteFont statFont;
        SpriteFont titleFont;
        SpriteFont weaponFont;
        public static int HP = 100;
        public static int Score = 0;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void levelUP(int index)
        {
            if (boardsCleared % 3 == 0)
            {
                if (shift == 0)
                {
                    switch (skillSelection[index].getName())
                    {
                        case "Defusal":
                            player.setDefuseSkill(true);
                            break;
                        case "Observation":
                            player.setObservationSkill(true);
                            break;
                    }
                }
                while (boardsCleared % 3 == 0 || boardsCleared % 2 == 0)
                {
                    boardsCleared--;
                    shift++;
                }
            }
            else if (boardsCleared % 2 == 0)
            {
                if (shift == 0)
                {
                    switch (weaponSelection[index].getName())
                    {
                        case "Explosive":
                            if (weapons.Count < 3)
                            {
                                weapons.Add(new Weapon(new Vector2(100 + (150 * weapons.Count - 1), 500), 0, "Explosive"));
                            }
                            else
                            {
                                weapons.Add(new Weapon(new Vector2(625 + (150 * weapons.Count - 1), 500), 0, "Explosive"));

                            }
                            break;
                        case "Shotgun":
                            if (weapons.Count < 3)
                            {
                                weapons.Add(new Weapon(new Vector2(100 + (150 * weapons.Count - 1), 500), 0, "Shotgun"));

                            }
                            else
                            {
                                weapons.Add(new Weapon(new Vector2(625 + (150 * weapons.Count - 1), 500), 0, "Shotgun"));

                            }
                            break;
                        case "Gun":
                            if (weapons.Count < 3)
                            {
                                weapons.Add(new Weapon(new Vector2(100 + (150 * weapons.Count - 1), 500), 0, "Gun"));

                            }
                            else
                            {
                                weapons.Add(new Weapon(new Vector2(625 + (150 * weapons.Count - 1), 500), 0, "Gun"));

                            }
                            break;
                    }
                }
                while (boardsCleared % 2 == 0 || boardsCleared % 3 == 0)
                {
                    boardsCleared--;
                    shift++;
                }
                
            }
            else if (boardsCleared % 3 != 0 && boardsCleared % 2 != 0)
            {
                switch (upgradeSelection[index].getName())
                {
                    case "Fire Rate UP":
                        weapons[upgradeSelection[index].getWeaponNum()].incFireRate(5);
                        break;
                    case "Damage UP":
                        weapons[upgradeSelection[index].getWeaponNum()].incDamage(5);
                        break;
                    case "Charge Rate UP":
                        weapons[upgradeSelection[index].getWeaponNum()].incChargeAdd(1);
                        break;
                }
                while (shift > 0)
                {
                    boardsCleared++;
                    shift--;
                }
                lvlUpActive = false;
                gameActive = true;
            }
            
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            size = 20;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player();

            background = Content.Load<Texture2D>("altbackground");
            explosion = Content.Load<Texture2D>("explosion");
            tile = Content.Load<Texture2D>("tile");
            title = Content.Load<Texture2D>("title");
            mine = Content.Load<Texture2D>("mine");
            projectile = Content.Load<Texture2D>("projectile");
            gun = Content.Load<Texture2D>("gun");
            flag = Content.Load<Texture2D>("flagontile");
            one = Content.Load<Texture2D>("one");
            two = Content.Load<Texture2D>("two");
            three = Content.Load<Texture2D>("three");
            four = Content.Load<Texture2D>("four");
            five = Content.Load<Texture2D>("five");
            six = Content.Load<Texture2D>("six");
            seven = Content.Load<Texture2D>("seven");
            eight = Content.Load<Texture2D>("eight");
            title = Content.Load<Texture2D>("title");
            robotEnemy = Content.Load<Texture2D>("robotenemy");
            pause = Content.Load<Texture2D>("pause");

            border = new Texture2D(GraphicsDevice, 1, 1);
            border.SetData(new Color[] { Color.Black });

            statFont = Content.Load<SpriteFont>("HP");
            titleFont = Content.Load<SpriteFont>("titlefont");
            weaponFont = Content.Load<SpriteFont>("weaponstat");

            weapons.Add(new Weapon(new Vector2(100, 500), 0, "Gun"));
            weapons.Add(new Weapon(new Vector2(250, 500), 0, "Gun"));

            possibleUpgrades.AddRange(new List<String>
            {
                "Fire Rate UP",
                "Charge Rate UP",
                "Damage UP",
            });

            possibleSkills.AddRange(new List<String>
            {
                "Defusal",
                "Observation"
            });

            possibleWeapons.AddRange(new List<String>
            {
                "Explosive",
                "Shotgun",
                "Gun",
            });

            robot = new List<Enemy>();

            board = new Board(size, tile, mine, flag, one, two, three, four, five, six, seven, eight);

        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            /*
            if (HP <= 0)
            {
                mainMenu = true;
                gameActive = false;
                player.setDefuseSkill(false);
                player.setObservationSkill(false);
                timer = 0;
                aim = new Vector2(0, 0);
                for (int i = 0; i < weapons.Count; i++)
                {
                    weapons[i].setCharge(0);
                }
                board = new Board(size, tile, mine, flag, one, two, three, four, five, six, seven, eight);
                robot.Clear();
                Score = 0;
                HP = 100;
            }
            */
            if (gameActive)
            {
                timer++;
                difficultylvl = 1 + timer / 5000;

                if (spawnDelay == 0)
                {
                    if (robot.Count < 20)
                    {
                        robot.Add(new Enemy(80 + 15*(difficultylvl-1)));
                    }
                    spawnDelay = spawnRate;
                }

                if (movementRate == 0)
                {
                    for (int i = 0; i < robot.Count; i++)
                    {
                        HP -= robot[i].Update();
                    }
                    movementRate = 15;
                }

                for (int i = 0; i < robot.Count; i++)
                {
                    if (robot[i].GetHP() <= 0)
                    {
                        robot.RemoveAt(i);
                        Score++;
                    }                   
                }

                if (robot.Count > 0)
                {
                    aim = new Vector2(robot[0].getX() + 35, robot[0].getY() + 50);
                }

                for (int i = 0; i < weapons.Count; i++)
                {
                    if (weapons[i].getCharge() >= 10 && robot.Count > 0)
                    {
                        switch (weapons[i].getType())
                        {
                            case "Gun":
                                if (weapons[i].depleteCharge(10))
                                {
                                    projectiles.Add(new Projectile(false, weapons[i].getDamage(), weapons[i].getGunPos(), new Vector2((float)Math.Cos((float)Math.Atan2(aim.Y - weapons[i].getGunPos().Y, aim.X - weapons[i].getGunPos().X)), (float)Math.Sin((float)Math.Atan2(aim.Y - weapons[i].getGunPos().Y, aim.X - weapons[i].getGunPos().X)))));
                                }
                                break;
                            case "Explosive":
                                if (weapons[i].getCharge() >= 20)
                                {
                                    if (weapons[i].depleteCharge(20))
                                    {
                                        projectiles.Add(new Projectile(true, weapons[i].getDamage(), weapons[i].getGunPos(), new Vector2((float)Math.Cos((float)Math.Atan2(aim.Y - weapons[i].getGunPos().Y, aim.X - weapons[i].getGunPos().X)), (float)Math.Sin((float)Math.Atan2(aim.Y - weapons[i].getGunPos().Y, aim.X - weapons[i].getGunPos().X)))));
                                    }
                                }
                                break;
                        }
                        
                    }
                }

                int charge = 0;
                if (mouseState.LeftButton == ButtonState.Pressed && cooldown == 0)
                {
                    charge = board.clicked(mouseState.X, mouseState.Y, 1);
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        if (charge > 0)
                        {
                            if (charge > 50)
                            {
                                weapons[i].chargeUp(50 + (weapons[i].getChargeAdd()*10));
                            }
                            else
                            {
                                weapons[i].chargeUp(charge);
                            }

                        }
                    }
                    charge = 0;
                    cooldown = 10;
                }

                for (int i = 0; i <  projectiles.Count; i++)
                {
                    projectiles[i].Update();
                    if (projectiles[i].getPos().X > 1500 || projectiles[i].getPos().X < 0 || projectiles[i].getPos().Y < 0)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                        continue;

                    }
                    if (projectiles[i].isUsed())
                    {
                        continue;
                    }
                    for (int j = 0; j < robot.Count; j++)
                    {
                        Projectile projHitBox = projectiles[i];

                        if (projHitBox.getPos().X > robot[j].getX() && projHitBox.getPos().X < robot[j].getX() + 75 && projHitBox.getPos().Y > robot[j].getY() && projHitBox.getPos().Y < robot[j].getY() + 100){
                            robot[j].lowerHP(projHitBox.getDamage());
                            if (projectiles[i].isExplosive())
                            {
                                explosions.Add(new Explosion(30, 40, explosion, projectiles[i].getPos()));
                            }
                            projectiles[i].setUsed(true);
                        }
                    }
                }

                player.Update();
                if (player.getDefuseSkill())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D) && cooldown == 0 && player.getDefuseCooldown() == 0 && bombs <= 3)
                    {
                        int defuse = board.clicked(mouseState.X, mouseState.Y, 3);
                        if (defuse == -1)
                        {
                            player.setDefuseCooldown(1000);
                            bombs++;

                        }
                        else if (defuse == -2)
                        {
                            player.setDefuseCooldown(1000);
                        }
                        cooldown = 10;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.B) && cooldown == 0 && bombs > 0 && mouseState.Y < 450)
                    {
                        cooldown = 10;
                        explosions.Add(new Explosion(60, 50, explosion, new Vector2(mouseState.X, mouseState.Y)));
                        bombs--;
                    }
                }
                if (player.getObservationSkill())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.R) && cooldown == 0 && player.getObservationCooldown() == 0)
                    {
                        board.revealMines(5);
                        player.setObservationCooldown(1500);
                        cooldown = 10;
                    }
                }

                if (explosions.Count > 0)
                {
                    for (int i = 0; i < explosions.Count; i++)
                    {
                        explosions[i].Update();
                        if (explosions[i].getActiveTime() > 0 && !explosions[i].isExploded())
                        {
                            for (int j = 0; j < robot.Count; j++)
                            {
                                if (robot[j].getX() + 75/2 > explosions[i].getPos().X - 100 && robot[j].getX() + 75/2 < explosions[i].getPos().X + 100 && robot[j].getY() + 50 > explosions[i].getPos().Y - 100 && robot[j].getY() + 50 < explosions[i].getPos().Y + 100)
                                {
                                    robot[j].lowerHP(explosions[i].getDmg());
                                }
                            }
                            explosions[i].setExploded(true);
                        }
                        else if (explosions[i].getActiveTime() == 0)
                        {
                            explosions.RemoveAt(i);
                        }
                    }
                }

                if (board.IsCleared())
                {
                    board.Clear();
                    board.Refill();
                    board.setMines(50);
                    boardsCleared++;
                    lvlUpActive = true;
                    gameActive = false;
                    HP = 100;
                    spawnRate -= 25;
                    for (int i = 0; i < 3; i++)
                    {
                        skillSelection[i] = new Upgrade(0, possibleSkills[rnd.Next(possibleSkills.Count)]);
                        upgradeSelection[i] = new Upgrade(rnd.Next(weapons.Count), possibleUpgrades[rnd.Next(possibleUpgrades.Count)]);
                        weaponSelection[i] = new Upgrade(rnd.Next(weapons.Count), possibleWeapons[rnd.Next(possibleWeapons.Count)]);
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S) && cooldown == 0)
                {
                    board.Clear();
                    board.Refill();
                    board.setMines(50);
                    boardsCleared++;
                    lvlUpActive = true;
                    gameActive = false;
                    spawnRate -= 5;
                    for (int i = 0; i < 3; i++)
                    {
                        skillSelection[i] = new Upgrade(0, possibleSkills[rnd.Next(possibleSkills.Count)]);
                        upgradeSelection[i] = new Upgrade(rnd.Next(weapons.Count), possibleUpgrades[rnd.Next(possibleUpgrades.Count)]);
                        weaponSelection[i] = new Upgrade(rnd.Next(weapons.Count), possibleWeapons[rnd.Next(possibleWeapons.Count)]);

                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && cooldown == 0)
                {
                    pauseActive = true;
                    gameActive = false;
                    cooldown = 10;
                }

                if (movementRate > 0)
                {
                    movementRate--;
                }
                if (spawnDelay > 0)
                {
                    spawnDelay--;
                }

            }

            if (pauseActive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && cooldown == 0)
                {
                    pauseActive = false;
                    gameActive = true;
                    cooldown = 10;
                }
            }         

            if (lvlUpActive)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && cooldown == 0)
                {
                    if (mouseState.X > 175 && mouseState.X < 475 && mouseState.Y > 300 && mouseState.Y < 600)
                    {
                        levelUP(0);
                    }
                    if (mouseState.X > 600 && mouseState.X < 900 && mouseState.Y > 300 && mouseState.Y < 600)
                    {
                        levelUP(1);
                    }
                    if (mouseState.X > 1000 && mouseState.X < 1300 && mouseState.Y > 300 && mouseState.Y < 600)
                    {
                        levelUP(2);
                    }
                    cooldown = 10;
                }

            }
            
            if (cooldown > 0)
            {
                cooldown--;
            }
            
            if (kstate.IsKeyDown(Keys.C) && gameActive)
            {
                board.Clear();
            }

            if (kstate.IsKeyDown(Keys.Space) && cooldown == 0)
            {
                if (gameActive)
                {
                    board.Refill();
                    cooldown = 20;
                }
                else
                {
                    mainMenu = false;
                    gameActive = true;
                    cooldown = 20;
                }
            }

            

            if (mouseState.RightButton == ButtonState.Pressed && cooldown == 0 && gameActive)
            {
                board.clicked(mouseState.X, mouseState.Y, 2);
                cooldown = 20;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, 1500, 1000), Color.White);
            _spriteBatch.End();

            if (mainMenu)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(title, new Rectangle(-30, -75, 1500, 1000), Color.White);
                _spriteBatch.Draw(border, new Rectangle(0, 450, 1500, 50), Color.Black);

                _spriteBatch.Draw(tile, new Rectangle(0, 0, 100, 100), Color.White);
                _spriteBatch.Draw(tile, new Rectangle(1400, 0, 100, 100), Color.White);
                _spriteBatch.Draw(tile, new Rectangle(0, 900, 100, 100), Color.White);
                _spriteBatch.Draw(tile, new Rectangle(1400, 900, 100, 100), Color.White);

                _spriteBatch.End();
            }

            if (lvlUpActive)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(statFont, "  Board Cleared!\nSelect an upgrade:", new Vector2(630, 0), Color.Black);
                _spriteBatch.Draw(tile, new Rectangle(175, 300, 300, 300), Color.White);
                _spriteBatch.Draw(tile, new Rectangle(600, 300, 300, 300), Color.White);
                _spriteBatch.Draw(tile, new Rectangle(1000, 300, 300, 300), Color.White);

                if (boardsCleared % 3 == 0 && boardsCleared % 2 == 0)
                {
                    _spriteBatch.DrawString(statFont, skillSelection[0].getName() + "\nSKILL", new Vector2(225, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, skillSelection[1].getName() + "\nSKILL", new Vector2(650, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, skillSelection[2].getName() + "\nSKILL", new Vector2(1050, 400), Color.Black);
                }
                else if(boardsCleared % 3 == 0)
                {
                    _spriteBatch.DrawString(statFont, skillSelection[0].getName() + "\nSKILL", new Vector2(225, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, skillSelection[1].getName() + "\nSKILL", new Vector2(650, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, skillSelection[2].getName() + "\nSKILL", new Vector2(1050, 400), Color.Black);
                }
                else if(boardsCleared % 2 == 0) {
                    _spriteBatch.DrawString(statFont, weaponSelection[0].getName() + "\nWEAPON", new Vector2(225, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, weaponSelection[1].getName() + "\nWEAPON", new Vector2(650, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, weaponSelection[2].getName() + "\nWEAPON", new Vector2(1050, 400), Color.Black);
                }
                else
                {
                    _spriteBatch.DrawString(statFont, upgradeSelection[0].getName() + "\nWeapon: " + (upgradeSelection[0].getWeaponNum() + 1), new Vector2(225, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, upgradeSelection[1].getName() + "\nWeapon: " + (upgradeSelection[1].getWeaponNum() + 1), new Vector2(650, 400), Color.Black);
                    _spriteBatch.DrawString(statFont, upgradeSelection[2].getName() + "\nWeapon: " + (upgradeSelection[2].getWeaponNum() + 1), new Vector2(1050, 400), Color.Black);
                    _spriteBatch.DrawString(weaponFont, "Charge: " + weapons[upgradeSelection[0].getWeaponNum()].getCharge() + "\nFire Rate: " + ((double)(Math.Round((double)100 / weapons[upgradeSelection[0].getWeaponNum()].getFireRate(), 2))) + "bps\nDamage: " + weapons[upgradeSelection[0].getWeaponNum()].getDamage() + "\nType: " + weapons[upgradeSelection[0].getWeaponNum()].getType(), new Vector2(275, 600), Color.Black);
                    _spriteBatch.DrawString(weaponFont, "Charge: " + weapons[upgradeSelection[1].getWeaponNum()].getCharge() + "\nFire Rate: " + ((double)(Math.Round((double)100 / weapons[upgradeSelection[1].getWeaponNum()].getFireRate(), 2))) + "bps\nDamage: " + weapons[upgradeSelection[1].getWeaponNum()].getDamage() + "\nType: " + weapons[upgradeSelection[1].getWeaponNum()].getType(), new Vector2(700, 600), Color.Black);
                    _spriteBatch.DrawString(weaponFont, "Charge: " + weapons[upgradeSelection[2].getWeaponNum()].getCharge() + "\nFire Rate: " + ((double)(Math.Round((double)100 / weapons[upgradeSelection[2].getWeaponNum()].getFireRate(), 2))) + "bps\nDamage: " + weapons[upgradeSelection[2].getWeaponNum()].getDamage() + "\nType: " + weapons[upgradeSelection[2].getWeaponNum()].getType(), new Vector2(1100, 600), Color.Black);
                }



                _spriteBatch.End();
            }

            if (gameActive || pauseActive)
            {
                _spriteBatch.Begin();
                if (!pauseActive)
                {
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (board.getBoard()[i, j].getSprite() == null)
                            {
                                continue;
                            }
                            _spriteBatch.Draw(board.getBoard()[i, j].getSprite(), new Rectangle(i * 25 + 500, j * 25 + 500, 25, 25), Color.White);

                        }
                    }
                }
                else
                {
                    _spriteBatch.Draw(pause, new Rectangle(500, 500, 500, 500), Color.White);
                }

                for (int i = 0; i < robot.Count; i++)
                {
                    _spriteBatch.Draw(robotEnemy, new Rectangle(robot[i].getX(), robot[i].getY(), 75, 100), Color.White);

                }

                if (player.getDefuseSkill())
                {
                    _spriteBatch.DrawString(statFont, "Defuse Cooldown: " + player.getDefuseCooldown() + "\nBomb Charges: " + bombs, new Vector2(1050, 800), Color.Black);
                }

                if (player.getObservationSkill())
                {
                    _spriteBatch.DrawString(statFont, "Observation Cooldown: " + player.getObservationCooldown(), new Vector2(80, 800), Color.Black);

                }

                for (int i = 0; i < projectiles.Count; i++)
                {
                    _spriteBatch.Draw(
                    projectile,
                    projectiles[i].getPos(),
                    null,
                    Color.White,
                    (float)Math.Atan2(projectiles[i].getStartPos().Y - projectiles[i].getPos().Y, projectiles[i].getStartPos().X - projectiles[i].getPos().X) + MathHelper.ToRadians(95),
                    new Vector2(projectile.Width / 2, projectile.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                }

                for (int i = 0; i < explosions.Count; i++)
                {
                    _spriteBatch.Draw(explosions[i].getSprite(), new Rectangle((int)explosions[i].getPos().X - 100, (int)explosions[i].getPos().Y - 100, 200, 200), Color.White); 

                }

                _spriteBatch.Draw(border, new Rectangle(0, 450, 1500, 50), Color.Black);
                for (int i = 0; i < weapons.Count; i++)
                {
                    _spriteBatch.Draw(
                        gun,
                        weapons[i].getGunPos(),
                        null,
                        Color.White,
                        (float)Math.Atan2(aim.Y - weapons[i].getGunPos().Y, aim.X - weapons[i].getGunPos().X) + MathHelper.ToRadians(95),
                        new Vector2(gun.Width / 2, gun.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0f
                        );         
                    _spriteBatch.DrawString(weaponFont, "Charge: " + weapons[i].getCharge() + "\nFire Rate: " + ((double)(Math.Round((double)100/weapons[i].getFireRate(), 2))) + "bps\nDamage: " + weapons[i].getDamage() + "\nType: " + weapons[i].getType(), new Vector2(weapons[i].getGunPos().X - 75, weapons[i].getGunPos().Y + 100), Color.Black);
                }
                _spriteBatch.DrawString(statFont, "HP: " + HP + "   Board: " + (boardsCleared + 1) + "   Score: " + Score, new Vector2(550, 460), Color.White);
                _spriteBatch.DrawString(statFont, "  Timer: " + (timer/100) + "\nDifficulty: " + difficultylvl, new Vector2(680, 0), Color.Black);

                _spriteBatch.End();
            }
            

            base.Draw(gameTime);
        }

    }
}
