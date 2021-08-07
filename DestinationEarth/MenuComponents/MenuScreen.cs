using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.GameComponents;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DestinationEarth.MenuComponents
{
    enum MenuSelection
    {
        StartGame,
        Help,
        About,
        Quit
    }
    class MenuScreen : DrawableGameComponent
    {
        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int selectedIndex;
        private Vector2 startingPosition;

        private Color regularColor = Color.White;
        private Color highlightColor = Color.Orange;

        private KeyboardState prevKS;

        public MenuScreen(Game game) : base(game)
        {
            menuItems = new List<string>
            {
                "Start Game",
                "Help",
                "About",
                "Quit"
            };
            startingPosition = new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 2);
            prevKS = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("menuFont");
            highlightFont = Game.Content.Load<SpriteFont>("selectedItemFont");

            Game.Services.GetService<MusicPlayer>().PlayMenuSong();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && prevKS.IsKeyUp(Keys.Down))
            {
                selectedIndex++;

                if (selectedIndex >= menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            else if (ks.IsKeyDown(Keys.Up) && prevKS.IsKeyUp(Keys.Up))
            {
                selectedIndex--;

                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            else if (ks.IsKeyDown(Keys.Enter) && prevKS.IsKeyUp(Keys.Enter))
            {
                SwitchScenes();
            }

            prevKS = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            Vector2 nextPosition = startingPosition;

            sb.Begin();
            for (int menuItem = 0; menuItem < menuItems.Count; menuItem++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                if (menuItem == selectedIndex)
                {
                    activeFont = highlightFont;
                    activeColor = highlightColor;
                }

                sb.DrawString(activeFont, menuItems[menuItem], nextPosition, activeColor);

                nextPosition.Y += regularFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }

        private void SwitchScenes()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)selectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<MusicPlayer>().PlayGameSong();
                    Game.Services.GetService<LevelOneScene>().Show();
                    break;

                case MenuSelection.Quit:
                    Game.Exit();
                    break;

                default:
                    Game.Services.GetService<MusicPlayer>().PlayMenuSong();
                    Game.Services.GetService<MenuScene>().Show();
                    break;

            }
        }
    }
}
