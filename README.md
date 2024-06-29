This project is an overengineered small 2D prototype of a herdsman. It showcases a few design patterns and best practices by allowing the player to collect animals and move them to the yard.

**Note**: KISS is a valid rule for design patterns and principles like SOLID, which aims to create a maintainable and scalable codebase without unnecessarily complicating the project. Given the relatively small size of the prototype, it’s essential to strike a balance between applying design patterns & architecture patterns and keeping the code straightforward.

**How to run:**

**Git clone** <https://github.com/MunteanuAndreiStefan/Herdsman>

**Open Unity hub -> Add -> Add project from disk -> Wait for it to download all packages -> Go to Resources -> Scenes -> Herdsman and press Play.**

**SOLID Principles** are touched on in this project.

- Single Responsibility Principle (**SRP**): Each class embodies this principle in this project, having a single job or responsibility. (Example: [PlayerInitializer](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/Player/PlayerInitializer.cs), [AnimalFactory](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/AnimalFactory.cs))
- Open/Closed Principle (**OCP**): Software entities should be open for extension but closed for modification. (Example: [AnimalFactory](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/AnimalFactory.cs))
- Liskov Substitution Principle (**LSP**): Subtypes must be substitutable for their base types, ensuring that derived classes can replace base classes without affecting functionality. (Example: [IAnimal](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/Interfaces/IAnimal.cs) and [Rabbit](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/Rabbit.cs)/[Sheep](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/Sheep.cs))
- Interface Segregation Principle (**ISP**): Clients should not be forced to depend on interfaces they do not use. This promotes creating smaller, more specific interfaces. (Example: [IUIManager](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameUI/Interfaces/IUIManager.cs))
- Dependency Inversion Principle (**DIP**): High-level modules should not depend on low-level modules. Both should depend on abstractions. High-level modules ([DiContainer](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/DiContainer.cs)) depend on abstractions ([IAnimalSpawner](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Interfaces/IAnimalSpawner.cs), [IScoreManager](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameUI/Interfaces/IScoreManager.cs), etc.), while Low-level modules (like [AnimalSpawner](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/AnimalSpawner.cs), [ScoreManager](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameUI/ScoreManager.cs), etc.) also depend on abstractions, ensuring loose coupling and better maintainability.

### **Singleton Persistent**

- **Purpose:** Ensures a class has only one instance and provides a global point of access to it.
- **Usage:** It could be used to manage the game state, score counter, and other global managers. I used it to ensure that only one Dependency injection container will ever exist in the game, for the [CameraManager](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCamera/CameraManager.cs) and the UI manager, since we won’t want these to get destroyed or for some other programmer to come in and overwrite them.

### **Factory**

- **Purpose:** Defines an interface for creating an object but let’s subclasses alter the type of objects that will be created.
- **Usage:** To create instances of [AnimalFactory](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/AnimalFactory.cs) with potentially different behaviours or attributes.

### **Strategy:**

- **Purpose**: Defines a family of algorithms, encapsulates each one, and makes them interchangeable.
- **Usage:** Could be used to define different [strategies](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Spawn/SpawnStrategies/RandomSpawnStrategy.cs) for animals (e.g., patrol behaviour, following behaviour), but I’ve only used it for spawn.

### **Object pool:**

- **Purpose**: Minimize object creation overhead, improve performance via not generating garbage for GC.
- **Usage:** Used for [animal](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/AnimalObjectPool.cs) reuse.

### **Observer via UniRx:**

- **Purpose**: Facilitate real-time data changes, reactive programming, and event-driven architectures.
- **Usage:** Used for the [score update](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameUI/UIScoreObserver.cs), on UI via a reactive property.

### **Best practices:**

Two canvases, one for static and one for dynamic, are needed to prevent the rebuilding of UI elements due to dirty flags.

Cache usage unity API on each frame instead of accessing it.

Usage of non-alloc functions to prevent GC issues, could be implemented other strategy of GC.

Usage of object pools to reduce allocation.

A low number of immutable operations, like string construction without a StringBuilder.

Reducing boxing and unboxing as much as possible while delivering good design patterns. Unity’s garbage collector is not generational.

Limit closures and anonymous methods because executing the closure requires instantiation of a copy of its generated class; executing the closure requires allocation of an object on the managed heap. Anonymous methods and method references should be minimized in performance-sensitive code, especially in code that performs on a per-frame basis.

All Unity APIs that return arrays create a new copy of the array each time they are accessed. It is extremely non-optimal to access an array-valued Unity API.

Transform manipulation hierarchies have a relatively high CPU cost due to the propagation of change messages.

Etc.

### Rendering Pipeline - URP

The **Universal Render Pipeline (URP)** is a Scriptable Render Pipeline (SRP) provided by Unity that is optimized for performance and scalability across a wide range of platforms. URP is designed to be a flexible and efficient rendering pipeline that can be customized to meet the needs of different projects.

I’ve picked it due: Key Features of URP:

- Cross-Platform Performance: URP is designed to work well on a wide range of devices.
- Scalability: URP provides tools to scale the quality and performance of the rendering.
- Lightweight: URP is more lightweight making it suitable for less powerful hardware.

Shaders used were unlit since they do not perform lighting calculations, they are very efficient and are ideal for objects where lighting is either not required or can be baked into the texture.

### GPU Instancing

**GPU Instancing** is a technique used in computer graphics to efficiently render large numbers of identical objects in a scene. GPU instancing allows for a single set of data to be sent and reused for multiple objects. This reduces the overhead and improves rendering performance significantly, especially when dealing with numerous objects that share the same mesh and material.

- Reduced Draw Calls: By batching similar objects together, GPU instancing reduces the number of draw calls, which is crucial for maintaining high performance.
- Improved Performance: Less CPU-GPU communication and better GPU utilization lead to smoother frame rates and the ability to render more objects.
- Simplified Data Management: The same geometry and material data are reused, simplifying the data management process.

Unfortunately, at this point, on URP GPU instancing is not available to the SpriteRenderer, so a custom implementation was needed via quads + material.

### **Architecture**

I would not say I like Model-View-ViewModel (**MVVM**) or Model-View-Controller (**MVC**) for Unity projects. From the performance point of view, all the time, they add overhead and introduce extra layers of unneeded abstractization most of the time which generates extra memory to be managed by the GC, adding unnecessary processing; while data binding can add some benefits, I feel like the disadvantages are bigger. In terms of project structure, the project structure would also grow without needing to. Unity's prefab system and inspector do not align well with the enforced MVVM or MVC. The specific workflows from Unity, like an event-driven system, are sometimes at odds with how MVVM and MVC handle interactions. Handling events in Unity often involves direct manipulation of GameObjects and Components, which can be cumbersome when integrated with MVVM or MVC. Reactive programming or event-based architectures integrate better with it.

My proposal as architecture is a single-entry point for dependency injection that would control all the logic from the game while trying to keep it as modular and encapsulated as possible.

**Modular Design**: By structuring the code into self-contained components, we can simplify management, testing, and scalability. Each module, encapsulating a specific functionality, can interact with other modules through well-defined interfaces, ensuring a more robust and flexible Unity game development process.

&nbsp;   Example: Separating game logic, input handling, and UI updates into distinct classes. ([InputManager](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameInput/InputManager.cs"%20\t%20"_blank))

**Encapsulation**: By keeping the internal state of objects hidden and exposing only necessary methods, we can ensure data integrity and reduce the risk of unintended interference. This is a key principle that will guide our Unity game development process.

&nbsp;   Example: Using private member variables with public getter and setter methods. ([AnimalFollower collider](https://github.com/MunteanuAndreiStefan/Herdsman/blob/main/Herdsman/Assets/Scripts/GameCore/NPCs/Animals/AnimalFollower.cs"%20\t%20"_blank))

### **Code Style**

**Consistency**: Adhering to a consistent code style throughout the codebase helps maintain readability and ease of understanding. I’m able to adapt to any code style, but for this project, I’ve used:

- Private member variables should start with an underscore ‘\_.’
- Public variables should begin with a large CamelCase (PascalCase).
- Constants should be in ALL_CAPS.
- Local variables should be camelCase
- Function names should be CamelCase (PascalCase).
- Interfaces should start with ‘I’
- Abstract classes should begin with “Abstract”
- The order of the functions should be based on external accessibility.
