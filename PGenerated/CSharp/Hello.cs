using PChecker;
using PChecker.Actors;
using PChecker.Actors.Events;
using PChecker.Runtime;
using PChecker.Specifications;
using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Plang.CSharpRuntime;
using Plang.CSharpRuntime.Values;
using Plang.CSharpRuntime.Exceptions;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 162, 219, 414, 1998
namespace PImplementation
{
}
namespace PImplementation
{
    internal partial class eStartBatteryFailSafe : PEvent
    {
        public eStartBatteryFailSafe() : base() {}
        public eStartBatteryFailSafe (PrtNamedTuple payload): base(payload){ }
        public override IPrtValue Clone() { return new eStartBatteryFailSafe();}
    }
}
namespace PImplementation
{
    internal partial class eUpdateBatteryPercentage : PEvent
    {
        public eUpdateBatteryPercentage() : base() {}
        public eUpdateBatteryPercentage (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eUpdateBatteryPercentage();}
    }
}
namespace PImplementation
{
    internal partial class eLanding : PEvent
    {
        public eLanding() : base() {}
        public eLanding (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eLanding();}
    }
}
namespace PImplementation
{
    internal partial class eLanded : PEvent
    {
        public eLanded() : base() {}
        public eLanded (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eLanded();}
    }
}
namespace PImplementation
{
    internal partial class eStartGeoFence : PEvent
    {
        public eStartGeoFence() : base() {}
        public eStartGeoFence (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eStartGeoFence();}
    }
}
namespace PImplementation
{
    internal partial class eRequestDroneMovement : PEvent
    {
        public eRequestDroneMovement() : base() {}
        public eRequestDroneMovement (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eRequestDroneMovement();}
    }
}
namespace PImplementation
{
    internal partial class eDroneMovementResponse : PEvent
    {
        public eDroneMovementResponse() : base() {}
        public eDroneMovementResponse (PrtNamedTuple payload): base(payload){ }
        public override IPrtValue Clone() { return new eDroneMovementResponse();}
    }
}
namespace PImplementation
{
    internal partial class eFenceReached : PEvent
    {
        public eFenceReached() : base() {}
        public eFenceReached (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eFenceReached();}
    }
}
namespace PImplementation
{
    internal partial class eFenceDistanced : PEvent
    {
        public eFenceDistanced() : base() {}
        public eFenceDistanced (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eFenceDistanced();}
    }
}
namespace PImplementation
{
    internal partial class eDecrementAltitude : PEvent
    {
        public eDecrementAltitude() : base() {}
        public eDecrementAltitude (IPrtValue payload): base(payload){ }
        public override IPrtValue Clone() { return new eDecrementAltitude();}
    }
}
namespace PImplementation
{
    internal partial class Battery : PMachine
    {
        private PrtInt battery_percentage = ((PrtInt)0);
        private PrtInt landing_threshold = ((PrtInt)0);
        private PMachineValue geofence_model = null;
        public class ConstructorEvent : PEvent{public ConstructorEvent(PMachineValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPrtValue value) { return new ConstructorEvent((PMachineValue)value); }
        public Battery() {
            this.sends.Add(nameof(eDecrementAltitude));
            this.sends.Add(nameof(eDroneMovementResponse));
            this.sends.Add(nameof(eFenceDistanced));
            this.sends.Add(nameof(eFenceReached));
            this.sends.Add(nameof(eLanded));
            this.sends.Add(nameof(eLanding));
            this.sends.Add(nameof(eRequestDroneMovement));
            this.sends.Add(nameof(eStartBatteryFailSafe));
            this.sends.Add(nameof(eStartGeoFence));
            this.sends.Add(nameof(eUpdateBatteryPercentage));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(eDecrementAltitude));
            this.receives.Add(nameof(eDroneMovementResponse));
            this.receives.Add(nameof(eFenceDistanced));
            this.receives.Add(nameof(eFenceReached));
            this.receives.Add(nameof(eLanded));
            this.receives.Add(nameof(eLanding));
            this.receives.Add(nameof(eRequestDroneMovement));
            this.receives.Add(nameof(eStartBatteryFailSafe));
            this.receives.Add(nameof(eStartGeoFence));
            this.receives.Add(nameof(eUpdateBatteryPercentage));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon(Event currentMachine_dequeuedEvent)
        {
            Battery currentMachine = this;
            PMachineValue geofence_model_local = (PMachineValue)(gotoPayload ?? ((PEvent)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PrtString TMP_tmp0 = ((PrtString)"");
            PMachineValue TMP_tmp1 = null;
            PEvent TMP_tmp2 = null;
            PrtInt TMP_tmp3 = ((PrtInt)0);
            PrtNamedTuple TMP_tmp4 = (new PrtNamedTuple(new string[]{"landing_threshold_amount"},((PrtInt)0)));
            battery_percentage = (PrtInt)(((PrtInt)100));
            landing_threshold = (PrtInt)(((PrtInt)20));
            geofence_model = (PMachineValue)(((PMachineValue)((IPrtValue)geofence_model_local)?.Clone()));
            TMP_tmp0 = (PrtString)(((PrtString) String.Format("Battery Failsafe Feature Enabled!")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0);
            TMP_tmp1 = (PMachineValue)(currentMachine.self);
            TMP_tmp2 = (PEvent)(new eStartBatteryFailSafe((new PrtNamedTuple(new string[]{"landing_threshold_amount"},((PrtInt)0)))));
            TMP_tmp3 = (PrtInt)(((PrtInt)((IPrtValue)landing_threshold)?.Clone()));
            TMP_tmp4 = (PrtNamedTuple)((new PrtNamedTuple(new string[]{"landing_threshold_amount"}, TMP_tmp3)));
            currentMachine.TrySendEvent(TMP_tmp1, (Event)TMP_tmp2, TMP_tmp4);
            currentMachine.TryGotoState<Monitoring>();
            return;
        }
        public void Anon_1(Event currentMachine_dequeuedEvent)
        {
            Battery currentMachine = this;
            PrtInt TMP_tmp0_1 = ((PrtInt)0);
            PrtInt TMP_tmp1_1 = ((PrtInt)0);
            PrtString TMP_tmp2_1 = ((PrtString)"");
            PrtBool TMP_tmp3_1 = ((PrtBool)false);
            PrtString TMP_tmp4_1 = ((PrtString)"");
            PMachineValue TMP_tmp5 = null;
            PEvent TMP_tmp6 = null;
            PMachineValue TMP_tmp7 = null;
            PEvent TMP_tmp8 = null;
            PrtString TMP_tmp9 = ((PrtString)"");
            PMachineValue TMP_tmp10 = null;
            PEvent TMP_tmp11 = null;
            TMP_tmp0_1 = (PrtInt)((battery_percentage) - (((PrtInt)1)));
            battery_percentage = TMP_tmp0_1;
            TMP_tmp1_1 = (PrtInt)(((PrtInt)((IPrtValue)battery_percentage)?.Clone()));
            TMP_tmp2_1 = (PrtString)(((PrtString) String.Format("battery updated, battery = {0}",TMP_tmp1_1)));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp2_1);
            TMP_tmp3_1 = (PrtBool)((battery_percentage) < (landing_threshold));
            if (TMP_tmp3_1)
            {
                TMP_tmp4_1 = (PrtString)(((PrtString) String.Format("battery below threshold, landing initiated!")));
                PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp4_1);
                TMP_tmp5 = (PMachineValue)(currentMachine.self);
                TMP_tmp6 = (PEvent)(new eLanding(null));
                currentMachine.TrySendEvent(TMP_tmp5, (Event)TMP_tmp6);
                TMP_tmp7 = (PMachineValue)(((PMachineValue)((IPrtValue)geofence_model)?.Clone()));
                TMP_tmp8 = (PEvent)(new eLanding(null));
                currentMachine.TrySendEvent(TMP_tmp7, (Event)TMP_tmp8);
                currentMachine.TryGotoState<Landing>();
                return;
            }
            else
            {
                TMP_tmp9 = (PrtString)(((PrtString) String.Format("battery above threshold, continue monitoring!")));
                PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp9);
                TMP_tmp10 = (PMachineValue)(currentMachine.self);
                TMP_tmp11 = (PEvent)(new eUpdateBatteryPercentage(null));
                currentMachine.TrySendEvent(TMP_tmp10, (Event)TMP_tmp11);
                currentMachine.TryGotoState<Monitoring>();
                return;
            }
        }
        public void Anon_2(Event currentMachine_dequeuedEvent)
        {
            Battery currentMachine = this;
            PrtString TMP_tmp0_2 = ((PrtString)"");
            PMachineValue TMP_tmp1_2 = null;
            PEvent TMP_tmp2_2 = null;
            TMP_tmp0_2 = (PrtString)(((PrtString) String.Format("Landing initiated!")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0_2);
            TMP_tmp1_2 = (PMachineValue)(((PMachineValue)((IPrtValue)geofence_model)?.Clone()));
            TMP_tmp2_2 = (PEvent)(new eLanding(null));
            currentMachine.TrySendEvent(TMP_tmp1_2, (Event)TMP_tmp2_2);
            currentMachine.TryGotoState<Landed>();
            return;
        }
        public void Anon_3(Event currentMachine_dequeuedEvent)
        {
            Battery currentMachine = this;
            PrtString TMP_tmp0_3 = ((PrtString)"");
            TMP_tmp0_3 = (PrtString)(((PrtString) String.Format("Landed!")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0_3);
        }
        [Start]
        [OnEntry(nameof(InitializeParametersFunction))]
        [OnEventGotoState(typeof(ConstructorEvent), typeof(Init))]
        class __InitState__ : State { }
        
        [OnEntry(nameof(Anon))]
        class Init : State
        {
        }
        [OnEventDoAction(typeof(eStartBatteryFailSafe), nameof(Anon_1))]
        [OnEventDoAction(typeof(eUpdateBatteryPercentage), nameof(Anon_1))]
        class Monitoring : State
        {
        }
        [OnEventDoAction(typeof(eLanding), nameof(Anon_2))]
        class Landing : State
        {
        }
        [OnEntry(nameof(Anon_3))]
        class Landed : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class GeoFence : PMachine
    {
        private PrtInt fence_alt_min = ((PrtInt)0);
        private PrtInt fence_alt_max = ((PrtInt)0);
        private PrtInt fence_radius = ((PrtInt)0);
        private PrtInt drone_horizontal_distance_to_origin = ((PrtInt)0);
        private PrtInt drone_altitude = ((PrtInt)0);
        public class ConstructorEvent : PEvent{public ConstructorEvent(IPrtValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPrtValue value) { return new ConstructorEvent((IPrtValue)value); }
        public GeoFence() {
            this.sends.Add(nameof(eDecrementAltitude));
            this.sends.Add(nameof(eDroneMovementResponse));
            this.sends.Add(nameof(eFenceDistanced));
            this.sends.Add(nameof(eFenceReached));
            this.sends.Add(nameof(eLanded));
            this.sends.Add(nameof(eLanding));
            this.sends.Add(nameof(eRequestDroneMovement));
            this.sends.Add(nameof(eStartBatteryFailSafe));
            this.sends.Add(nameof(eStartGeoFence));
            this.sends.Add(nameof(eUpdateBatteryPercentage));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(eDecrementAltitude));
            this.receives.Add(nameof(eDroneMovementResponse));
            this.receives.Add(nameof(eFenceDistanced));
            this.receives.Add(nameof(eFenceReached));
            this.receives.Add(nameof(eLanded));
            this.receives.Add(nameof(eLanding));
            this.receives.Add(nameof(eRequestDroneMovement));
            this.receives.Add(nameof(eStartBatteryFailSafe));
            this.receives.Add(nameof(eStartGeoFence));
            this.receives.Add(nameof(eUpdateBatteryPercentage));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon_4(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtString TMP_tmp0_4 = ((PrtString)"");
            PMachineValue TMP_tmp1_3 = null;
            PEvent TMP_tmp2_3 = null;
            fence_alt_min = (PrtInt)(((PrtInt)0));
            fence_alt_max = (PrtInt)(((PrtInt)100));
            fence_radius = (PrtInt)(((PrtInt)100));
            drone_horizontal_distance_to_origin = (PrtInt)(((PrtInt)0));
            drone_altitude = (PrtInt)(((PrtInt)0));
            TMP_tmp0_4 = (PrtString)(((PrtString) String.Format("GeoFence Feature Enabled!")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0_4);
            TMP_tmp1_3 = (PMachineValue)(currentMachine.self);
            TMP_tmp2_3 = (PEvent)(new eStartGeoFence(null));
            currentMachine.TrySendEvent(TMP_tmp1_3, (Event)TMP_tmp2_3);
            currentMachine.TryGotoState<Monitoring_1>();
            return;
        }
        public void Anon_5(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PMachineValue TMP_tmp0_5 = null;
            PEvent TMP_tmp1_4 = null;
            TMP_tmp0_5 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_4 = (PEvent)(new eRequestDroneMovement(null));
            currentMachine.TrySendEvent(TMP_tmp0_5, (Event)TMP_tmp1_4);
            currentMachine.TryGotoState<GenerateMovement>();
            return;
        }
        public void Anon_6(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            currentMachine.TryGotoState<Landing_1>();
            return;
        }
        public void Anon_7(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtNamedTuple response = (PrtNamedTuple)(gotoPayload ?? ((PEvent)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PrtInt TMP_tmp0_6 = ((PrtInt)0);
            PrtInt TMP_tmp1_5 = ((PrtInt)0);
            PrtBool TMP_tmp2_4 = ((PrtBool)false);
            PMachineValue TMP_tmp3_2 = null;
            PEvent TMP_tmp4_2 = null;
            PrtInt TMP_tmp5_1 = ((PrtInt)0);
            PrtInt TMP_tmp6_1 = ((PrtInt)0);
            PrtBool TMP_tmp7_1 = ((PrtBool)false);
            PrtInt TMP_tmp8_1 = ((PrtInt)0);
            PrtInt TMP_tmp9_1 = ((PrtInt)0);
            PrtBool TMP_tmp10_1 = ((PrtBool)false);
            PrtInt TMP_tmp11_1 = ((PrtInt)0);
            PrtInt TMP_tmp12 = ((PrtInt)0);
            PrtBool TMP_tmp13 = ((PrtBool)false);
            PrtBool TMP_tmp14 = ((PrtBool)false);
            PMachineValue TMP_tmp15 = null;
            PEvent TMP_tmp16 = null;
            PrtInt TMP_tmp17 = ((PrtInt)0);
            PrtInt TMP_tmp18 = ((PrtInt)0);
            PrtInt TMP_tmp19 = ((PrtInt)0);
            PrtInt TMP_tmp20 = ((PrtInt)0);
            PMachineValue TMP_tmp21 = null;
            PEvent TMP_tmp22 = null;
            TMP_tmp0_6 = (PrtInt)(((PrtNamedTuple)response)["horizontal_movement"]);
            TMP_tmp1_5 = (PrtInt)((drone_horizontal_distance_to_origin) + (TMP_tmp0_6));
            TMP_tmp2_4 = (PrtBool)((TMP_tmp1_5) > (fence_radius));
            if (TMP_tmp2_4)
            {
                TMP_tmp3_2 = (PMachineValue)(currentMachine.self);
                TMP_tmp4_2 = (PEvent)(new eFenceReached(null));
                currentMachine.TrySendEvent(TMP_tmp3_2, (Event)TMP_tmp4_2);
                currentMachine.TryGotoState<Holding>();
                return;
            }
            else
            {
                TMP_tmp5_1 = (PrtInt)(((PrtNamedTuple)response)["horizontal_movement"]);
                TMP_tmp6_1 = (PrtInt)((drone_horizontal_distance_to_origin) + (TMP_tmp5_1));
                TMP_tmp7_1 = (PrtBool)((TMP_tmp6_1) < (((PrtInt)0)));
                if (TMP_tmp7_1)
                {
                }
                else
                {
                    TMP_tmp8_1 = (PrtInt)(((PrtNamedTuple)response)["vertical_movement"]);
                    TMP_tmp9_1 = (PrtInt)((drone_altitude) + (TMP_tmp8_1));
                    TMP_tmp10_1 = (PrtBool)((TMP_tmp9_1) < (fence_alt_min));
                    TMP_tmp14 = (PrtBool)(((PrtBool)((IPrtValue)TMP_tmp10_1)?.Clone()));
                    if (TMP_tmp14)
                    {
                    }
                    else
                    {
                        TMP_tmp11_1 = (PrtInt)(((PrtNamedTuple)response)["vertical_movement"]);
                        TMP_tmp12 = (PrtInt)((drone_altitude) + (TMP_tmp11_1));
                        TMP_tmp13 = (PrtBool)((TMP_tmp12) > (fence_alt_max));
                        TMP_tmp14 = (PrtBool)(((PrtBool)((IPrtValue)TMP_tmp13)?.Clone()));
                    }
                    if (TMP_tmp14)
                    {
                        TMP_tmp15 = (PMachineValue)(currentMachine.self);
                        TMP_tmp16 = (PEvent)(new eFenceReached(null));
                        currentMachine.TrySendEvent(TMP_tmp15, (Event)TMP_tmp16);
                        currentMachine.TryGotoState<Holding>();
                        return;
                    }
                    else
                    {
                        TMP_tmp17 = (PrtInt)(((PrtNamedTuple)response)["vertical_movement"]);
                        TMP_tmp18 = (PrtInt)((drone_altitude) + (TMP_tmp17));
                        drone_altitude = TMP_tmp18;
                        TMP_tmp19 = (PrtInt)(((PrtNamedTuple)response)["horizontal_movement"]);
                        TMP_tmp20 = (PrtInt)((drone_horizontal_distance_to_origin) + (TMP_tmp19));
                        drone_horizontal_distance_to_origin = TMP_tmp20;
                        TMP_tmp21 = (PMachineValue)(currentMachine.self);
                        TMP_tmp22 = (PEvent)(new eRequestDroneMovement(null));
                        currentMachine.TrySendEvent(TMP_tmp21, (Event)TMP_tmp22);
                        currentMachine.TryGotoState<GenerateMovement>();
                        return;
                    }
                }
            }
        }
        public void Anon_8(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtString TMP_tmp0_7 = ((PrtString)"");
            PMachineValue TMP_tmp1_6 = null;
            PEvent TMP_tmp2_5 = null;
            TMP_tmp0_7 = (PrtString)(((PrtString) String.Format("Fence Reached! Starting Holding Pattern")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0_7);
            TMP_tmp1_6 = (PMachineValue)(currentMachine.self);
            TMP_tmp2_5 = (PEvent)(new eFenceDistanced(null));
            currentMachine.TrySendEvent(TMP_tmp1_6, (Event)TMP_tmp2_5);
            currentMachine.TryGotoState<Monitoring_1>();
            return;
        }
        public void Anon_9(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            currentMachine.TryGotoState<Landing_1>();
            return;
        }
        public void Anon_10(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtNamedTuple response_1 = (new PrtNamedTuple(new string[]{"horizontal_movement","vertical_movement"},((PrtInt)0), ((PrtInt)0)));
            PrtInt TMP_tmp0_8 = ((PrtInt)0);
            PrtInt TMP_tmp1_7 = ((PrtInt)0);
            PrtInt TMP_tmp2_6 = ((PrtInt)0);
            PrtInt TMP_tmp3_3 = ((PrtInt)0);
            PrtInt TMP_tmp4_3 = ((PrtInt)0);
            PrtInt TMP_tmp5_2 = ((PrtInt)0);
            PrtString TMP_tmp6_2 = ((PrtString)"");
            PMachineValue TMP_tmp7_2 = null;
            PEvent TMP_tmp8_2 = null;
            PrtNamedTuple TMP_tmp9_2 = (new PrtNamedTuple(new string[]{"horizontal_movement","vertical_movement"},((PrtInt)0), ((PrtInt)0)));
            TMP_tmp0_8 = (PrtInt)(((PrtInt)currentMachine.TryRandom(((PrtInt)20))));
            TMP_tmp1_7 = (PrtInt)((TMP_tmp0_8) - (((PrtInt)10)));
            ((PrtNamedTuple)response_1)["horizontal_movement"] = TMP_tmp1_7;
            TMP_tmp2_6 = (PrtInt)(((PrtInt)currentMachine.TryRandom(((PrtInt)20))));
            TMP_tmp3_3 = (PrtInt)((TMP_tmp2_6) - (((PrtInt)10)));
            ((PrtNamedTuple)response_1)["vertical_movement"] = TMP_tmp3_3;
            TMP_tmp4_3 = (PrtInt)(((PrtNamedTuple)response_1)["horizontal_movement"]);
            TMP_tmp5_2 = (PrtInt)(((PrtNamedTuple)response_1)["vertical_movement"]);
            TMP_tmp6_2 = (PrtString)(((PrtString) String.Format("Movement Generated, horizontal movement = {0}, vertical movement = {1}",TMP_tmp4_3,TMP_tmp5_2)));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp6_2);
            TMP_tmp7_2 = (PMachineValue)(currentMachine.self);
            TMP_tmp8_2 = (PEvent)(new eDroneMovementResponse((new PrtNamedTuple(new string[]{"horizontal_movement","vertical_movement"},((PrtInt)0), ((PrtInt)0)))));
            TMP_tmp9_2 = (PrtNamedTuple)(((PrtNamedTuple)((IPrtValue)response_1)?.Clone()));
            currentMachine.TrySendEvent(TMP_tmp7_2, (Event)TMP_tmp8_2, TMP_tmp9_2);
            currentMachine.TryGotoState<Monitoring_1>();
            return;
        }
        public void Anon_11(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtString TMP_tmp0_9 = ((PrtString)"");
            TMP_tmp0_9 = (PrtString)(((PrtString) String.Format("Landing! Horizonal Distance Remain Unchanged")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0_9);
        }
        public void Anon_12(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtInt TMP_tmp0_10 = ((PrtInt)0);
            PrtBool TMP_tmp1_8 = ((PrtBool)false);
            PMachineValue TMP_tmp2_7 = null;
            PEvent TMP_tmp3_4 = null;
            PMachineValue TMP_tmp4_4 = null;
            PEvent TMP_tmp5_3 = null;
            TMP_tmp0_10 = (PrtInt)((drone_altitude) - (((PrtInt)1)));
            drone_altitude = TMP_tmp0_10;
            TMP_tmp1_8 = (PrtBool)((PrtValues.SafeEquals(drone_altitude,((PrtInt)0))));
            if (TMP_tmp1_8)
            {
                TMP_tmp2_7 = (PMachineValue)(currentMachine.self);
                TMP_tmp3_4 = (PEvent)(new eLanded(null));
                currentMachine.TrySendEvent(TMP_tmp2_7, (Event)TMP_tmp3_4);
                currentMachine.TryGotoState<Landed_1>();
                return;
            }
            else
            {
                TMP_tmp4_4 = (PMachineValue)(currentMachine.self);
                TMP_tmp5_3 = (PEvent)(new eDecrementAltitude(null));
                currentMachine.TrySendEvent(TMP_tmp4_4, (Event)TMP_tmp5_3);
            }
        }
        public void Anon_13(Event currentMachine_dequeuedEvent)
        {
            GeoFence currentMachine = this;
            PrtString TMP_tmp0_11 = ((PrtString)"");
            TMP_tmp0_11 = (PrtString)(((PrtString) String.Format("Landed! Altitude Set to 0")));
            PModule.runtime.Logger.WriteLine("<PrintLog> " + TMP_tmp0_11);
            drone_altitude = (PrtInt)(((PrtInt)0));
        }
        [Start]
        [OnEntry(nameof(InitializeParametersFunction))]
        [OnEventGotoState(typeof(ConstructorEvent), typeof(Init_1))]
        class __InitState__ : State { }
        
        [OnEntry(nameof(Anon_4))]
        class Init_1 : State
        {
        }
        [OnEventDoAction(typeof(eStartGeoFence), nameof(Anon_5))]
        [OnEventDoAction(typeof(eFenceDistanced), nameof(Anon_5))]
        [OnEventDoAction(typeof(eLanding), nameof(Anon_6))]
        [OnEventDoAction(typeof(eDroneMovementResponse), nameof(Anon_7))]
        class Monitoring_1 : State
        {
        }
        [OnEventDoAction(typeof(eFenceReached), nameof(Anon_8))]
        [OnEventDoAction(typeof(eLanding), nameof(Anon_9))]
        class Holding : State
        {
        }
        [OnEventDoAction(typeof(eRequestDroneMovement), nameof(Anon_10))]
        class GenerateMovement : State
        {
        }
        [OnEntry(nameof(Anon_11))]
        [OnEventDoAction(typeof(eDecrementAltitude), nameof(Anon_12))]
        class Landing_1 : State
        {
        }
        [OnEntry(nameof(Anon_13))]
        class Landed_1 : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class Test_Original_Model : PMachine
    {
        public class ConstructorEvent : PEvent{public ConstructorEvent(IPrtValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPrtValue value) { return new ConstructorEvent((IPrtValue)value); }
        public Test_Original_Model() {
            this.sends.Add(nameof(eDecrementAltitude));
            this.sends.Add(nameof(eDroneMovementResponse));
            this.sends.Add(nameof(eFenceDistanced));
            this.sends.Add(nameof(eFenceReached));
            this.sends.Add(nameof(eLanded));
            this.sends.Add(nameof(eLanding));
            this.sends.Add(nameof(eRequestDroneMovement));
            this.sends.Add(nameof(eStartBatteryFailSafe));
            this.sends.Add(nameof(eStartGeoFence));
            this.sends.Add(nameof(eUpdateBatteryPercentage));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(eDecrementAltitude));
            this.receives.Add(nameof(eDroneMovementResponse));
            this.receives.Add(nameof(eFenceDistanced));
            this.receives.Add(nameof(eFenceReached));
            this.receives.Add(nameof(eLanded));
            this.receives.Add(nameof(eLanding));
            this.receives.Add(nameof(eRequestDroneMovement));
            this.receives.Add(nameof(eStartBatteryFailSafe));
            this.receives.Add(nameof(eStartGeoFence));
            this.receives.Add(nameof(eUpdateBatteryPercentage));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Battery));
            this.creates.Add(nameof(I_GeoFence));
        }
        
        public void Anon_14(Event currentMachine_dequeuedEvent)
        {
            Test_Original_Model currentMachine = this;
            PMachineValue TMP_tmp0_12 = null;
            TMP_tmp0_12 = (PMachineValue)(currentMachine.CreateInterface<I_GeoFence>( currentMachine));
            currentMachine.CreateInterface<I_Battery>(currentMachine, TMP_tmp0_12);
        }
        [Start]
        [OnEntry(nameof(InitializeParametersFunction))]
        [OnEventGotoState(typeof(ConstructorEvent), typeof(Init_2))]
        class __InitState__ : State { }
        
        [OnEntry(nameof(Anon_14))]
        class Init_2 : State
        {
        }
    }
}
namespace PImplementation
{
    public class original {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Battery)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_GeoFence)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Test_Original_Model)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Test_Original_Model)].Add(nameof(I_Battery), nameof(I_Battery));
            PModule.linkMap[nameof(I_Test_Original_Model)].Add(nameof(I_GeoFence), nameof(I_GeoFence));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Battery), typeof(Battery));
            PModule.interfaceDefinitionMap.Add(nameof(I_GeoFence), typeof(GeoFence));
            PModule.interfaceDefinitionMap.Add(nameof(I_Test_Original_Model), typeof(Test_Original_Model));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
        }
        
        public static void InitializeMonitorMap(IActorRuntime runtime) {
            PModule.monitorMap.Clear();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(IActorRuntime runtime) {
            runtime.RegisterLog(new PLogFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateActor(typeof(_GodMachine), new _GodMachine.Config(typeof(Test_Original_Model)));
        }
    }
}
// TODO: NamedModule Battery_1
// TODO: NamedModule GeoFence_1
namespace PImplementation
{
    public class I_Battery : PMachineValue {
        public I_Battery (ActorId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_GeoFence : PMachineValue {
        public I_GeoFence (ActorId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_Test_Original_Model : PMachineValue {
        public I_Test_Original_Model (ActorId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public partial class PHelper {
        public static void InitializeInterfaces() {
            PInterfaces.Clear();
            PInterfaces.AddInterface(nameof(I_Battery), nameof(eDecrementAltitude), nameof(eDroneMovementResponse), nameof(eFenceDistanced), nameof(eFenceReached), nameof(eLanded), nameof(eLanding), nameof(eRequestDroneMovement), nameof(eStartBatteryFailSafe), nameof(eStartGeoFence), nameof(eUpdateBatteryPercentage), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_GeoFence), nameof(eDecrementAltitude), nameof(eDroneMovementResponse), nameof(eFenceDistanced), nameof(eFenceReached), nameof(eLanded), nameof(eLanding), nameof(eRequestDroneMovement), nameof(eStartBatteryFailSafe), nameof(eStartGeoFence), nameof(eUpdateBatteryPercentage), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_Test_Original_Model), nameof(eDecrementAltitude), nameof(eDroneMovementResponse), nameof(eFenceDistanced), nameof(eFenceReached), nameof(eLanded), nameof(eLanding), nameof(eRequestDroneMovement), nameof(eStartBatteryFailSafe), nameof(eStartGeoFence), nameof(eUpdateBatteryPercentage), nameof(PHalt));
        }
    }
    
}
namespace PImplementation
{
    public partial class PHelper {
        public static void InitializeEnums() {
            PrtEnum.Clear();
        }
    }
    
}
#pragma warning restore 162, 219, 414
