using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DestinationEarth
{
    public enum PlayerState
    {
        Flying,
        Crashing
    }

    class Player : DrawableGameComponent
    {
        const int SPEED = 4;
        const double FRAME_DURATION = 0.1;

        List<Texture2D> textures;
        Vector2 position;

        int currentFrame;
        double frameTimer;

        public Rectangle playerHitBox
        {
            get
            {
                Rectangle hitBox = textures[currentFrame].Bounds;
                hitBox.Location = position.ToPoint();
                return hitBox;
            }
        }

        public Player(Game game) 
            : base(game)
        {
            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 4,
                Game.GraphicsDevice.Viewport.Height / 2);
            textures = new List<Texture2D>();
        }

        protected override void LoadContent()
        {
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0001"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0002"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0003"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0004"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0005"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0006"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0007"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0008"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0009"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0010"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0011"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0012"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0013"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0014"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0015"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0016"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0017"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0018"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0019"));
            textures.Add(Game.Content.Load<Texture2D>("smallfighter0020"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y -= SPEED;
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                position.Y += SPEED;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= SPEED;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                position.X += SPEED;
            }

            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                currentFrame++;
                frameTimer = 0;
            }

            if (currentFrame >= textures.Count)
            {
                currentFrame = 0;
            }

            position.X = MathHelper.Clamp(position.X, 0, Game.GraphicsDevice.Viewport.Width - textures[currentFrame].Width);
            position.Y = MathHelper.Clamp(position.Y, 0, Game.GraphicsDevice.Viewport.Height - textures[currentFrame].Height);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(textures[currentFrame], position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

    }
}
