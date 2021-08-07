using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.Scenes
{
    public class LevelOneScene : GameScene
    {
        public LevelOneScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            this.AddComponent(new Background(Game, "SpaceBg", 1));
            this.AddComponent(new SpaceObjectGenerator(Game, this));

            FuelMeter fuelMeter = new FuelMeter(Game);
            this.AddComponent(fuelMeter);
            Game.Services.AddService<FuelMeter>(fuelMeter);

            Player player = new Player(Game);
            this.AddComponent(player);
            Game.Services.AddService<Player>(player);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.E))
            {
                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<MenuScene>().Show();
            }

            base.Update(gameTime);
        }
    }
}
