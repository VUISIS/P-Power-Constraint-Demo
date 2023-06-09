// Date: 2023-05-05 10:01:48
// 
// Label: Original Model for GeoFence feature
//
// Purpose: GenFence prevents the drone from getting into unsafe region of the map
// it will start corresponding fail safe actions (like hold, return to launch, or land)
// in this case, it will start the holding pattern.
//
// Implementation: The machine has a simplistic built-in movement model, which increments the distance to origin
// by 1 unit every 1 second. The machine will start the holding pattern when the distance to origin is greater than
// the maximum distance allowed (defined by the genfence)
//
// Properties to check:
// safety property: drone should never cross the Fence
//

type tDroneMovementResponse = (horizontal_movement : int, vertical_movement : int);

event eStartGeoFence;
event eRequestDroneMovement;
event eDroneMovementResponse: tDroneMovementResponse;
event eFenceReached;
event eFenceDistanced;
event eDecrementAltitude;

machine GeoFence
{
  // fence settings
  var fence_alt_min : int;
  var fence_alt_max : int;
  var fence_radius : int; // in meters, cetnered around the home location

  // drone states
  var drone_horizontal_distance_to_origin : int; // in meters
  var drone_altitude : int; // in meters

  start state Init {
    entry {
      // define the fence boundary
      fence_alt_min = 0;
      fence_alt_max = 100;
      fence_radius = 100;

      // start the drone at home
      drone_horizontal_distance_to_origin = 0;
      drone_altitude = 0;

      print "GeoFence Feature Enabled!";

      send this, eStartGeoFence;
      goto Monitoring;
    }
  }

  state Monitoring {
    on eStartGeoFence, eFenceDistanced do {
      // start the drone movement generator
      send this, eRequestDroneMovement;
      goto GenerateMovement;
    }
      
    on eLanding do {
      // start landing sequence
      goto Landing;
    }

    on eDroneMovementResponse do (response : tDroneMovementResponse) {
      // **********************************************************
      // *** COMMENT THE CHECKS BELOW TO SEE THE COUNTEREXAMPLE ***
      // **********************************************************

      // check if the drone is within the fence
      // exceeded fence radius
      if (drone_horizontal_distance_to_origin + response.horizontal_movement > fence_radius) {
        // drone is outside the fence, start holding pattern
        send this, eFenceReached;
        goto Holding;
      } 
      
      // note that the drone's distance to horizon cannot be negative
      else if (drone_horizontal_distance_to_origin + response.horizontal_movement < 0) {
        // do nothing 
      } 

      // exceeded fence altitude
      else if (drone_altitude + response.vertical_movement < fence_alt_min || drone_altitude + response.vertical_movement > fence_alt_max) {
        // drone is within the fence, check if the altitude is within the fence
        send this, eFenceReached;
        goto Holding;
      }
      
      // update the drone's location normally
      else {
        // update the drone's position and request new subsequent movement
        drone_altitude = drone_altitude + response.vertical_movement;
        drone_horizontal_distance_to_origin = drone_horizontal_distance_to_origin + response.horizontal_movement;
        send this, eRequestDroneMovement;
        goto GenerateMovement;
      }
    }
  }

  // temporarily holding the drone.
  state Holding {
    on eFenceReached do {
      // start holding pattern
      print "Fence Reached! Starting Holding Pattern";

      send this, eFenceDistanced;
      goto Monitoring;
    }

    on eLanding do {
      goto Landing;
    }

  }

  state GenerateMovement {
    on eRequestDroneMovement do {
      var response : tDroneMovementResponse;

      // generate random movement from -10 ~ 10
      response.horizontal_movement = choose(20) - 10;
      response.vertical_movement = choose(20) - 10;

      print format ("Movement Generated, horizontal movement = {0}, vertical movement = {1}", response.horizontal_movement, response.vertical_movement);

      send this, eDroneMovementResponse, response;
      goto Monitoring;
    }
  }

  state Landing {
    entry {
      print "Landing! Horizonal Distance Remain Unchanged";
    }

    on eDecrementAltitude do {
      drone_altitude = drone_altitude - 1;
      if (drone_altitude == 0) {
        send this, eLanded;
        goto Landed;
      } else {
        send this, eDecrementAltitude;
      }
    }
  }

  state Landed {
    entry {
      print "Landed! Altitude Set to 0";
      drone_altitude = 0;
    }
  }
}