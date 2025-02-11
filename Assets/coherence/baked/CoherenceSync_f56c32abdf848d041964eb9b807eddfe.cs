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
    public class Binding_f56c32abdf848d041964eb9b807eddfe_9f4a33c03f184458b7752144471d43b4 : PositionBinding
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
    public class Binding_f56c32abdf848d041964eb9b807eddfe_2d20918f15074567b751c740e36ba5e8 : RotationBinding
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
    public class Binding_f56c32abdf848d041964eb9b807eddfe_e118ae1d321b45518e714fa9fe8ffd85 : ScaleBinding
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
    public class Binding_f56c32abdf848d041964eb9b807eddfe_045921bcf5c349a6b49c5df4e3ef7cc6 : Vector3Binding
    {   
        private global::RigidbodySync CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::RigidbodySync)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_5150605711725245781);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_5150605711725245781";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override UnityEngine.Vector3 Value
        {
            get { return (UnityEngine.Vector3)(CastedUnityComponent.velocity); }
            set { CastedUnityComponent.velocity = (UnityEngine.Vector3)(value); }
        }

        protected override (UnityEngine.Vector3 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).velocity;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).velocitySimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.velocity = Value;
            }
            else
            {
                update.velocity = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.velocitySimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_f56c32abdf848d041964eb9b807eddfe_1bcd061bbce74cd4b618ee5e2990e1c9 : Vector3Binding
    {   
        private global::RigidbodySync CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::RigidbodySync)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_5150605711725245781);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_5150605711725245781";
        public override uint FieldMask => 0b00000000000000000000000000000010;

        public override UnityEngine.Vector3 Value
        {
            get { return (UnityEngine.Vector3)(CastedUnityComponent.angularVelocity); }
            set { CastedUnityComponent.angularVelocity = (UnityEngine.Vector3)(value); }
        }

        protected override (UnityEngine.Vector3 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).angularVelocity;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).angularVelocitySimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.angularVelocity = Value;
            }
            else
            {
                update.angularVelocity = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.angularVelocitySimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_f56c32abdf848d041964eb9b807eddfe_a8303c163b8d4e49a7355156cc497235 : Vector3Binding
    {   
        private global::RigidbodySync CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::RigidbodySync)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_5150605711725245781);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_5150605711725245781";
        public override uint FieldMask => 0b00000000000000000000000000000100;

        public override UnityEngine.Vector3 Value
        {
            get { return (UnityEngine.Vector3)(CastedUnityComponent.position); }
            set { CastedUnityComponent.position = (UnityEngine.Vector3)(value); }
        }

        protected override (UnityEngine.Vector3 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).position;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).positionSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.position = Value;
            }
            else
            {
                update.position = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.positionSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_f56c32abdf848d041964eb9b807eddfe_5def4b633c3f4c2ead66b70c8e3571d2 : QuaternionBinding
    {   
        private global::RigidbodySync CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::RigidbodySync)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_5150605711725245781);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_5150605711725245781";
        public override uint FieldMask => 0b00000000000000000000000000001000;

        public override UnityEngine.Quaternion Value
        {
            get { return (UnityEngine.Quaternion)(CastedUnityComponent.rotation); }
            set { CastedUnityComponent.rotation = (UnityEngine.Quaternion)(value); }
        }

        protected override (UnityEngine.Quaternion value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).rotation;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).rotationSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.rotation = Value;
            }
            else
            {
                update.rotation = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.rotationSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_f56c32abdf848d041964eb9b807eddfe_b28fe16e3b74477c8d2b05dc7f1acde9 : DoubleBinding
    {   
        private global::RigidbodySync CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::RigidbodySync)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_5150605711725245781);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_5150605711725245781";
        public override uint FieldMask => 0b00000000000000000000000000010000;

        public override System.Double Value
        {
            get { return (System.Double)(CastedUnityComponent.dataUpdateTime); }
            set { CastedUnityComponent.dataUpdateTime = (System.Double)(value); }
        }

        protected override (System.Double value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).dataUpdateTime;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent).dataUpdateTimeSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_5150605711725245781)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.dataUpdateTime = Value;
            }
            else
            {
                update.dataUpdateTime = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.dataUpdateTimeSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_5150605711725245781();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_f56c32abdf848d041964eb9b807eddfe_d376ff7d068143109f4f3ea47cec9546 : StringBinding
    {   
        private global::Coherence.Toolkit.CoherenceNode CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::Coherence.Toolkit.CoherenceNode)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_2556207594499470627);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_2556207594499470627";
        public override uint FieldMask => 0b00000000000000000000000000000001;

        public override System.String Value
        {
            get { return (System.String)(CastedUnityComponent.path); }
            set { CastedUnityComponent.path = (System.String)(value); }
        }

        protected override (System.String value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)coherenceComponent).path;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)coherenceComponent).pathSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.path = Value;
            }
            else
            {
                update.path = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.pathSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_2556207594499470627();
        }    
    }
    
    [UnityEngine.Scripting.Preserve]
    public class Binding_f56c32abdf848d041964eb9b807eddfe_050f27fca3e54f65b9557140a5676d7d : IntBinding
    {   
        private global::Coherence.Toolkit.CoherenceNode CastedUnityComponent;

        protected override void OnBindingCloned()
        {
    	    CastedUnityComponent = (global::Coherence.Toolkit.CoherenceNode)UnityComponent;
        }

        public override global::System.Type CoherenceComponentType => typeof(_f56c32abdf848d041964eb9b807eddfe_2556207594499470627);
        public override string CoherenceComponentName => "_f56c32abdf848d041964eb9b807eddfe_2556207594499470627";
        public override uint FieldMask => 0b00000000000000000000000000000010;

        public override System.Int32 Value
        {
            get { return (System.Int32)(CastedUnityComponent.pathDirtyCounter); }
            set { CastedUnityComponent.pathDirtyCounter = (System.Int32)(value); }
        }

        protected override (System.Int32 value, AbsoluteSimulationFrame simFrame) ReadComponentData(ICoherenceComponentData coherenceComponent, Vector3 floatingOriginDelta)
        {
            var value = ((_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)coherenceComponent).pathDirtyCounter;

            var simFrame = ((_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)coherenceComponent).pathDirtyCounterSimulationFrame;
            
            return (value, simFrame);
        }

        public override ICoherenceComponentData WriteComponentData(ICoherenceComponentData coherenceComponent, AbsoluteSimulationFrame simFrame)
        {
            var update = (_f56c32abdf848d041964eb9b807eddfe_2556207594499470627)coherenceComponent;
            if (Interpolator.IsInterpolationNone)
            {
                update.pathDirtyCounter = Value;
            }
            else
            {
                update.pathDirtyCounter = GetInterpolatedAt(simFrame / InterpolationSettings.SimulationFramesPerSecond);
            }

            update.pathDirtyCounterSimulationFrame = simFrame;
            
            return update;
        }

        public override ICoherenceComponentData CreateComponentData()
        {
            return new _f56c32abdf848d041964eb9b807eddfe_2556207594499470627();
        }    
    }

    [UnityEngine.Scripting.Preserve]
    public class CoherenceSync_f56c32abdf848d041964eb9b807eddfe : CoherenceSyncBaked
    {
        private Entity entityId;
        private Logger logger = Coherence.Log.Log.GetLogger<CoherenceSync_f56c32abdf848d041964eb9b807eddfe>();
        
        
        
        private IClient client;
        private CoherenceBridge bridge;
        
        private readonly Dictionary<string, Binding> bakedValueBindings = new Dictionary<string, Binding>()
        {
            ["9f4a33c03f184458b7752144471d43b4"] = new Binding_f56c32abdf848d041964eb9b807eddfe_9f4a33c03f184458b7752144471d43b4(),
            ["2d20918f15074567b751c740e36ba5e8"] = new Binding_f56c32abdf848d041964eb9b807eddfe_2d20918f15074567b751c740e36ba5e8(),
            ["e118ae1d321b45518e714fa9fe8ffd85"] = new Binding_f56c32abdf848d041964eb9b807eddfe_e118ae1d321b45518e714fa9fe8ffd85(),
            ["045921bcf5c349a6b49c5df4e3ef7cc6"] = new Binding_f56c32abdf848d041964eb9b807eddfe_045921bcf5c349a6b49c5df4e3ef7cc6(),
            ["1bcd061bbce74cd4b618ee5e2990e1c9"] = new Binding_f56c32abdf848d041964eb9b807eddfe_1bcd061bbce74cd4b618ee5e2990e1c9(),
            ["a8303c163b8d4e49a7355156cc497235"] = new Binding_f56c32abdf848d041964eb9b807eddfe_a8303c163b8d4e49a7355156cc497235(),
            ["5def4b633c3f4c2ead66b70c8e3571d2"] = new Binding_f56c32abdf848d041964eb9b807eddfe_5def4b633c3f4c2ead66b70c8e3571d2(),
            ["b28fe16e3b74477c8d2b05dc7f1acde9"] = new Binding_f56c32abdf848d041964eb9b807eddfe_b28fe16e3b74477c8d2b05dc7f1acde9(),
            ["d376ff7d068143109f4f3ea47cec9546"] = new Binding_f56c32abdf848d041964eb9b807eddfe_d376ff7d068143109f4f3ea47cec9546(),
            ["050f27fca3e54f65b9557140a5676d7d"] = new Binding_f56c32abdf848d041964eb9b807eddfe_050f27fca3e54f65b9557140a5676d7d(),
        };
        
        private Dictionary<string, Action<CommandBinding, CommandsHandler>> bakedCommandBindings = new Dictionary<string, Action<CommandBinding, CommandsHandler>>();
        
        public CoherenceSync_f56c32abdf848d041964eb9b807eddfe()
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
                    logger.Warning($"CoherenceSync_f56c32abdf848d041964eb9b807eddfe Unhandled command: {command.GetType()}.");
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
            this.logger = logger.With<CoherenceSync_f56c32abdf848d041964eb9b807eddfe>();
            this.bridge = bridge;
            this.entityId = entityId;
            this.client = client;        
        }
    }

}