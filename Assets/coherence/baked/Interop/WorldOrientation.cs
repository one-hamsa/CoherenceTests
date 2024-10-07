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

    public struct WorldOrientation : ICoherenceComponentData
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Interop
        {
            [FieldOffset(0)]
            public Quaternion value;
        }

        public static unsafe WorldOrientation FromInterop(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 16) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 16) " +
                    "for component with ID 1");
            }

            if (simFramesCount != 1) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 1) " +
                    "for component with ID 1");
            }

            var orig = new WorldOrientation();

            var comp = (Interop*)data;

            orig.value = comp->value;
            orig.valueSimulationFrame = simFrames[0].Into();

            return orig;
        }

        public static unsafe WorldOrientation FromInteropArchetype_f56c32abdf848d041964eb9b807eddfe_WorldOrientation_LOD0(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 16) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 16) " +
                    "for component with ID 168");
            }

                
            if (simFramesCount != 1) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 1) " +
                    "for component with ID 168");
            }

            var orig = new WorldOrientation();

            var comp = (Interop*)data;

            orig.value = comp->value;
            orig.valueSimulationFrame = simFrames[0].Into();

            return orig;
        }

        public static uint valueMask => 0b00000000000000000000000000000001;
        public AbsoluteSimulationFrame valueSimulationFrame;
        public Quaternion value;

        public uint FieldsMask { get; set; }
        public uint StoppedMask { get; set; }
        public uint GetComponentType() => 1;
        public int PriorityLevel() => 1000;
        public const int order = 0;
        public uint InitialFieldsMask() => 0b00000000000000000000000000000001;
        public bool HasFields() => true;
        public bool HasRefFields() => false;

        private long[] simulationFrames;

        public long[] GetSimulationFrames() {
            if (simulationFrames == null)
            {
                simulationFrames = new long[1];
            }

            simulationFrames[0] = valueSimulationFrame;

            return simulationFrames;
        }

        public int GetFieldCount() => 1;


        
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

            if ((FieldsMask & WorldOrientation.valueMask) != 0 && (min == null || this.valueSimulationFrame < min))
            {
                min = this.valueSimulationFrame;
            }

            return min;
        }

        public ICoherenceComponentData MergeWith(ICoherenceComponentData data)
        {
            var other = (WorldOrientation)data;
            var otherMask = other.FieldsMask;

            FieldsMask |= otherMask;
            StoppedMask &= ~(otherMask);

            if ((otherMask & 0x01) != 0)
            {
                this.valueSimulationFrame = other.valueSimulationFrame;
                this.value = other.value;
            }

            otherMask >>= 1;
            StoppedMask |= other.StoppedMask;

            return this;
        }

        public uint DiffWith(ICoherenceComponentData data)
        {
            throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");
        }

        public static uint Serialize(WorldOrientation data, bool isRefSimFrameValid, AbsoluteSimulationFrame referenceSimulationFrame, IOutProtocolBitStream bitStream, Logger logger)
        {
            if (bitStream.WriteMask(data.StoppedMask != 0))
            {
                bitStream.WriteMaskBits(data.StoppedMask, 1);
            }

            var mask = data.FieldsMask;

            if (bitStream.WriteMask((mask & 0x01) != 0))
            {
                if (isRefSimFrameValid) {
                    var simFrameDelta = data.valueSimulationFrame - referenceSimulationFrame;
                    if (simFrameDelta > byte.MaxValue) {
                        simFrameDelta = byte.MaxValue;
                    }

                    SerializeTools.WriteFieldSimFrameDelta(bitStream, (byte)simFrameDelta);
                } else {
                    SerializeTools.WriteFieldSimFrameDelta(bitStream, 0);
                }


                var fieldValue = (data.value.ToCoreQuaternion());



                bitStream.WriteQuaternion(fieldValue, 32);
            }

            mask >>= 1;

            return mask;
        }

        public static WorldOrientation Deserialize(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(1);
            }

            var val = new WorldOrientation();
            if (bitStream.ReadMask())
            {
                val.valueSimulationFrame = referenceSimulationFrame + DeserializerTools.ReadFieldSimFrameDelta(bitStream);

                val.value = bitStream.ReadQuaternion(32).ToUnityQuaternion();
                val.FieldsMask |= WorldOrientation.valueMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }

        public static WorldOrientation DeserializeArchetype_f56c32abdf848d041964eb9b807eddfe_WorldOrientation_LOD0(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(1);
            }

            var val = new WorldOrientation();
            if (bitStream.ReadMask())
            {
                val.valueSimulationFrame = referenceSimulationFrame + DeserializerTools.ReadFieldSimFrameDelta(bitStream);

                val.value = bitStream.ReadQuaternion(32).ToUnityQuaternion();
                val.FieldsMask |= valueMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }

        public override string ToString()
        {
            return $"WorldOrientation(" +
                $" value: { this.value }" +
                $", valueSimFrame: { this.valueSimulationFrame }" +
                $" Mask: { System.Convert.ToString(FieldsMask, 2).PadLeft(1, '0') }, " +
                $"Stopped: { System.Convert.ToString(StoppedMask, 2).PadLeft(1, '0') })";
        }
    }


}