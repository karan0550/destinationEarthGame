using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.GameComponents
{
    class Planet : DrawableGameComponent, ICollidable
    {
        const int SPEED = 2;

        Texture2D texture;
        Vector2 position;

        public Rectangle HitBox
        {
            get
            {
                Rectangle hitBox = texture.Bounds;
                hitBox.Location = position.ToPoint();
                return hitBox;
            }
        }

        public Planet(Game game) 
            : base(game)
        {
            this.position = new Vector2(Game.GraphicsDevice.Viewport.Width, 0);
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("planet2");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= SPEED;
            CheckForCollision();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(texture, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        public void CheckForCollision()
        {
            Player player = Game.Services.GetService<Player>();
            if (player != null)
            {
                if (HitBox.Intersects(player.playerHitBox))
                {
                    Game.Components.Remove(player);
                    Game.Services.RemoveService(player.GetType());

                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<WinScene>().Show();
                }
            }
        }

        public void RemoveOffScreenObjects()
        {
            
        }
    }
}
