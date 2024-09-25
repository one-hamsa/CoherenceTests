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

    public struct _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867 : ICoherenceComponentData
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Interop
        {
            [FieldOffset(0)]
            public System.Byte blah;
            [FieldOffset(1)]
            public Vector3 stam;
        }

        public static unsafe _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867 FromInterop(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 13) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 13) " +
                    "for component with ID 156");
            }

            if (simFramesCount != 0) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 0) " +
                    "for component with ID 156");
            }

            var orig = new _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867();

            var comp = (Interop*)data;

            orig.blah = comp->blah != 0;
            orig.stam = comp->stam;

            return orig;
        }


        public static uint blahMask => 0b00000000000000000000000000000001;
        public AbsoluteSimulationFrame blahSimulationFrame;
        public System.Boolean blah;
        public static uint stamMask => 0b00000000000000000000000000000010;
        public AbsoluteSimulationFrame stamSimulationFrame;
        public Vector3 stam;

        public uint FieldsMask { get; set; }
        public uint StoppedMask { get; set; }
        public uint GetComponentType() => 156;
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


        public AbsoluteSimulationFrame? GetMinSimulationFrame()
        {
            AbsoluteSimulationFrame? min = null;


            return min;
        }

        public ICoherenceComponentData MergeWith(ICoherenceComponentData data)
        {
            var other = (_7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867)data;
            var otherMask = other.FieldsMask;

            FieldsMask |= otherMask;
            StoppedMask &= ~(otherMask);

            if ((otherMask & 0x01) != 0)
            {
                this.blahSimulationFrame = other.blahSimulationFrame;
                this.blah = other.blah;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.stamSimulationFrame = other.stamSimulationFrame;
                this.stam = other.stam;
            }

            otherMask >>= 1;
            StoppedMask |= other.StoppedMask;

            return this;
        }

        public uint DiffWith(ICoherenceComponentData data)
        {
            throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");
        }

        public static uint Serialize(_7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867 data, bool isRefSimFrameValid, AbsoluteSimulationFrame referenceSimulationFrame, IOutProtocolBitStream bitStream, Logger logger)
        {
            if (bitStream.WriteMask(data.StoppedMask != 0))
            {
                bitStream.WriteMaskBits(data.StoppedMask, 2);
            }

            var mask = data.FieldsMask;

            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = data.blah;



                bitStream.WriteBool(fieldValue);
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {


                var fieldValue = (data.stam.ToCoreVector3());



                bitStream.WriteVector3(fieldValue, FloatMeta.NoCompression());
            }

            mask >>= 1;

            return mask;
        }

        public static _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867 Deserialize(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(2);
            }

            var val = new _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867();
            if (bitStream.ReadMask())
            {

                val.blah = bitStream.ReadBool();
                val.FieldsMask |= _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867.blahMask;
            }
            if (bitStream.ReadMask())
            {

                val.stam = bitStream.ReadVector3(FloatMeta.NoCompression()).ToUnityVector3();
                val.FieldsMask |= _7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867.stamMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }


        public override string ToString()
        {
            return $"_7aa047939d3aa49498cc38bb3ac3eec7_7616975171125922867(" +
                $" blah: { this.blah }" +
                $" stam: { this.stam }" +
                $" Mask: { System.Convert.ToString(FieldsMask, 2).PadLeft(2, '0') }, " +
                $"Stopped: { System.Convert.ToString(StoppedMask, 2).PadLeft(2, '0') })";
        }
    }


}