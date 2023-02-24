# TopDown Action Prototype

**Unity version:** 2020.3.17f1  
**Dev time:** 23h~23h30

## Features:
- Character that can move in all directions
- Top-down camera that follows the player
- Dash mechanic with reload time
- Basic HUD
- Health system
- Enemies with simple AI (idle -> chase -> attack)
- Combo feature (X attacks that can be chained together)
- Placeholder defeat screen (no game loop)

## Known bugs:
AIs sometimes stays in chase state when they are already close enough to the player

## Details
The entities use a basic Character - Controller architecture : the Playable/Enemy Character and Player/AI Controller uses the same common code to simplify scalability.

AI enemies use an FSM that needs a Controller as an owner.  
The states contain a list of transition that are checked each frame.

Inputs are handled with the input systems events to avoid calls going through all components.

### Skills
- As the skills all require a cooldown, a parent script handles this. Inheriting from it enables to create a skill whose cooldown is already handled in this parent script.
- How the dash behaves (the evolution of the speed at each frame) is customizable through an animation curve.
- Each attack from the combo system is deeply customizable by inheriting from the AttackData script (where specific behavior can be defined), but the system cannot handle animation specific behavior/constraints (which require specific timing defined through Animation Events).

ScriptableObjects are used for any data that should be configurable.
