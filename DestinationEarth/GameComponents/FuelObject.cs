using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    class FuelObject : DrawableGameComponent, ICollidable
    {
        const int SPEED = 3;
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

        public FuelObject(Game game, Vector2 position) 
            : base(game)
        {
            this.position = position;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("sprite_asteroid_2");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= SPEED;
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
            FuelMeter fuelMeter = Game.Services.GetService<FuelMeter>();

            if (player != null && fuelMeter != null)
            {
                if (HitBox.Intersects(player.playerHitBox))
                {
                    fuelMeter.fuelAmount++;
                    Game.Components.Remove(this);
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
