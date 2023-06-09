# Usage
```
p compile # compile the PSrc, PSpec and PTst
p check   # model check the P model against the PSpec.
          # the specific spec/property must be declared via assert in PTst

# Compile a Minimal P Model
- setup a source state machine (files: Ping.p, Pong.p)
- setup module systems (files: PingModules.p) (i.e. module <module name> = { <state machine
  name>}
- setup test script and test driver (files: TestDriver.p, TestScript.p)

# Model Checking
There are currently two violations to the specification in `PingPongSpec.p`
- First is the safety violation with respect to the assertion statement `assert global_counter <= 10, "global_counter must be <= 10";`. Theoretically, the ePing and ePong events can be sent indefinitely. But the assertion statement states that the back and forth can only go on less than or equal to 10 times
- Second, the system cannot terminate on a hot state (liveness property). There's a maximum limit in terms of how much this state machine can run for. It can potentially terminate on the hot state. (set the state from hot to cold or to nothing to prevent the error)