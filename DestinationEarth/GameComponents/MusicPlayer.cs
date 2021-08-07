using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DestinationEarth.GameComponents
{
    public class MusicPlayer : GameComponent
    {
        const float VOLUME = 0.3f;

        Song gameSong;
        Song menuSong;


        public MusicPlayer(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            menuSong = Game.Content.Load<Song>("menusong");
            gameSong = Game.Content.Load<Song>("ingamesong");

            base.Initialize();
        }

        public void PlayMenuSong()
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = VOLUME;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(menuSong);
        }

        public void PlayGameSong()
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = VOLUME;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(gameSong);
        }
    }
}
