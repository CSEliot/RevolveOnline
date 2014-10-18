Okay, here's the basic game design:

GameMaster holds the universal variables. These variables are modifiers of whatever the base stats are
on the guns, characters and maps.

Maps have base stats in the GameMaster, or presets into the settings, including enabled characters, etc.

Guns have base stats for firing rate, the bullet they fire, how quickly they fire, whatever you want.
BUT, Guns MUST have the following 3 stats that get added or subtracted by the GameMaster._M.variable:
- Initial Bullet Speed
- Bullet Firing Rate
- Bullet Damage
The rest is up for you to interpret, but all content base stats are not modified by the GM.

Characters are similar to guns in this way, characters MUST have the following that can be modified by the 
universal stats given by the GameMaster variables:
- Running Speed
- Walking Speed
- Running Enabled
- Jump Height
- Jump Count
- Jump Enabled
- Max Health
- Max Armor
And the following extremities: Strafe Toggle, Invert Toggle, Mouse Invert Toggle, Drunk Toggle

When creating guns, they should be implemented into every level, just place them in a pedestel's origin and
place the pedestal somewhere, make sure the gun has a rigidbody w/ IsKinematic and NoGravity and a box 
collision <<trigger>>. They must also have a child component at top that says where the bullets spawn.

When creating Characters, they MUST have a gun setup script that places the gun upon equip. Similar situation
with bullets, just look at our current prefabs for those to get an idea as to how to make your own.

In the future, each scene will be a level, and we'll have a level select screen with names and you can view the preset.
The question now is, will we save a .bin file with all of these different presets, OR, have one .bin per stage?
ALSO, GM number changes will be small, considering we'll have different play areas.