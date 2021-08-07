using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    class Explosion : DrawableGameComponent
    {
        const int WIDTH = 96;
        const int HEIGHT = 96;
        const float VOLUME = 0.2f;
        const float FRAME_DURATION = 0.1f;

        static Texture2D texture;
        static List<Rectangle> sourceRects;

        static SoundEffect explosionSFX;
        Vector2 position;

        double explosionTimer;

        int currentFrame;
        double frameTimer;

        public Explosion(Game game, Vector2 position) 
            : base(game)
        {
            this.position = position;
            this.explosionTimer = 0;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Explosion");
            sourceRects = new List<Rectangle>();
            int explosionFrames = 12;

            for (int frame = 0; frame < explosionFrames; frame++)
            {
                sourceRects.Add(new Rectangle(frame * WIDTH, 0, WIDTH, HEIGHT));
            }

            explosionSFX = Game.Content.Load<SoundEffect>("Chunky Explosion");

            explosionSFX.Play(VOLUME, 0, 0);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                currentFrame++;
                frameTimer = 0;
            }

            if (currentFrame >= sourceRects.Count)
            {
                Game.Components.Remove(this);
                ResetLevel();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(texture, position, sourceRects[currentFrame], Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        private void ResetLevel()
        {
            Player player = Game.Services.GetService<Player>();
            FuelMeter fuelMeter = Game.Services.GetService<FuelMeter>();

            if (player != null)
            {
                Game.Services.RemoveService(player.GetType());
                player = null;
            }
            
            Game.Services.RemoveService(fuelMeter.GetType());           
            fuelMeter = null;

            ((Game1)Game).Reset(Game.Services.GetService<LevelOneScene>());
            Game.Services.GetService<GameOverScene>().Show();
        }
    }
}
