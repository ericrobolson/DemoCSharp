# Basic engine core

- [x] Build out basic project structure
- [x] Fix your timestep gaffer on games
- [x] Add in sprite batches + fix non-premultiplied alpha
- [x] Implement basic drawing of sprites
- [x] Implement drawing subsections of sprites
- [x] Implement checking for whether an aabb contains a vec2
- [x] Create an entity id
- [x] Create statically allocated container
- [x] Create a world with a collection of Entities
- [x] Implement an asset manager and load a texture in a background thread
- [x] Implement loading of assets from a active asset list
- [x] Implement drawing of sprites from a sprite draw list
- [x] Implement collision detection between two aabbs

# Unordered stuff

- [ ] Create a room loader
- [ ] Create a font renderer
- [ ] Pick a simple game and implement it
- [ ] Determine genre based on marketing potential
- [ ] Determine art style based on marketing potential
- [ ] Install C# linter. Make it so all data stuff is serializable.
- [ ] Make all components serializable.
- [ ] Make a simple content pipeline that takes in blender objects, then creates spritesheets from them
- [ ] Make a simple content pipeline tool that adds new fonts as bitmaps
- [ ] Split out systems for ECS then write back to entities
- [ ] Create a component store container; use something like the TurboSquid ecs implementation with two lists. One for entity ids, one for actual components to allow fast iteration.
- [ ] Implement audio
- [ ] Implement camera + 'in view' concept to trigger loading of audio + sprites

# Puntable

## Items that can be deferred until needed. Done this way as YAGNI.

- [ ] Convert entities from Array of Structs to Struct of Arrays
- [ ] Implement scaling of sprites
- [ ] Implement rotation of sprites
