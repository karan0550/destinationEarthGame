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
    class FuelMeter : DrawableGameComponent
    {
        const int FUEL_INTERVAL = 5;
        const int STARTING_FUEL_AMOUNT = 10;

        SpriteFont font;
        Vector2 position;
        public int fuelAmount;
        double fuelTimer;

        public FuelMeter(Game game) : base(game)
        {
            position = Vector2.Zero;
            fuelAmount = STARTING_FUEL_AMOUNT;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("fuelmeter");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            fuelTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (fuelTimer >= FUEL_INTERVAL)
            {
                fuelAmount--;
                fuelTimer = 0;
            }

            if (fuelAmount == 0)
            {
                Player player = Game.Services.GetService<Player>();
                Game.Components.Remove(player);
                Game.Components.Add(new Explosion(Game, player.playerHitBox.Center.ToVector2()));
                Game.Services.RemoveService(player.GetType());
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.DrawString(font, $"Fuel Meter: {fuelAmount} Litres", position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
