This document provides comprehensive notes for preparing for your exam on Unit 6: Abstraction for Programming, covering the introduction to abstraction, and the hierarchy of software layers used in multimedia development.

***

## Unit 6: Abstraction for Programming

### I. Introduction to Abstraction Levels

#### A. Definition and Rationale of Abstraction
**Abstraction** in computer science provides access to a system while simultaneously **hiding details** that might otherwise make access more challenging. It shifts focus to details of greater importance in a given context.

The essence of abstraction is **preserving information that is relevant** and **forgetting information that is irrelevant** to the context. It allows humans to create large systems by concentrating on a few issues at a time, relying on architectures made of specific choices of abstractions.

#### B. Abstraction in Multimedia Programming
The current state of programming often means application code is highly dependent on hardware. A change in multimedia devices still frequently requires re-implementation. The primary goal of using abstraction levels in multimedia programming is to abstract away from multimedia hardware, which helps address core development problems such as:
*   **Portability**.
*   **Reusability**.
*   **Coding efficiency**.

#### C. Overview of Abstraction Levels
Computer science typically presents **levels (or layers) of abstraction**, where each level uses a different model of the same information but with varying amounts of detail. Each relatively abstract, "higher" level builds on a relatively concrete, "lower" level.

The hierarchical abstraction levels for the programming of multimedia systems are generally structured as follows, moving from the lowest hardware interface up to the final application layer:

1.  **Device**
2.  **Device Drivers for Continuous Media**
3.  **Libraries**
4.  **System Software**
5.  **Toolkits**
6.  **Higher Programming Languages** (Often parallel to Object-Oriented Programming Language)
7.  **Object-Oriented Programming Language** (Often parallel to Higher Programming Language)
8.  **Multimedia Application**

---

### II. Libraries

**Libraries** facilitate the processing of continuous media through **functions embedded in libraries**. These libraries differ in their degree of abstraction.

#### Example: OpenGL
**OpenGL** (Open Graphics Library) is a prominent example of a library abstraction, serving as a 2D and 3D graphics API (Application Programming Interface) originally developed by Silicon Graphics.

The basic idea behind OpenGL is to **"write applications once, deploy across many platforms"**, supporting systems like PCs, workstations, and super computers. OpenGL is known for being **portable**, stable, reliable, scalable (supporting features like zoom and rectangle handling), and easy to use. OpenGL has become the most popular 3D API in use today because it is **highly portable** and runs on all popular modern operating systems, including UNIX, Linux, Windows, and Macintosh.

---

### III. System Software

System software abstracts hardware by incorporating device access into the operating system.

#### A. Data Representation in System Software

1.  **Data as Time Capsules (Logical Data Units - LDUs):** The operating system can treat data as "time capsules" (file extensions). Each **Logical Data Unit (LDU)** carries its data type, actual value, and **valid life span**. This is particularly useful for video, where a frame might have a valid life span of $40 \text{ms}$. Presentation rates for functions like fast forward or slow forward (VCR functions) can be adjusted by changing the presentation life span of an LDU, or by skipping or repeating LDUs.
2.  **Data as Streams:** A stream denotes the **continuous flow of audio and video data** between a source and a sink (destination). The stream must be established, similar to setting up a network connection, prior to the flow.

#### B. Key System Software/APIs (Microsoft Examples)

1.  **Windows Media Control Interface (MCI):** This system uses the **MMSYSTEM library** to achieve device independence and extensibility. MCI provides access to low-level functions and controls devices such as joysticks, waveform devices, MIDI devices, videodisc players, and compact disc audio.
2.  **DirectX:** A set of **low-level APIs and libraries** designed for **high-performance applications**, especially computer games (formerly the "Game SDK"). DirectX provides direct access to hardware services like audio and video cards and hardware accelerators.
    *   **Components:** DirectX components include **DirectDraw** (2D graphics), **Direct3D** (functional 3D graphics API), **DirectSound** (3D sound, mixing), **DirectPlay** (network multiplayer development), and **DirectInput** (input from peripherals like joysticks or data gloves).
    *   **Implementation Strategy:** DirectX utilizes a **Hardware Abstraction Layer (HAL)** and a Hardware Emulation Layer (HEL). It also includes a **Media Layer** for aggregated high-level functionality such as animations, media streaming, and synchronization.

---

### IV. Toolkits

**Toolkits** provide a simpler approach compared to dealing directly with the system software interface.

*   **Abstraction Goal:** Toolkits **abstract from the actual physical layer**.
*   **Interface:** They allow for a **uniform interface** for communication with all different devices of continuous media.
*   **Paradigm:** Toolkits often introduce the **client-server paradigm**.
*   **Integration:** They can be embedded into higher programming languages or object-oriented environments.

---

### V. Higher Programming Languages

Programming languages facilitate **control abstraction**, enabling high-level statements to execute complex, low-level binary and register operations automatically. Language abstraction itself is exemplified by the evolution from machine language to assembly language and subsequently to **high-level programming languages**.

In multimedia programming, higher languages allow media to be managed in two ways:

1.  **Media as Data Types:** This involves defining appropriate data types (e.g., for video and audio), where the **smallest unit can be an LDU**.
2.  **Media as Files:** Continuous media can be treated as files that can be opened, read, mixed, activated, and closed using standard programming operations.

#### Language Requirements for Continuous Media
Higher-level languages used for multimedia must meet specific requirements to handle time-dependent data:
*   The language should support **parallel processing**, as continuous data processing is controlled by pure asynchronous instructions.
*   Different processes must be able to communicate using an **inter-process communication mechanism**.
*   This mechanism must be able to **understand a priori specified time requirements** (such as Quality of Service parameters extracted from the data type) and transmit the continuous data accordingly.

---

### VI. Object-Oriented Approaches (OOP)

OOP provides robust methods for abstraction, supporting polymorphism and delegation. The fundamental idea of object-oriented programming is **data encapsulation** combined with class and object definitions.

#### A. Key Principles of Abstraction in OOP
*   **Abstract Type Definition:** The definition of data types through abstract interfaces.
*   **Class:** The implementation of an abstract data type.
*   **Object:** An instance of a class.
*   **Encapsulation:** The hiding of state details.
*   **Polymorphism:** The ability for objects of different types to be substituted, achieved when abstraction proceeds into the defined operations.
*   **Inheritance:** Structuring classes to simplify complex relationships.

#### B. Application of OOP Abstraction in Multimedia

Object-oriented approaches apply abstraction by modeling different components of a multimedia system as classes:

1.  **Devices as Classes:** Devices are assigned to objects that represent their behavior and interface. For instance, a base class `media_device` could define common functions like `on()` and `off()`, which are then inherited by specialized classes such as `media_in_device` and `media_out_device`.
2.  **Media as Classes:** **Media Class Hierarchies** define hierarchical relations for different media, allowing specific properties to be inherited or specialized. For example, a hierarchy might include **Acoustic\_Medium** $\rightarrow$ **Music** $\rightarrow$ **Opus** (for sound score) or **Optical\_Medium** $\rightarrow$ **Video** $\rightarrow$ **Video\_Scene**.
3.  **Processing Units as Classes (Multimedia Objects):** This allows for the establishment of data flow paths by connecting objects.
    *   **Basic Multimedia Classes (BMCs) / Objects (BMOs):** These are the fundamental object types.
    *   **Compound Multimedia Classes (CMCs) / Objects (CMOs):** These are composed of BMCs/BMOs and other CMCs/CMOs.
    *   BMOs and CMOs have the potential to be **distributed over different computer nodes**.

***

**Analogy for Levels of Abstraction:**

Think of programming a multimedia application like ordering and receiving a custom car.

The **Highest Programming Language/Object-Oriented Approach** is the Customer (the programmer) giving high-level commands, like "I want the car to accelerate quickly" (defining an object's behavior). The programmer doesn't care about the spark plug gaps.

The **Toolkit** is the Dealership or Sales Team, which provides a simplified, uniform interface (e.g., standard option packages) and handles the client-server relationship with the factory (the underlying system).

The **System Software (e.g., DirectX)** is the Factory Supervisor (the Operating System), coordinating the overall production line. It manages complex streams of data ("time capsules" like frames) and makes sure high-performance components (Graphics/Audio cards) are accessible via specialized low-level APIs like HAL.

The **Libraries (e.g., OpenGL)** are the specialized, portable robot arms on the assembly line that perform precise, repeatable tasks (rendering 3D graphics) using efficient, pre-written functions.

The **Device Drivers** are the exact specifications and manuals for each unique component (the specific CPU, GPU, or sound card), which translate the factory supervisor's commands into electrical signals that the **Device** (the hardware) can execute.


## New
