// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
    using Coherence.ProtocolDef;
    using Coherence.Serializer;
    using Coherence.Brook;
    using Coherence.Entities;
    using Coherence.Log;
    using Coherence.Core;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System;
    using UnityEngine;

    public struct _86637d6221845a54c80f4dee3a49e53e : IEntityInput, IEquatable<_86637d6221845a54c80f4dee3a49e53e>
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Interop
        {
        }

        public static unsafe _86637d6221845a54c80f4dee3a49e53e FromInterop(System.IntPtr data, System.Int32 dataSize)
        {
            if (dataSize != 0) {
                throw new System.Exception($"Given data size is not equal to the struct size. ({dataSize} != 0) " +
                    "for input with ID 3");
            }

            var orig = new _86637d6221845a54c80f4dee3a49e53e();
            var comp = (Interop*)data;
            return orig;
        }

        public uint GetComponentType() => 3;

        public Entity Entity { get; set; }
        public MessageTarget Routing { get; set; }
        public uint Sender { get; set; }
        public long Frame { get; set; }
        private bool isRemoteInput;


        public _86637d6221845a54c80f4dee3a49e53e(
        Entity entity,
        long frame,
        bool isRemoteInput)
        {
            this.Entity = entity;
            this.Routing = MessageTarget.All;
            this.Sender = 0;
            this.Frame = frame;
            this.isRemoteInput = isRemoteInput;
        }

        public override string ToString()
        {
            return $"Entity: {Entity}, Frame: {Frame}, Inputs: []";
        }

        public IEntityMessage Clone()
        {
            // This is a struct, so we can safely return
            // a struct copy.
            return this;
        }

        public IEntityMapper.Error MapToAbsolute(IEntityMapper mapper, Coherence.Log.Logger logger)
        {
            var err = mapper.MapToAbsoluteEntity(Entity, false, out var absoluteEntity);
            if (err != IEntityMapper.Error.None)
            {
                return err;
            }
            Entity = absoluteEntity;
            return IEntityMapper.Error.None;
        }

        public IEntityMapper.Error MapToRelative(IEntityMapper mapper, Coherence.Log.Logger logger)
        {
            var err = mapper.MapToRelativeEntity(Entity, false, out var relativeEntity);
            if (err != IEntityMapper.Error.None)
            {
                return err;
            }
            Entity = relativeEntity;
            return IEntityMapper.Error.None;
        }

        public HashSet<Entity> GetEntityRefs() => default;

        public void NullEntityRefs(Entity entity) { }

        public bool Equals(_86637d6221845a54c80f4dee3a49e53e other)
        {
            return true;
        }

        public static void Serialize(_86637d6221845a54c80f4dee3a49e53e inputData, IOutProtocolBitStream bitStream)
        {
        }

        public static _86637d6221845a54c80f4dee3a49e53e Deserialize(IInProtocolBitStream bitStream, Entity entity, long frame)
        {

            return new _86637d6221845a54c80f4dee3a49e53e()
            {
                Entity = entity,
                Frame = frame,
                isRemoteInput = true
            };
        }
    }


}