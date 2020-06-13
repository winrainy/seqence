﻿using System;
using UnityEngine.Timeline.Data;

namespace UnityEngine.Timeline
{
    public enum TrackFlag
    {
        SubOnly = 1,
        RootOnly = 1 << 1,
        NoClip = 1 << 2,
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TrackFlagAttribute : Attribute
    {
        public bool isOnlySub
        {
            get { return (flag & TrackFlag.SubOnly) > 0; }
        }

        public bool allowClip
        {
            get
            {
                bool noclip = (flag & TrackFlag.NoClip) > 0;
                return !noclip;
            }
        }

        private TrackFlag flag;

        public TrackFlagAttribute(TrackFlag flag)
        {
            this.flag = flag;
        }
    }

    public class TrackRequreType : Attribute
    {
        public Type type;

        public TrackRequreType(Type t)
        {
            type = t;
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class UseParentAttribute : Attribute
    {
        public Type parent;

        public UseParentAttribute(Type t)
        {
            parent = t;
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class MarkUsageAttribute : Attribute
    {
        public TrackType type;

        public MarkUsageAttribute(TrackType t)
        {
            this.type = t;
        }
    }
}
