using Raylib_cs;
using static Game.IAudio;

/// TODO: Adjust Volumes according to sound
namespace Game
{
    internal class Audio : IAudio
    {
        /// <summary>
        /// Current music stream being played
        /// </summary>
        private Music currentMusic;

        public Music GetMusic(MusicEffect song)
        {
            string MusicFileName = "MFXTitleScreen.wav";
            if (song == IAudio.MusicEffect.Puzzle)
                MusicFileName = "MFXPuzzle.wav";
            else if (song == IAudio.MusicEffect.Boss)
                MusicFileName = "MFXBoss.wav";
            else if (song == IAudio.MusicEffect.GameEnd)
                MusicFileName = "MFXGameEnd.wav";
            return Raylib.LoadMusicStream(MusicFileName);
        }

        public void PlaySound(IAudio.SoundEffect soundEffect)
        {
            string SoundFileName = "SFXStep.wav";
            if (soundEffect == IAudio.SoundEffect.PushBlock)
                SoundFileName = "SFXPushBlock.wav";
            else if (soundEffect == IAudio.SoundEffect.Fire)
                SoundFileName = "SFXFire.wav";
            else if (soundEffect == IAudio.SoundEffect.Water)
                SoundFileName = "SFXWater.wav";
            else if (soundEffect == IAudio.SoundEffect.Wall)
                SoundFileName = "SFXWall.wav";
            else if (soundEffect == IAudio.SoundEffect.LevelComplete)
                SoundFileName = "SFXLevelComplete.wav";
            Sound sound = Raylib.LoadSound(SoundFileName);
            Raylib.PlaySound(sound);
        }

        public void UpdateMusic(IAudio.MusicEffect song = MusicEffect.None)
        {
            if (song != MusicEffect.None)
            {
                if(Raylib.IsMusicReady(currentMusic))
                    Raylib.UnloadMusicStream(currentMusic);
                this.currentMusic = GetMusic(song);
            }
            if (!Raylib.IsMusicStreamPlaying(this.currentMusic))
                Raylib.PlayMusicStream(this.currentMusic);
            Raylib.UpdateMusicStream(this.currentMusic);
        }

        public void PauseMusic()
        {
            if (Raylib.IsMusicStreamPlaying(this.currentMusic))
                Raylib.PauseMusicStream(this.currentMusic);
        }
    }
}
