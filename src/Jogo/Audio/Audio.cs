using Raylib_cs;
using static Jogo.Audio.IAudio;

/// TODO: Adjust Volumes according to sound
namespace Jogo.Audio
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
                default:
                    SoundFileName = "sfx-step.wav";
                    break;
            };
            Raylib.UnloadSound(currentSound);
            currentSound = Raylib.LoadSound(path+SoundFileName);
            Raylib.PlaySound(currentSound);
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

        public void UpdateMusic(MusicEffect song = MusicEffect.None)
        {
            if (song != MusicEffect.None)
            {
                if (Raylib.IsMusicStreamPlaying(currentMusic))
                    Raylib.UnloadMusicStream(currentMusic);
                currentMusic = GetMusic(song);
                isPaused = false;
            }
            if (!Raylib.IsMusicStreamPlaying(currentMusic) && !isPaused)
                Raylib.PlayMusicStream(currentMusic);
            Raylib.UpdateMusicStream(currentMusic);
        }

        public void PauseMusic()
        {
            if (Raylib.IsMusicStreamPlaying(currentMusic))
                Raylib.PauseMusicStream(currentMusic);
            isPaused = true;
        }
        public void ResumeMusic()
        {
            isPaused = false;
        }

        public void StopMusic()
        {
            if (Raylib.IsMusicStreamPlaying(currentMusic))
                Raylib.StopMusicStream(currentMusic);
            isPaused = true;
        }

    }
}
