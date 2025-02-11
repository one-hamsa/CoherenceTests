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

    public struct _daaa1fa3f3526944a84e601f43726765_6822197146557525417 : ICoherenceComponentData
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Interop
        {
            [FieldOffset(0)]
            public System.Int32 leftArmItemID;
            [FieldOffset(4)]
            public System.Int32 rightArmItemID;
            [FieldOffset(8)]
            public System.Int32 leftWeaponItemID;
            [FieldOffset(12)]
            public System.Int32 rightWeaponItemID;
        }

        public static unsafe _daaa1fa3f3526944a84e601f43726765_6822197146557525417 FromInterop(IntPtr data, Int32 dataSize, InteropAbsoluteSimulationFrame* simFrames, Int32 simFramesCount)
        {
            if (dataSize != 16) {
                throw new Exception($"Given data size is not equal to the struct size. ({dataSize} != 16) " +
                    "for component with ID 162");
            }

            if (simFramesCount != 0) {
                throw new Exception($"Given simFrames size is not equal to the expected length. ({simFramesCount} != 0) " +
                    "for component with ID 162");
            }

            var orig = new _daaa1fa3f3526944a84e601f43726765_6822197146557525417();

            var comp = (Interop*)data;

            orig.leftArmItemID = comp->leftArmItemID;
            orig.rightArmItemID = comp->rightArmItemID;
            orig.leftWeaponItemID = comp->leftWeaponItemID;
            orig.rightWeaponItemID = comp->rightWeaponItemID;

            return orig;
        }


        public static uint leftArmItemIDMask => 0b00000000000000000000000000000001;
        public AbsoluteSimulationFrame leftArmItemIDSimulationFrame;
        public System.Int32 leftArmItemID;
        public static uint rightArmItemIDMask => 0b00000000000000000000000000000010;
        public AbsoluteSimulationFrame rightArmItemIDSimulationFrame;
        public System.Int32 rightArmItemID;
        public static uint leftWeaponItemIDMask => 0b00000000000000000000000000000100;
        public AbsoluteSimulationFrame leftWeaponItemIDSimulationFrame;
        public System.Int32 leftWeaponItemID;
        public static uint rightWeaponItemIDMask => 0b00000000000000000000000000001000;
        public AbsoluteSimulationFrame rightWeaponItemIDSimulationFrame;
        public System.Int32 rightWeaponItemID;

        public uint FieldsMask { get; set; }
        public uint StoppedMask { get; set; }
        public uint GetComponentType() => 162;
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

        private static readonly System.Int32 _leftArmItemID_Min = -1;
        private static readonly System.Int32 _leftArmItemID_Max = 32;
        private static readonly System.Int32 _rightArmItemID_Min = -1;
        private static readonly System.Int32 _rightArmItemID_Max = 32;
        private static readonly System.Int32 _leftWeaponItemID_Min = -1;
        private static readonly System.Int32 _leftWeaponItemID_Max = 32;
        private static readonly System.Int32 _rightWeaponItemID_Min = -1;
        private static readonly System.Int32 _rightWeaponItemID_Max = 32;

        public AbsoluteSimulationFrame? GetMinSimulationFrame()
        {
            AbsoluteSimulationFrame? min = null;


            return min;
        }

        public ICoherenceComponentData MergeWith(ICoherenceComponentData data)
        {
            var other = (_daaa1fa3f3526944a84e601f43726765_6822197146557525417)data;
            var otherMask = other.FieldsMask;

            FieldsMask |= otherMask;
            StoppedMask &= ~(otherMask);

            if ((otherMask & 0x01) != 0)
            {
                this.leftArmItemIDSimulationFrame = other.leftArmItemIDSimulationFrame;
                this.leftArmItemID = other.leftArmItemID;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.rightArmItemIDSimulationFrame = other.rightArmItemIDSimulationFrame;
                this.rightArmItemID = other.rightArmItemID;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.leftWeaponItemIDSimulationFrame = other.leftWeaponItemIDSimulationFrame;
                this.leftWeaponItemID = other.leftWeaponItemID;
            }

            otherMask >>= 1;
            if ((otherMask & 0x01) != 0)
            {
                this.rightWeaponItemIDSimulationFrame = other.rightWeaponItemIDSimulationFrame;
                this.rightWeaponItemID = other.rightWeaponItemID;
            }

            otherMask >>= 1;
            StoppedMask |= other.StoppedMask;

            return this;
        }

        public uint DiffWith(ICoherenceComponentData data)
        {
            throw new System.NotSupportedException($"{nameof(DiffWith)} is not supported in Unity");
        }

        public static uint Serialize(_daaa1fa3f3526944a84e601f43726765_6822197146557525417 data, bool isRefSimFrameValid, AbsoluteSimulationFrame referenceSimulationFrame, IOutProtocolBitStream bitStream, Logger logger)
        {
            if (bitStream.WriteMask(data.StoppedMask != 0))
            {
                bitStream.WriteMaskBits(data.StoppedMask, 4);
            }

            var mask = data.FieldsMask;

            if (bitStream.WriteMask((mask & 0x01) != 0))
            {

                Coherence.Utils.Bounds.Check(data.leftArmItemID, _leftArmItemID_Min, _leftArmItemID_Max, "_daaa1fa3f3526944a84e601f43726765_6822197146557525417.leftArmItemID", logger);

                data.leftArmItemID = Coherence.Utils.Bounds.Clamp(data.leftArmItemID, _leftArmItemID_Min, _leftArmItemID_Max);

                var fieldValue = data.leftArmItemID;



                bitStream.WriteIntegerRange(fieldValue, 6, -1);
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {

                Coherence.Utils.Bounds.Check(data.rightArmItemID, _rightArmItemID_Min, _rightArmItemID_Max, "_daaa1fa3f3526944a84e601f43726765_6822197146557525417.rightArmItemID", logger);

                data.rightArmItemID = Coherence.Utils.Bounds.Clamp(data.rightArmItemID, _rightArmItemID_Min, _rightArmItemID_Max);

                var fieldValue = data.rightArmItemID;



                bitStream.WriteIntegerRange(fieldValue, 6, -1);
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {

                Coherence.Utils.Bounds.Check(data.leftWeaponItemID, _leftWeaponItemID_Min, _leftWeaponItemID_Max, "_daaa1fa3f3526944a84e601f43726765_6822197146557525417.leftWeaponItemID", logger);

                data.leftWeaponItemID = Coherence.Utils.Bounds.Clamp(data.leftWeaponItemID, _leftWeaponItemID_Min, _leftWeaponItemID_Max);

                var fieldValue = data.leftWeaponItemID;



                bitStream.WriteIntegerRange(fieldValue, 6, -1);
            }

            mask >>= 1;
            if (bitStream.WriteMask((mask & 0x01) != 0))
            {

                Coherence.Utils.Bounds.Check(data.rightWeaponItemID, _rightWeaponItemID_Min, _rightWeaponItemID_Max, "_daaa1fa3f3526944a84e601f43726765_6822197146557525417.rightWeaponItemID", logger);

                data.rightWeaponItemID = Coherence.Utils.Bounds.Clamp(data.rightWeaponItemID, _rightWeaponItemID_Min, _rightWeaponItemID_Max);

                var fieldValue = data.rightWeaponItemID;



                bitStream.WriteIntegerRange(fieldValue, 6, -1);
            }

            mask >>= 1;

            return mask;
        }

        public static _daaa1fa3f3526944a84e601f43726765_6822197146557525417 Deserialize(AbsoluteSimulationFrame referenceSimulationFrame, InProtocolBitStream bitStream)
        {
            var stoppedMask = (uint)0;
            if (bitStream.ReadMask())
            {
                stoppedMask = bitStream.ReadMaskBits(4);
            }

            var val = new _daaa1fa3f3526944a84e601f43726765_6822197146557525417();
            if (bitStream.ReadMask())
            {

                val.leftArmItemID = bitStream.ReadIntegerRange(6, -1);
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_6822197146557525417.leftArmItemIDMask;
            }
            if (bitStream.ReadMask())
            {

                val.rightArmItemID = bitStream.ReadIntegerRange(6, -1);
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_6822197146557525417.rightArmItemIDMask;
            }
            if (bitStream.ReadMask())
            {

                val.leftWeaponItemID = bitStream.ReadIntegerRange(6, -1);
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_6822197146557525417.leftWeaponItemIDMask;
            }
            if (bitStream.ReadMask())
            {

                val.rightWeaponItemID = bitStream.ReadIntegerRange(6, -1);
                val.FieldsMask |= _daaa1fa3f3526944a84e601f43726765_6822197146557525417.rightWeaponItemIDMask;
            }

            val.StoppedMask = stoppedMask;

            return val;
        }


        public override string ToString()
        {
            return $"_daaa1fa3f3526944a84e601f43726765_6822197146557525417(" +
                $" leftArmItemID: { this.leftArmItemID }" +
                $" rightArmItemID: { this.rightArmItemID }" +
                $" leftWeaponItemID: { this.leftWeaponItemID }" +
                $" rightWeaponItemID: { this.rightWeaponItemID }" +
                $" Mask: { System.Convert.ToString(FieldsMask, 2).PadLeft(4, '0') }, " +
                $"Stopped: { System.Convert.ToString(StoppedMask, 2).PadLeft(4, '0') })";
        }
    }


}