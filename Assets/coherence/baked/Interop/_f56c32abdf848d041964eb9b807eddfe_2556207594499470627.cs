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

    public struct _f56c32abdf848d041964eb9b807eddfe_2556207594499470627 : ICoherenceComponentData
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Interop
        {
            [FieldOffset(0)]
            public ByteArray path;
            [FieldOffset(16)]
            public System.Int32 pathDirtyCounter;
        }

        public static unsafe _f56c32abdf848d041964eb9b807eddfe_2556207594499470627 FromInterop(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 20) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 20) " +
                    "for component with ID 164");
            }

            if (simFramesCount != 0) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 0) " +
                    "for component with ID 164");
            }

            var orig = new _f56c32abdf848d041964eb9b807eddfe_2556207594499470627();

            var comp = (Interop*)data;

            orig.path = comp->path.Data != null ? System.Text.Encoding.UTF8.GetString((byte*)comp->path.Data, (int)comp->path.Length) : null;
            orig.pathDirtyCounter = comp->pathDirtyCounter;

            return orig;
        }


        public static uint pathMask => 0b00000000000000000000000000000001;
        public AbsoluteSimulationFrame pathSimulationFrame;
        public System.String path;
        public static uint pathDirtyCounterMask => 0b00000000000000000000000000000010;
        public AbsoluteSimulationFrame pathDirtyCounterSimulationFrame;
        public System.Int32 pathDirtyCounter;

        public uint FieldsMask { get; set; }
        public uint StoppedMask { get; set; }
        public uint GetComponentType() => 164;
        public int PriorityLevel() => 100;
        public const int order = 0;
        public uint InitialFieldsMask() => 0b00000000000000000000000000000011;
        public bool HasFields() => true;
        public bool HasRefFields() => false;


        public long[] GetSimulationFrames() {
            return null;
        }

        public int GetFieldCount() => 2;


        
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

        private static readonly System.Int32 _pathDirtyCounter_Min = -2147483648;
        private static readonly System.Int32 _pathDirtyCounter_Max = 2147483647;

        public AbsoluteSimulationFrame? GetMinSimulationFrame()
        {
            AbsoluteSimulationFrame? min = null;


            return min;
        }

        public ICoherenceComponentData MergeWith(ICoherenceComponentData data)
        {
            var other = (_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)data;
            var otherMask = other.FieldsMask;

            FieldsMask |= otherMask;
            StoppedMask &= ~(otherMask);

            if ((otherMask & 0x01) != 0)
            {
                this.pathSimulationFrame = other.pathSimulationFrame;
                this.path = other.path;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.pathDirtyCounterSimulationFrame = other.pathDirtyCounterSimulationFrame;
                this.pathDirtyCounter = other.pathDirtyCounter;
            }

            otherMask >>= 1;
            StoppedMask |= other.StoppedMask;

            return this;
        }

        public uint DiffWith(ICoherenceComponentData data)
        {
            throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");
        }

        public static uint Serialize(_f56c32abdf848d041964eb9b807eddfe_2556207594499470627 data, bool isRefSimFrameValid, AbsoluteSimulationFrame referenceSimulationFrame, IOutProtocolBitStream bitStream, Logger logger)
        {
            if (bitStream.WriteMask(data.StoppedMask != 0))
            {
                bitStream.WriteMaskBits(data.StoppedMask, 2);
            }

            var mask = data.FieldsMask;

            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = data.path;



                bitStream.WriteShortString(fieldValue);
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {

                Coherence.Utils.Bounds.Check(data.pathDirtyCounter, _pathDirtyCounter_Min, _pathDirtyCounter_Max, "_f56c32abdf848d041964eb9b807eddfe_2556207594499470627.pathDirtyCounter", logger);

                data.pathDirtyCounter = Coherence.Utils.Bounds.Clamp(data.pathDirtyCounter, _pathDirtyCounter_Min, _pathDirtyCounter_Max);

                var fieldValue = data.pathDirtyCounter;



                bitStream.WriteIntegerRange(fieldValue, 32, -2147483648);
            }

            mask >>= 1;

            return mask;
        }

        public static _f56c32abdf848d041964eb9b807eddfe_2556207594499470627 Deserialize(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(2);
            }

            var val = new _f56c32abdf848d041964eb9b807eddfe_2556207594499470627();
            if (bitStream.ReadMask())
            {

                val.path = bitStream.ReadShortString();
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_2556207594499470627.pathMask;
            }
            if (bitStream.ReadMask())
            {

                val.pathDirtyCounter = bitStream.ReadIntegerRange(32, -2147483648);
                val.FieldsMask |= _f56c32abdf848d041964eb9b807eddfe_2556207594499470627.pathDirtyCounterMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }


        public override string ToString()
        {
            return $"_f56c32abdf848d041964eb9b807eddfe_2556207594499470627(" +
                $" path: { this.path }" +
                $" pathDirtyCounter: { this.pathDirtyCounter }" +
                $" Mask: { System.Convert.ToString(FieldsMask, 2).PadLeft(2, '0') }, " +
                $"Stopped: { System.Convert.ToString(StoppedMask, 2).PadLeft(2, '0') })";
        }
    }


}