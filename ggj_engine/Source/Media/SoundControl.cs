﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Media
{
    public class SoundControl
    {
        public FMOD.System SoundSystem;

        private Dictionary<String, FMOD.Sound> loadedSounds;
        private List<Channel> playingChannels;
        private FMOD.ChannelGroup effectsChannelGroup;
        private FMOD.ChannelGroup musicChannelGroup;

        #region CONSTANTS
        const float FADE_WIDTH = 250f;
        const float FADE_VOLUME_MAX_DIST = 1000f;
        #endregion



        public SoundControl(FMOD.System soundSystem)
        {
            SoundSystem = soundSystem;
            loadedSounds = new Dictionary<String, FMOD.Sound>();
            playingChannels = new List<Channel>();
            effectsChannelGroup = new FMOD.ChannelGroup();
            musicChannelGroup = new FMOD.ChannelGroup();
            SoundSystem.createChannelGroup(null, ref effectsChannelGroup);
            SoundSystem.createChannelGroup(null, ref musicChannelGroup);
        }

        /// <summary>
        /// Loads all sounds into memory
        /// </summary>
        /// <param name="names"></param>
        /// <param name="soundPaths"></param>
        public void LoadAllSounds(Dictionary<string, string>.KeyCollection names, Dictionary<string, string>.ValueCollection soundPaths)
        {
            //Load all sounds into memory and dont stream them from the disk trying to decompress at runtime ahem gee cough.
            //This could be better in that songs should be streamed rather than loaded, but who cares.
            for (int i = 0; i < names.Count; i++)
            {
                string name = names.ElementAt(i);
                string path = soundPaths.ElementAt(i);

                FMOD.Sound sound = new FMOD.Sound();
                FMOD.RESULT result = SoundSystem.createSound(path, FMOD.MODE.DEFAULT, ref sound);
                if (result != FMOD.RESULT.OK)
                {
                    throw new FormatException("Error with result loading sound");
                }
                loadedSounds.Add(name, sound);
            }
        }

        /// <summary>
        /// Plays a sound
        /// </summary>
        /// <param name="sound">String containing the name of the sound to play</param>
        /// <param name="loop">Whether to loop or not (please keep track of this channel if you loop)</param>
        /// <returns></returns>
        public Channel PlaySFX(string sound, bool loop)
        {
            Channel channel = PlaySFX(sound, loop, false, Vector2.Zero);
            return channel;
        }

        /// <summary>
        /// Plays a sound
        /// </summary>
        /// <param name="sound">String containing the name of the sound to play</param>
        /// <param name="loop">Whether to loop or not (please keep track of this channel if you loop)</param>
        /// <param name="positional">Is this sound a positional sound</param>
        /// <param name="position">Position of sound in pixel world space</param>
        /// <returns></returns>
        public Channel PlaySFX(string sound, bool loop, bool positional, Vector2 position)
        {
            Channel channel = new Channel();
            FMOD.Channel fmodChannel = new FMOD.Channel();

            if (!loadedSounds.ContainsKey(sound))
            {
                throw new FieldAccessException("Invalid sound, " + sound + " does not exist!");
            }

            FMOD.RESULT result = SoundSystem.playSound(FMOD.CHANNELINDEX.FREE, loadedSounds[sound], false, ref fmodChannel);
            channel.channel = fmodChannel;

            //Add to the effects channel
            fmodChannel.setChannelGroup(effectsChannelGroup);

            //Looping
            if (loop)
            {
                fmodChannel.setMode(FMOD.MODE.LOOP_NORMAL);
                fmodChannel.setLoopCount(-1);
            }

            //Indicate whether this sound should be positional or not
            channel.Positional = positional;
            channel.Position = position;

            playingChannels.Add(channel);

            return channel;
        }

        /// <summary>
        /// Plays music
        /// </summary>
        /// <param name="sound">Name of song to play</param>
        /// <param name="loop">Whether to loop or not</param>
        /// <returns></returns>
        public Channel PlayMusic(string sound, bool loop)
        {
            Channel channel = new Channel();
            FMOD.Channel fmodChannel = new FMOD.Channel();

            FMOD.RESULT result = SoundSystem.playSound(FMOD.CHANNELINDEX.FREE, loadedSounds[sound], false, ref fmodChannel);
            channel.channel = fmodChannel;

            //Add to the music channel
            fmodChannel.setChannelGroup(musicChannelGroup);

            //Looping
            if (loop)
            {
                fmodChannel.setMode(FMOD.MODE.LOOP_NORMAL);
                fmodChannel.setLoopCount(-1);
            }

            playingChannels.Add(channel);

            return channel;
        }

        /// <summary>
        /// Sets the music volume
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicVolume(float volume)
        {
            musicChannelGroup.setVolume(volume);
        }

        /// <summary>
        /// Sets the sfx volume
        /// </summary>
        /// <param name="volume"></param>
        public void SetEffectsVolume(float volume)
        {
            effectsChannelGroup.setVolume(volume);
        }

        /// <summary>
        /// Updates the positions of positional sounds
        /// </summary>
        /// <param name="centerPosition"></param>
        public void UpdatePositionalSounds(Vector2 centerPosition)
        {
            foreach (Channel channel in playingChannels)
            {
                if (channel.Positional)
                {
                    //Find the pan of the channel and clamp from -1 to 1
                    float pan = Math.Max(-1, Math.Min(1, ((channel.Position.X - centerPosition.X) / FADE_WIDTH)));
                    channel.channel.setPan(pan);

                    //Volume! Also clamped from 0 to 1
                    float volume = Math.Max(0, Math.Min(1, 1 - (Vector2.Distance(channel.Position, centerPosition) / FADE_VOLUME_MAX_DIST)));
                    channel.channel.setVolume(channel.Volume * volume);
                }
            }
        }

        /// <summary>
        /// Update the controller
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < playingChannels.Count; i++)
            {
                Channel channel = playingChannels.ElementAt(i);

                channel.Update();

                //Clean up stopped channels
                bool playing = false;
                channel.channel.isPlaying(ref playing);
                if (!playing)
                {
                    playingChannels.Remove(channel);
                    i--;
                }
            }
        }

        public void SetPauseAllChannels(bool paused)
        {
            foreach (Channel c in playingChannels)
            {
                c.SetPause(paused);
            }
        }

        public class Channel
        {
            public FMOD.Channel channel;
            public Vector2 Position;
            public bool Positional;
            public float Volume = 1;

            private float volumeFade;
            private bool disposeWhenSilent;

            /// <summary>
            /// Disposes the sound and stops playing it
            /// </summary>
            public void Dispose()
            {
                channel.stop();
            }

            /// <summary>
            /// Pause or resume the sound
            /// </summary>
            /// <param name="value"></param>
            public void SetPause(bool value)
            {
                channel.setPaused(value);
            }

            /// <summary>
            /// Fades the volume of this channel to 100% or 0%
            /// </summary>
            /// <param name="fadeSpeed"></param>
            public void SetFade(float fadeSpeed)
            {
                volumeFade = fadeSpeed;
            }

            /// <summary>
            /// Fades the sound until silenced, and then disposes
            /// </summary>
            /// <param name="fadeSpeed"></param>
            public void SetFadeOutDispose(float fadeSpeed)
            {
                volumeFade = fadeSpeed;
                disposeWhenSilent = true;
            }

            public void Update()
            {
                Volume += volumeFade;
                Volume = Math.Max(0, Math.Min(1, Volume));
                if (Volume == 0 || Volume == 1)
                {
                    volumeFade = 0;
                }

                if (Volume == 0 && disposeWhenSilent)
                {
                    Dispose();
                }

                channel.setVolume(Volume);
            }
        }
    }
}
