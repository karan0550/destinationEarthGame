using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    enum SpaceShipType
    {
        Destroyer,
        Cruiser
    }

    class SpaceShip : DrawableGameComponent, ICollidable
    {
        Texture2D texture;
        int speed;
        SpaceShipType type;
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

        public SpaceShip(Game game, SpaceShipType type, Vector2 position, int speed) 
            : base(game)
        {
            this.type = type;
            this.position = position;
            this.speed = speed;
        }

        protected override void LoadContent()
        {
            if (type == SpaceShipType.Cruiser)
            {
                texture = Game.Content.Load<Texture2D>("Cruiser");
            }

            if (type == SpaceShipType.Destroyer)
            {
                texture = Game.Content.Load<Texture2D>("Destroyer");
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
            FuelMeter fuelMeter = Game.Services.GetService<FuelMeter>();
            if (player != null)
            {
                if (HitBox.Intersects(player.playerHitBox))
                {
                    Game.Components.Remove(fuelMeter);
                    Game.Components.Remove(player);
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
