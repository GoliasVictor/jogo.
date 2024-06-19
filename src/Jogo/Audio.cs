using Raylib_cs;
using static Game.IAudio;

/// TODO: Adjust Volumes according to sound
namespace Game
{
    internal class Audio : IAudio
    {
        private Music currentMusic;

        public Music GetMusic(MusicEffect music)
        {
            throw new NotImplementedException();
        }

        public bool IsMusicPlaying()
        {

            throw new NotImplementedException();
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
            Sound sound = Raylib.LoadSound(SoundFileName);
            Raylib.PlaySound(sound);
        }

        public void UpdateMusic()
        {
            throw new NotImplementedException();
        }
    }
}
