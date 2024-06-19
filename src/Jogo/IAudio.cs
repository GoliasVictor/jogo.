using Raylib_cs;

namespace Game
{
    /// <summary>
    /// Interface para carregar streams de audio Raylib
    /// </summary>
    internal interface IAudio
    {
        /// <summary>
        /// Enum of different music files
        /// </summary>
        public enum MusicEffect
        {
            TitleScreen,
            Puzzle,
            Boss,
            GameEnd
        }

        /// <summary>
        /// Enum of different sound effects
        /// </summary>
        public enum SoundEffect
        {
            Step,
            PushBlock,
            Fire,
            Water,
            Wall
        }

        /// <summary>
        /// <param>Used to get AudioStream from desired music</param>
        /// </summary>
        /// <param name="music">Music</param>
        /// <returns>AudioStream</returns>
        internal Music GetMusic(MusicEffect music);

        /// <summary>
        /// <param>Used to play Sound Effects</param>
        /// <param>If no valid (or implemented) sound is provided, plays step sound</param>
        /// </summary>
        /// <param name="sound">SoundEffect</param>
        /// <returns>AudioStream</returns>
        public void PlaySound(SoundEffect sound);

        public void UpdateMusic();

        public bool IsMusicPlaying();
    }
}
