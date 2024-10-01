// Copyright (c) coherence ApS.
// For all coherence generated code, the coherence SDK license terms apply. See the license file in the coherence Package root folder for more information.

// <auto-generated>
// Generated file. DO NOT EDIT!
// </auto-generated>
namespace Coherence.Generated
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Coherence.Toolkit;
    using Coherence.Toolkit.Bindings;
    using Coherence.Entities;
    using Coherence.ProtocolDef;
    using Coherence.Brook;
    using Coherence.Toolkit.Bindings.ValueBindings;
    using Coherence.Toolkit.Bindings.TransformBindings;
    using Coherence.Connection;
    using Coherence.SimulationFrame;
    using Coherence.Interpolation;
    using Coherence.Log;
    using Logger = Coherence.Log.Logger;
    using UnityEngine.Scripting;
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_ddf545e2defc9334d82e1bfb3a54514a_afdd95ed2f444dd2b1be1ac8dfcbd925 : PositionBinding
    {   
        private global::UnityEngine.Transform CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(WorldPosition);
        public override string CoherenceComponentName => "WorldPosition";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override UnityEngine.Vector3 Value
        {
            get { return (UnityEngine.Vector3)(coherenceSync.coherencePosition); }
            set { coherenceSync.coherencePosition = (UnityEngine.Vector3)(value); }
        }

        protected override (UnityEngine.Vector3 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((WorldPosition)coherenceComponent).value;
            if (!coherenceSync.HasParentWithCoherenceSync) { value += floatingOriginDelta; }

            var simFrame = ((WorldPosition)coherenceComponent).valueSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (WorldPosition)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.value = Value;
            }
            else
            {
                update.value = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.valueSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new WorldPosition();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_ddf545e2defc9334d82e1bfb3a54514a_fa9df38f990b4ca1abb99740d40d9719 : RotationBinding
    {   
        private global::UnityEngine.Transform CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(WorldOrientation);
        public override string CoherenceComponentName => "WorldOrientation";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override UnityEngine.Quaternion Value
        {
            get { return (UnityEngine.Quaternion)(coherenceSync.coherenceRotation); }
            set { coherenceSync.coherenceRotation = (UnityEngine.Quaternion)(value); }
        }

        protected override (UnityEngine.Quaternion value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((WorldOrientation)coherenceComponent).value;

            var simFrame = ((WorldOrientation)coherenceComponent).valueSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (WorldOrientation)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.value = Value;
            }
            else
            {
                update.value = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.valueSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new WorldOrientation();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_ddf545e2defc9334d82e1bfb3a54514a_d17e573b69e04290956bf6ce6a16010b : ScaleBinding
    {   
        private global::UnityEngine.Transform CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::UnityEngine.Transform)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(GenericScale);
        public override string CoherenceComponentName => "GenericScale";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override UnityEngine.Vector3 Value
        {
            get { return (UnityEngine.Vector3)(coherenceSync.coherenceLocalScale); }
            set { coherenceSync.coherenceLocalScale = (UnityEngine.Vector3)(value); }
        }

        protected override (UnityEngine.Vector3 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((GenericScale)coherenceComponent).value;

            var simFrame = ((GenericScale)coherenceComponent).valueSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (GenericScale)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.value = Value;
            }
            else
            {
                update.value = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.valueSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new GenericScale();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_ddf545e2defc9334d82e1bfb3a54514a_e9dfd6bfcef24680809073a7909881c3 : IntBinding
    {   
        private global::PlayerPart CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::PlayerPart)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_ddf545e2defc9334d82e1bfb3a54514a_8181434535533552611);
        public override string CoherenceComponentName => "_ddf545e2defc9334d82e1bfb3a54514a_8181434535533552611";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override System.Int32 Value
        {
            get { return (System.Int32)(CastedUnityComponent.partIndex); }
            set { CastedUnityComponent.partIndex = (System.Int32)(value); }
        }

        protected override (System.Int32 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_ddf545e2defc9334d82e1bfb3a54514a_8181434535533552611)coherenceComponent).partIndex;

            var simFrame = ((_ddf545e2defc9334d82e1bfb3a54514a_8181434535533552611)coherenceComponent).partIndexSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_ddf545e2defc9334d82e1bfb3a54514a_8181434535533552611)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.partIndex = Value;
            }
            else
            {
                update.partIndex = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.partIndexSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _ddf545e2defc9334d82e1bfb3a54514a_8181434535533552611();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_ddf545e2defc9334d82e1bfb3a54514a_dcafd195c0f64117b2725de78b16688a : ByteArrayBinding
    {   
        private global::Coherence.Toolkit.PrefabSyncGroup CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::Coherence.Toolkit.PrefabSyncGroup)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_ddf545e2defc9334d82e1bfb3a54514a_8625299608525973422);
        public override string CoherenceComponentName => "_ddf545e2defc9334d82e1bfb3a54514a_8625299608525973422";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override System.Byte[] Value
        {
            get { return (System.Byte[])(CastedUnityComponent.ids); }
            set { CastedUnityComponent.ids = (System.Byte[])(value); }
        }

        protected override (System.Byte[] value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_ddf545e2defc9334d82e1bfb3a54514a_8625299608525973422)coherenceComponent).ids;

            var simFrame = ((_ddf545e2defc9334d82e1bfb3a54514a_8625299608525973422)coherenceComponent).idsSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_ddf545e2defc9334d82e1bfb3a54514a_8625299608525973422)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.ids = Value;
            }
            else
            {
                update.ids = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.idsSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _ddf545e2defc9334d82e1bfb3a54514a_8625299608525973422();
        }    
    }

    [UnityEngine.Scripting.Preserve]
    public class CoherenceSync_ddf545e2defc9334d82e1bfb3a54514a : CoherenceSyncBaked
    {
        private Entity entityId;
        private Logger logger = Coherence.Log.Log.GetLogger<CoherenceSync_ddf545e2defc9334d82e1bfb3a54514a>();
        
        
        
        private IClient client;
        private CoherenceBridge bridge;
        
        private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
        {
            ["afdd95ed2f444dd2b1be1ac8dfcbd925"] = new Binding_ddf545e2defc9334d82e1bfb3a54514a_afdd95ed2f444dd2b1be1ac8dfcbd925(),
            ["fa9df38f990b4ca1abb99740d40d9719"] = new Binding_ddf545e2defc9334d82e1bfb3a54514a_fa9df38f990b4ca1abb99740d40d9719(),
            ["d17e573b69e04290956bf6ce6a16010b"] = new Binding_ddf545e2defc9334d82e1bfb3a54514a_d17e573b69e04290956bf6ce6a16010b(),
            ["e9dfd6bfcef24680809073a7909881c3"] = new Binding_ddf545e2defc9334d82e1bfb3a54514a_e9dfd6bfcef24680809073a7909881c3(),
            ["dcafd195c0f64117b2725de78b16688a"] = new Binding_ddf545e2defc9334d82e1bfb3a54514a_dcafd195c0f64117b2725de78b16688a(),
        };
        
        private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings = new Dictionary<string, Action<CommandBinding, CommandsHandler>>();
        
        public CoherenceSync_ddf545e2defc9334d82e1bfb3a54514a()
        {
        }
        
        public override Binding BakeValueBinding(Binding valueBinding)
        {
            if (bakedValueBindings.TryGetValue(valueBinding.guid, out var bakedBinding))
            {
                valueBinding.CloneTo(bakedBinding);
                return bakedBinding;
            }
            
            return null;
        }
        
        public override void BakeCommandBinding(CommandBinding commandBinding, CommandsHandler commandsHandler)
        {
            if (bakedCommandBindings.TryGetValue(commandBinding.guid, out var commandBindingBaker))
            {
                commandBindingBaker.Invoke(commandBinding, commandsHandler);
            }
        }
        
        public override void ReceiveCommand(IEntityCommand command)
        {
            switch (command)
            {
                default:
                    logger.Warning($"CoherenceSync_ddf545e2defc9334d82e1bfb3a54514a Unhandled command: {command.GetType()}.");
                    break;
            }
        }
        
        public override List<ICoherenceComponentData> CreateEntity(bool usesLodsAtRuntime, string archetypeName, AbsoluteSimulationFrame simFrame)
        {
            if (!usesLodsAtRuntime)
            {
                return null;
            }
            
            if (Archetypes.IndexForName.TryGetValue(archetypeName, out int archetypeIndex))
            {
                var components = new List<ICoherenceComponentData>()
                {
                    new ArchetypeComponent
                    {
                        index = archetypeIndex,
                        indexSimulationFrame = simFrame,
                        FieldsMask = 0b1
                    }
                };

                return components;
            }
    
            logger.Warning($"Unable to find archetype {archetypeName} in dictionary. Please, bake manually (coherence > Bake)");
            
            return null;
        }
        
        public override void Dispose()
        {
        }
        
        public override void Initialize(Entity entityId, CoherenceBridge bridge, IClient client, CoherenceInput input, Logger logger)
        {
            this.logger = logger.With<CoherenceSync_ddf545e2defc9334d82e1bfb3a54514a>();
            this.bridge = bridge;
            this.entityId = entityId;
            this.client = client;        
        }
    }

}