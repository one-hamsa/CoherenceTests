// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
    using System;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;
    using Coherence.ProtocolDef;
    using Coherence.Serializer;
    using Coherence.SimulationFrame;
    using Coherence.Entities;
    using Coherence.Utils;
    using Coherence.Brook;
    using Coherence.Core;
    using Logger = Coherence.Log.Logger;
    using UnityEngine;
    using Coherence.Toolkit;

    public struct _daaa1fa3f3526944a84e601f43726765_7687811572458356758 : ICoherenceComponentData
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Interop
        {
            [FieldOffset(0)]
            public Vector3 velocity;
            [FieldOffset(12)]
            public Vector3 angularVelocity;
            [FieldOffset(24)]
            public Vector3 position;
            [FieldOffset(36)]
            public Quaternion rotation;
        }

        public static unsafe _daaa1fa3f3526944a84e601f43726765_7687811572458356758 FromInterop(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 52) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 52) " +
                    "for component with ID 163");
            }

            if (simFramesCount != 0) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 0) " +
                    "for component with ID 163");
            }

            var orig = new _daaa1fa3f3526944a84e601f43726765_7687811572458356758();

            var comp = (Interop*)data;

            orig.velocity = comp->velocity;
            orig.angularVelocity = comp->angularVelocity;
            orig.position = comp->position;
            orig.rotation = comp->rotation;

            return orig;
        }


        public static uint velocityMask => 0b00000000000000000000000000000001;
        public AbsoluteSimulationFrame velocitySimulationFrame;
        public Vector3 velocity;
        public static uint angularVelocityMask => 0b00000000000000000000000000000010;
        public AbsoluteSimulationFrame angularVelocitySimulationFrame;
        public Vector3 angularVelocity;
        public static uint positionMask => 0b00000000000000000000000000000100;
        public AbsoluteSimulationFrame positionSimulationFrame;
        public Vector3 position;
        public static uint rotationMask => 0b00000000000000000000000000001000;
        public AbsoluteSimulationFrame rotationSimulationFrame;
        public Quaternion rotation;

        public uint FieldsMask { get; set; }
        public uint StoppedMask { get; set; }
        public uint GetComponentType() => 163;
        public int PriorityLevel() => 100;
        public const int order = 0;
        public uint InitialFieldsMask() => 0b00000000000000000000000000001111;
        public bool HasFields() => true;
        public bool HasRefFields() => false;


        public long[] GetSimulationFrames() {
            return null;
        }

        public int GetFieldCount() => 4;


        
        public HashSet<Entity> GetEntityRefs()
        {
            return default;
        }

        public uint ReplaceReferences(Entity fromEntity, Entity toEntity)
        {
            return 0;
        }
        
        public IEntityMapper.Error MapToAbsolute(IEntityMapper mapper)
        {
            return IEntityMapper.Error.None;
        }

        public IEntityMapper.Error MapToRelative(IEntityMapper mapper)
        {
            return IEntityMapper.Error.None;
        }

        public ICoherenceComponentData Clone() => this;
        public int GetComponentOrder() => order;
        public bool IsSendOrdered() => false;


        public AbsoluteSimulationFrame? GetMinSimulationFrame()
        {
            AbsoluteSimulationFrame? min = null;


            return min;
        }

        public ICoherenceComponentData MergeWith(ICoherenceComponentData data)
        {
            var other = (_daaa1fa3f3526944a84e601f43726765_7687811572458356758)data;
            var otherMask = other.FieldsMask;

            FieldsMask |= otherMask;
            StoppedMask &= ~(otherMask);

            if ((otherMask & 0x01) != 0)
            {
                this.velocitySimulationFrame = other.velocitySimulationFrame;
                this.velocity = other.velocity;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.angularVelocitySimulationFrame = other.angularVelocitySimulationFrame;
                this.angularVelocity = other.angularVelocity;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.positionSimulationFrame = other.positionSimulationFrame;
                this.position = other.position;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.rotationSimulationFrame = other.rotationSimulationFrame;
                this.rotation = other.rotation;
            }

            otherMask >>= 1;
            StoppedMask |= other.StoppedMask;

            return this;
        }

        public uint DiffWith(ICoherenceComponentData data)
        {
            throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");
        }

        public static uint Serialize(_daaa1fa3f3526944a84e601f43726765_7687811572458356758 data, bool isRefSimFrameValid, AbsoluteSimulationFrame referenceSimulationFrame, IOutProtocolBitStream bitStream, Logger logger)
        {
            if (bitStream.WriteMask(data.StoppedMask != 0))
            {
                bitStream.WriteMaskBits(data.StoppedMask, 4);
            }

            var mask = data.FieldsMask;

            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = (data.velocity.ToCoreVector3());



                bitStream.WriteVector3(fieldValue, FloatMeta.NoCompression());
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = (data.angularVelocity.ToCoreVector3());



                bitStream.WriteVector3(fieldValue, FloatMeta.NoCompression());
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = (data.position.ToCoreVector3());



                bitStream.WriteVector3(fieldValue, FloatMeta.NoCompression());
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = (data.rotation.ToCoreQuaternion());



                bitStream.WriteQuaternion(fieldValue, 32);
            }

            mask >>= 1;

            return mask;
        }

        public static _daaa1fa3f3526944a84e601f43726765_7687811572458356758 Deserialize(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(4);
            }

            var val = new _daaa1fa3f3526944a84e601f43726765_7687811572458356758();
            if (bitStream.ReadMask())
            {

                val.velocity = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_7687811572458356758.velocityMask;
            }
            if (bitStream.ReadMask())
            {

                val.angularVelocity = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_7687811572458356758.angularVelocityMask;
            }
            if (bitStream.ReadMask())
            {

                val.position = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_7687811572458356758.positionMask;
            }
            if (bitStream.ReadMask())
            {

                val.rotation = bitStream.ReadQuaternion(32).ToUnityQuaternion();
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_7687811572458356758.rotationMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }


        public override string ToString()
        {
            return $"_daaa1fa3f3526944a84e601f43726765_7687811572458356758(" +
                $" velocity: { this.velocity }" +
                $" angularVelocity: { this.angularVelocity }" +
                $" position: { this.position }" +
                $" rotation: { this.rotation }" +
                $" Mask: { System.Convert.ToString(FieldsMask, 2).PadLeft(4, '0') }, " +
                $"Stopped: { System.Convert.ToString(StoppedMask, 2).PadLeft(4, '0') })";
        }
    }


}