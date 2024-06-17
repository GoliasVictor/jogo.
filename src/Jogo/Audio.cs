﻿using Raylib_cs;
using static Game.IAudio;

/// TODO: Adjust Volumes according to sound
namespace Game
{
    internal class Audio : IAudio
    {
        public Music GetMusic(MusicEffect music)
        {
            throw new NotImplementedException();
        }

        public void PlaySound(IAudio.SoundEffect soundEffect)
        {
            string SoundFileName = "Step.wav";
            if (soundEffect == IAudio.SoundEffect.Block) 
                SoundFileName = "Block.wav";
            else if (soundEffect == IAudio.SoundEffect.Fire)
                SoundFileName = "Fire.wav";
            else if (soundEffect == IAudio.SoundEffect.Water)
                SoundFileName = "Water.wav";
            else if (soundEffect == IAudio.SoundEffect.Wood)
                SoundFileName = "Wood.wav";
            else if (soundEffect == IAudio.SoundEffect.Block)
                SoundFileName = "Block.wav";
            Sound sound = Raylib.LoadSound(SoundFileName);
            Raylib.PlaySound(sound);
        }

        public void UpdateMusic()
        {
            throw new NotImplementedException();
        }
    }
}