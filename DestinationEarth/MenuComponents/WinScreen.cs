using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.GameComponents
{
    class WinScreen : DrawableGameComponent
    {
        SpriteFont regularFont;
        Color regularColor = Color.White;
        Vector2 position;

        string winMessage;

        int playerScore;


        public WinScreen(Game game) 
            : base(game)
        {           
            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 2);
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("menuFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            playerScore = Game.Services.GetService<FuelMeter>().fuelAmount;

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Enter))
            {
                // Update high scores database
                // Reset Game
                // Go to high score area in the menu
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            winMessage = $"Congratulations! You win!\nScore: {playerScore}";

            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.DrawString(regularFont, winMessage, position, regularColor);
            sb.End();

            base.Draw(gameTime);
        }


    }
}
