﻿using UnityEngine.Timeline.Data;

namespace UnityEngine.Timeline
{
    public class XTimeline
    {
        public TimelineConfig config;
        public XTrack[] trackTrees;
        private float prev;

        private static uint id = 0;

        public static uint IncID { get { return id++; } }
        
        public float Time { get { return prev; } }

        public XTimeline(string path)
        {
            config = new TimelineConfig();
            config.Read(path);
            Build();
        }

        public XTimeline(TimelineConfig config)
        {
            this.config = config;
            Build();
        }

        private void Build()
        {
            var tracksData = config.tracks;
            int len = tracksData.Length;
            trackTrees = new XTrack[len];
            for (int i = 0; i < len; i++)
            {
                trackTrees[i] = XTrack.NewTrack(tracksData[i], this);
            }
            prev = 0;
        }


        public void Process(float time)
        {
            for (int i = 0; i < trackTrees.Length; i++)
            {
                trackTrees[i].Process(time, prev);
            }
            prev = time;
        }

        public void Dispose()
        {
            for (int i = 0; i < trackTrees.Length; i++)
            {
                trackTrees[i].Dispose();
            }
        }

        public float RecalcuteDuration()
        {
            float dur = 0;
            if (trackTrees != null)
            {
                for(int i=0;i<trackTrees.Length;i++)
                {
                    var track = trackTrees[i];
                    track.ForeachHierachyTrack((trac)=>trac.ForeachClip((clip) =>
                    {
                        if (clip.end >dur)
                        {
                            dur = clip.end;
                        }
                    }));
                }
            }
            return dur;
        }

        public static implicit operator bool(XTimeline timeline)
        {
            return timeline != null;
        }

    }

}