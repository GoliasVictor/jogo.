using Raylib_cs;

namespace Jogo.Systems
{
    /// <summary>
    /// Interface para carregar streams de audio Raylib
    /// </summary>
    internal interface IAudio : ISystem
    {
        /// <summary>
        /// Enum of different music files
        /// </summary>
        public enum MusicEffect
        {
            TitleScreen,
            Puzzle,
            Boss,
            GameEnd,
            None
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
            Wall,
            LevelComplete
        }

        // public Music GetMusic(MusicEffect music);

        /// <summary>
        /// <param>Used to play Sound Effects</param>
        /// <param>If no valid (or implemented) sound is provided, plays step sound</param>
        /// </summary>
        /// <param name="sound">SoundEffect</param>
        /// <returns>AudioStream</returns>
        public void  PlaySound(SoundEffect sound);


        /// <summary>
        /// <param>Updates Music Stream buffer and starts to play if it isn't being played</param>
        /// </summary>
        /// <param name="song"> If no music is specified just keeps playing the one previously set</param>
        public void PlayMusic(MusicEffect song);

        /// <summary>
        /// <param>Pauses current Music Stream</param>
        /// </summary>
        public void PauseMusic();

        /// <summary>
        /// <param>Unpauses current Music Stream</param>
        /// </summary>
        public void ResumeMusic();

        /// <summary>
        /// <param>Stops music</param>
        /// </summary>
        public void StopMusic();
    }
}
