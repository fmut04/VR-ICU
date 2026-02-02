# BEAM
*Created by Ben Rider, Matthew Carter, Amy Choi, Akmal Faiz Mohd Nizam, Emmaleigh Shinno, Mohamad Aiman Bin Zamri, 2025*

BEAM (Basic framEwork for mediAl procedure siMulaton) is an alpha-stage VR medical care procedure training simulation framework centered around Central Line-Associated BloodStream Infection (CLABSI) Prevention During ICU Care Transitions. The system implemented allows for modular step attachment which allows proctors to modify and add to the procedure as needed. 

# Development
In order to contribute to BEAM, the developer must have have a license to the following Unity Assets

  - [Hospital Pack](https://assetstore.unity.com/packages/3d/environments/industrial/hospital-recovery-room-80725)
  - [ObiRope](https://assetstore.unity.com/packages/tools/physics/obi-rope-55579)

## Procedure Management System
In order to keep track of the different states of the implemented procedure, we introduce the `GameManager`, `State`, and `InteractonStatusHandler` classes.

### `GameManager`
GameManager is the high-level singleton class which keeps track of the current state of the game (which state is the `CurrentState`) and handles moving from state to state once a state is completed. In order to add a State to the GameManager, you must add the state within the public list `gameStates` in the order in which the states should appear. The `State` object at index `0` of this list is the beginning state by default. This logic is the same for the last `State` in this list being the end `State`.

### State
The `State` class allows a `GameObject` to represent a set of steps within a procedure. For example. one could represent the sterilization phase of a procedure as the steps Washing Hands and Applying Gloves. This way, this state of the procedure will only be completed once *all* of those steps are completed within the program.

#### Creating new `States`
To create a new `State`, we follow this procedure:
- Create an empty `GameObject` in the editor
- Add the `State` script as a component
- For each step the `State` is keeping track of, add a descriptive name for that step within the exposed list. For a steps truth value to be properly recorded, this name must match the name given within that steps `InteractonStatusHandler`   

### InteractonStatusHandler
`InteractionStatusHandler` is the class which represents an individual step within a procedure such as washing hands or hanging an IV bag. This class allows for the `GameObjects` which hold the interaction logic for these steps to communicate their completion to the `State` which owns them. 

#### Creating a new steps
A step can be defined as any GameObject which holds an `InteractonStatusHandler` component. For this step to be properly registered, the `InteractionStatusHandler` must be given a reference to that steps owning `State`, and given the name of the step which *must match one of the names within the `State` object`

# User Interaction
Since all user interactions within a procedure usually follow the same general principle, we have implemented an `InteractionZone` prefab which gives a starting place for the development of an interaction. 

## When should I use this prefab?
This prefab is good to use when the interaction to be modeled involves the user using some set of in-game object (or just their hands) within a specific area in the scene. Good examples of this include, hanging an IV bag. The required objects in this interaction are the New IV bag, and the user must hang the bag on the IV pole. This prefab is implemented with an `boxcollider` trigger which represents the zone in which the user must hold the required game objects to initiate the interaction. 

### RequiredObjectsChecker
This script is used to ensure that set of GameObjects that are necessary to complete a step of a procedure are within the `InteractionZone` before the interaction is carried out. In order to utilize this script, you must specify a list of tags which correspond to the tags of the required GameObjects needed for the associated step.

