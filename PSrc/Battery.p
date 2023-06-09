// Date: 2023-05-05 10:01:48
// 
// Label: Original Model for Battery Fail Safe Feature
//
// Purpose: BatteryFailSafe machine ensures if a drone's battery dips below certain threshold
// it will land safely.
//
// Implementation: This machine has a built-in battery model, and it is standalone,
// given the initial battery percentage and landing_threshold.
//
// Properties:
// safety property: when the battery deplets, the drone should always land at
//                  the origin.
// liveness property: the drone eventually lands at the origin

// broadcast the landing threshold amount
type tStartBatteryFailSafe =  (landing_threshold_amount : int);

event eStartBatteryFailSafe: tStartBatteryFailSafe;
event eUpdateBatteryPercentage;
event eLanding;
event eLanded;

machine Battery
{
  // indicate the battery percentage limit where landing is required.
  var battery_percentage : int;
  var landing_threshold : int;
  var geofence_model : GeoFence;

  start state Init {
    entry (geofence_model_local : GeoFence) {
      // set initial battery percentage
      battery_percentage = 100;
      landing_threshold = 20; 
      geofence_model = geofence_model_local;

      print "Battery Failsafe Feature Enabled!";

      // broadcast the landing threshold amount
      send this, eStartBatteryFailSafe, (landing_threshold_amount = landing_threshold,);
      goto Monitoring;
    }
  }

  // monitor and update battery percentage
  // determines if the drone needs to land
  state Monitoring {
    on eStartBatteryFailSafe, eUpdateBatteryPercentage do {

      // update battery percentage
      battery_percentage = battery_percentage - 1;

      print format ("battery updated, battery = {0}", battery_percentage);

      // if battery percentage is below landing threshold, land
      if (battery_percentage < landing_threshold) {
        print "battery below threshold, landing initiated!";
        send this, eLanding;
        send geofence_model, eLanding;
        goto Landing;
      }

      // otherwise, continue monitoring
      else {
        print "battery above threshold, continue monitoring!";
        send this, eUpdateBatteryPercentage;
        goto Monitoring;
      }
    }
  }

  state Landing {
    on eLanding do {
      print "Landing initiated!";
      send geofence_model, eLanding; // separate sequence for landing
      goto Landed;
    }
  }

  state Landed {
    entry {
      // terminating state
      print "Landed!";
    }
  }
}