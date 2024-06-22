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

        /// <summary>
        /// Current sound stream being palyed
        /// </summary>
        private Sound currentSound;

        /// <summary>
        /// Aux bool to help pausing and unpausing music
        /// </summary>
        public bool isPaused { get; set; }

        public void PlaySound(IAudio.SoundEffect soundEffect)
        {
            string SoundFileName;
            switch (soundEffect)
            {
                case IAudio.SoundEffect.Step:
                    SoundFileName = "SFXStep.wav";
                    break;
                case IAudio.SoundEffect.PushBlock:
                    SoundFileName = "SFXPushBlock.wav";
                    break;
                case IAudio.SoundEffect.Fire:
                    SoundFileName = "SFXFire.wav";
                    break;
                case IAudio.SoundEffect.Water:
                    SoundFileName = "SFXWater.wav";
                    break;
                case IAudio.SoundEffect.Wall:
                    SoundFileName = "SFXWall.wav";
                    break;
                case IAudio.SoundEffect.LevelComplete:
                    SoundFileName = "SFXLevelComplete.wav";
                    break;
                default:
                    SoundFileName = "SFXStep.wav";
                    break;
            };
            Raylib.UnloadSound(this.currentSound);
            this.currentSound = Raylib.LoadSound(SoundFileName);
            Raylib.PlaySound(this.currentSound);
        }

        /// <summary>
        /// <param>Used to get AudioStream from desired music</param>
        /// </summary>
        /// <param name="music">Music</param>
        /// <returns>AudioStream</returns>
        private Music GetMusic(MusicEffect song)
        {
            string MusicFileName;
            switch (song)
            {
                case IAudio.MusicEffect.TitleScreen:
                    MusicFileName = "MFXTitleScreen.wav";
                    break;
                case IAudio.MusicEffect.Puzzle:
                    MusicFileName = "MFXPuzzle.wav";
                    break;
                case IAudio.MusicEffect.Boss:
                    MusicFileName = "MFXBoss.wav";
                    break;
                case IAudio.MusicEffect.GameEnd:
                    MusicFileName = "MFXGameEnd.wav";
                    break;
                default:
                    MusicFileName = "MFXTitleScreen.wav";
                    break;
            };
            return Raylib.LoadMusicStream(MusicFileName);
        }

        public void UpdateMusic(IAudio.MusicEffect song = MusicEffect.None)
        {
            if (song != MusicEffect.None)
            {
                if(Raylib.IsMusicStreamPlaying(currentMusic))
                    Raylib.UnloadMusicStream(currentMusic);
                this.currentMusic = GetMusic(song);
                this.isPaused = false;
            }
            if (!Raylib.IsMusicStreamPlaying(this.currentMusic) && !this.isPaused)
                Raylib.PlayMusicStream(this.currentMusic);
            Raylib.UpdateMusicStream(this.currentMusic);
        }

        public void PauseMusic()
        {
            if (Raylib.IsMusicStreamPlaying(this.currentMusic))
                Raylib.PauseMusicStream(this.currentMusic);
            this.isPaused = true;
        }
        public void ResumeMusic()
        {
            this.isPaused = false;
        }

        public void StopMusic()
        {
            if (Raylib.IsMusicStreamPlaying(this.currentMusic))
                Raylib.StopMusicStream(this.currentMusic);
            this.isPaused = true;
        }

    }
}
