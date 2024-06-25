using Raylib_cs;
using static Jogo.Systems.IAudio;

/// TODO: Adjust Volumes according to sound
namespace Jogo.Systems
{
    internal class Audio : IAudio
    {
        /// <summary>
        /// Current music stream being played
        /// </summary>
        private Music currentMusic;

        /// <summary>
        /// Current sound stream being played
        /// </summary>
        private Sound currentSound;

        /// <summary>
        /// Aux bool to help pausing and unpausing music
        /// </summary>
        public bool isPaused { get; set; }

        public void PlaySound(SoundEffect soundEffect)
        {
            string SoundFileName;
            string path = "Assets/Audio/";
            switch (soundEffect)
            {
                case SoundEffect.Step:
                    SoundFileName = "sfx-step.wav";
                    break;
                case SoundEffect.PushBlock:
                    SoundFileName = "sfx-push-block.wav";
                    break;
                case SoundEffect.Fire:
                    SoundFileName = "sfx-fire.wav";
                    break;
                case SoundEffect.Water:
                    SoundFileName = "sfx-water.wav";
                    break;
                case SoundEffect.Wall:
                    SoundFileName = "sfx-wall.wav";
                    break;
                case SoundEffect.LevelComplete:
                    SoundFileName = "sfx-level-complete.wav";
                    break;
                case SoundEffect.Key:
                    SoundFileName = "sfx-key.wav";
                    break;
                default:
                    SoundFileName = "sfx-step.wav";
                    break;
            };
            Raylib.UnloadSound(currentSound);
            currentSound = Raylib.LoadSound(path + SoundFileName);
            Raylib.PlaySound(currentSound);
        }

        public static void PlaySound(SoundEffect soundEffect, bool noClass)
        {
            string SoundFileName;
            string path = "Assets/Audio/";
            switch (soundEffect)
            {
                case SoundEffect.Step:
                    SoundFileName = "sfx-step.wav";
                    break;
                case SoundEffect.PushBlock:
                    SoundFileName = "sfx-push-block.wav";
                    break;
                case SoundEffect.Fire:
                    SoundFileName = "sfx-fire.wav";
                    break;
                case SoundEffect.Water:
                    SoundFileName = "sfx-water.wav";
                    break;
                case SoundEffect.Wall:
                    SoundFileName = "sfx-wall.wav";
                    break;
                case SoundEffect.LevelComplete:
                    SoundFileName = "sfx-level-complete.wav";
                    break;
                default:
                    SoundFileName = "sfx-step.wav";
                    break;
            };
            Sound sound = Raylib.LoadSound(path + SoundFileName);
            Raylib.PlaySound(sound);
        }

        /// <summary>
        /// <param>Used to get AudioStream from desired music</param>
        /// </summary>
        /// <param name="music">Music</param>
        /// <returns>AudioStream</returns>
        private Music GetMusic(MusicEffect song)
        {
            string MusicFileName;
            string path = "Assets/Audio/";
            switch (song)
            {
                case MusicEffect.TitleScreen:
                    MusicFileName = "mfx-title-screen.wav";
                    break;
                case MusicEffect.Puzzle:
                    MusicFileName = "mfx-puzzle.wav";
                    break;
                case MusicEffect.Boss:
                    MusicFileName = "mfx-boss.wav";
                    break;
                case MusicEffect.GameEnd:
                    MusicFileName = "mfx-game-end.wav";
                    break;
                default:
                    MusicFileName = "mfx-title-screen.wav";
                    break;
            };
            return Raylib.LoadMusicStream(path + MusicFileName);
        }

        public void Update()
        {
            Raylib.UpdateMusicStream(this.currentMusic);
        }

        public void PlayMusic(MusicEffect song)
        {
            if (Raylib.IsMusicStreamPlaying(this.currentMusic))
                Raylib.UnloadMusicStream(this.currentMusic);
            this.currentMusic = GetMusic(song);
            if (!Raylib.IsMusicStreamPlaying(this.currentMusic))
                Raylib.PlayMusicStream(this.currentMusic);
            this.Update();
        }

        public void PauseMusic()
        {
            if (Raylib.IsMusicStreamPlaying(currentMusic))
                Raylib.PauseMusicStream(currentMusic);
            this.isPaused = true;
        }
        public void ResumeMusic()
        {
            this.isPaused = false;
        }

        public void StopMusic()
        {
            if (Raylib.IsMusicStreamPlaying(currentMusic))
                Raylib.StopMusicStream(currentMusic);
            this.isPaused = true;
        }

    }
}
