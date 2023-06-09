machine Test_Original_Model {
  start state Init {
    entry {
      // the hierarchical composition ensures that the battery model can send
      // an event to the geofence model
      // battery_model -> geofence_model
      new Battery(new GeoFence());
    }
  }
}
