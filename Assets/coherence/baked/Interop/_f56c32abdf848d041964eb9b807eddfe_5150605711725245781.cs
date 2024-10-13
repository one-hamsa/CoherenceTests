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

    public struct _f56c32abdf848d041964eb9b807eddfe_5150605711725245781 : ICoherenceComponentData
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
            [FieldOffset(52)]
            public System.Double dataUpdateTime;
        }

        public static unsafe _f56c32abdf848d041964eb9b807eddfe_5150605711725245781 FromInterop(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 60) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 60) " +
                    "for component with ID 165");
            }

            if (simFramesCount != 1) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 1) " +
                    "for component with ID 165");
            }

            var orig = new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();

            var comp = (Interop*)data;

            orig.velocity = comp->velocity;
            orig.angularVelocity = comp->angularVelocity;
            orig.position = comp->position;
            orig.rotation = comp->rotation;
            orig.dataUpdateTime = comp->dataUpdateTime;
            orig.dataUpdateTimeSimulationFrame = simFrames[0].Into();

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
        public static uint dataUpdateTimeMask => 0b00000000000000000000000000010000;
        public AbsoluteSimulationFrame dataUpdateTimeSimulationFrame;
        public System.Double dataUpdateTime;

        public uint FieldsMask { get; set; }
        public uint StoppedMask { get; set; }
        public uint GetComponentType() => 165;
        public int PriorityLevel() => 100;
        public const int order = 0;
        public uint InitialFieldsMask() => 0b00000000000000000000000000011111;
        public bool HasFields() => true;
        public bool HasRefFields() => false;

        private long[] simulationFrames;

        public long[] GetSimulationFrames() {
            if (simulationFrames == null)
            {
                simulationFrames = new long[1];
            }

            simulationFrames[0] = dataUpdateTimeSimulationFrame;

            return simulationFrames;
        }

        public int GetFieldCount() => 5;


        
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

            if ((FieldsMask & _f56c32abdf848d041964eb9b807eddfe_5150605711725245781.dataUpdateTimeMask) != 0 && (min == null || this.dataUpdateTimeSimulationFrame < min))
            {
                min = this.dataUpdateTimeSimulationFrame;
            }

            return min;
        }

        public ICoherenceComponentData MergeWith(ICoherenceComponentData data)
        {
            var other = (_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)data;
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
            if ((otherMask & 0x01) != 0)
            {
                this.dataUpdateTimeSimulationFrame = other.dataUpdateTimeSimulationFrame;
                this.dataUpdateTime = other.dataUpdateTime;
            }

            otherMask >>= 1;
            StoppedMask |= other.StoppedMask;

            return this;
        }

        public uint DiffWith(ICoherenceComponentData data)
        {
            throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");
        }

        public static uint Serialize(_f56c32abdf848d041964eb9b807eddfe_5150605711725245781 data, bool isRefSimFrameValid, AbsoluteSimulationFrame referenceSimulationFrame, IOutProtocolBitStream bitStream, Logger logger)
        {
            if (bitStream.WriteMask(data.StoppedMask != 0))
            {
                bitStream.WriteMaskBits(data.StoppedMask, 5);
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
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {
                if (isRefSimFrameValid) {
                    var simFrameDelta = data.dataUpdateTimeSimulationFrame - referenceSimulationFrame;
                    if (simFrameDelta > byte.MaxValue) {
                        simFrameDelta = byte.MaxValue;
                    }

                    SerializeTools.WriteFieldSimFrameDelta(bitStream, (byte)simFrameDelta);
                } else {
                    SerializeTools.WriteFieldSimFrameDelta(bitStream, 0);
                }


                var fieldValue = data.dataUpdateTime;



                bitStream.WriteDouble(fieldValue);
            }

            mask >>= 1;

            return mask;
        }

        public static _f56c32abdf848d041964eb9b807eddfe_5150605711725245781 Deserialize(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(5);
            }

            var val = new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();
            if (bitStream.ReadMask())
            {

                val.velocity = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_5150605711725245781.velocityMask;
            }
            if (bitStream.ReadMask())
            {

                val.angularVelocity = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_5150605711725245781.angularVelocityMask;
            }
            if (bitStream.ReadMask())
            {

                val.position = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_5150605711725245781.positionMask;
            }
            if (bitStream.ReadMask())
            {

                val.rotation = bitStream.ReadQuaternion(32).ToUnityQuaternion();
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_5150605711725245781.rotationMask;
            }
            if (bitStream.ReadMask())
            {
                val.dataUpdateTimeSimulationFrame = referenceSimulationFrame + DeserializerTools.ReadFieldSimFrameDelta(bitStream);

                val.dataUpdateTime = bitStream.ReadDouble();
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_5150605711725245781.dataUpdateTimeMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }


        public override string ToString()
        {
            return $"_f56c32abdf848d041964eb9b807eddfe_5150605711725245781(" +
                $" velocity: { this.velocity }" +
                $" angularVelocity: { this.angularVelocity }" +
                $" position: { this.position }" +
                $" rotation: { this.rotation }" +
                $" dataUpdateTime: { this.dataUpdateTime }" +
                $", dataUpdateTimeSimFrame: { this.dataUpdateTimeSimulationFrame }" +
                $" Mask: { System.Convert.ToString(FieldsMask, 2).PadLeft(5, '0') }, " +
                $"Stopped: { System.Convert.ToString(StoppedMask, 2).PadLeft(5, '0') })";
        }
    }


}