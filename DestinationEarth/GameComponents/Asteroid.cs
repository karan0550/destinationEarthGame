using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.MenuComponents;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    public enum AsteroidType
    {
        Brown,
        Gray,
        Dark,
        Square,
    }

    public class Asteroid : DrawableGameComponent, ICollidable
    {
        Texture2D texture;
        int speed;
        AsteroidType type;
        public Vector2 position;

        public Rectangle HitBox
        {
            get
            {
                Rectangle hitBox = texture.Bounds;
                hitBox.Location = position.ToPoint();
                return hitBox;
            }
        }

        public Asteroid(Game game, AsteroidType type, Vector2 position, int speed) 
            : base(game)
        {
            this.type = type;
            this.position = position;
            this.speed = speed;
        }

        protected override void LoadContent()
        {
            if (type == AsteroidType.Brown)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_brown");
            }

            if (type == AsteroidType.Gray)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_dark");
            }

            if (type == AsteroidType.Dark)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_gray");
            }

            if (type == AsteroidType.Square)
            {
                texture = Game.Content.Load<Texture2D>("asteroid_square");
            }
            

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= speed;
            CheckForCollision();
            RemoveOffScreenObjects();
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
                    Game.Components.Add(new Explosion(Game, player.playerHitBox.Center.ToVector2()));                  
                }
            }
        }

        public void RemoveOffScreenObjects()
        {
            if (position.X + texture.Width < 0)
            {
                Game.Components.Remove(this);
            }
        }
    }
}
