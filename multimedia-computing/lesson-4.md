The following notes provide a comprehensive overview of the topics required for Unit 4: Video and Animation.

***

## Unit 4: Video and Animation

### I. Digital Video

**Digital Video** is a representation of moving visual images in the form of encoded digital data, contrasting with analog video, which uses analog signals.

#### A. Advantages of Digital Video
Digital video offers several advantages over analog video:
1.  **Ease of sharing and storage**.
2.  **No degradation of data quality when copied**.
3.  **Easy and inexpensive copying**.
4.  **Capacity for multicasting**.
5.  **Direct access**, which simplifies **nonlinear video editing**.
6.  **Better tolerance to channel noise** and **ease of encryption**.

#### B. Digital Video Representation
Digital A/V information consists of discrete units of data (zeros and ones) placed so close together that human senses perceive them as a continuous flow.

In a digital video system, samples of the image matrix are conveyed in the same order as in an analog system: first the top line (left to right), then the next lower line, and so on.

**High-Definition Television (HDTV)** is a digital video format defined as having twice the vertical and twice the horizontal resolution of conventional television, a picture aspect ratio of **16:9** instead of 4:3, and a frame rate of 24 Hz or higher. The data rate of studio-quality HDTV is about **120 megabytes per second**. HDTV is moving toward **progressive (noninterlaced) scanning** to avoid serrated edges and flicker caused by interlacing.

### II. Video Signal Representation

A digital video image is sampled in the **horizontal, vertical, and temporal axes**. This forms a **spatiotemporal domain**.

#### A. Components of the Video Signal (Luma and Chroma)
A video system conveys image data using one component for **brightness** and two components for **color**.

1.  **Luma ($\text{Y}'$):** The signal representative of brightness, which has been subjected to a **nonlinear transfer function (gamma correction)**. It is usually computed as a weighted sum of the nonlinear red, green, and blue components ($\text{R}', \text{G}', \text{B}'$). In component video, the **luma component channel (Y)** makes up the dark and light part of the video picture.
2.  **Chrominance (Chroma, $\text{C}$):** The signals used to convey color information, often represented by two color-difference components, **$\text{C}_{\text{B}}$ and $\text{C}_{\text{R}}$**. These two components, derived from the color differences ($\text{B}' - \text{Y}'$) and ($\text{R}' - \text{Y}'$), can be scaled to form $\text{U}$ and $\text{V}$ signals, or $\text{I}$ and $\text{Q}$ signals (used in NTSC). Since human vision has poor color acuity relative to brightness, the data capacity dedicated to color information can be reduced.

#### B. Chroma Subsampling
**Chroma Subsampling** is a technique that reduces the data capacity accorded to color information by taking advantage of the relatively poor color acuity of vision, while maintaining full luma detail. This is performed because humans are better at differentiating spatial resolution in grayscale images than in the color part of color images.

The notation uses a numerical basis of 4, historically referencing a sample rate of about four times the color subcarrier frequency. Common sampling formats include:

*   **4:4:4 ($\text{R}'\text{G}'\text{B}'$ or $\text{Y}'\text{C}_{\text{B}}\text{C}_{\text{R}}$):** No subsampling; occupies 12 bytes for a $2\times2$ array in an 8-bit system.
*   **4:2:2 ($\text{Rec. 601}$):** Chroma components are subsampled by a factor of **2 along the horizontal axis**. The aggregate data capacity is **16 bits per pixel** (8 bytes for a $2\times2$ array) in an 8-bit system. Rec. 601 specifies sampling of luma at **13.5 MHz** and chroma at **6.75 MHz**.
*   **4:2:0 ($\text{JPEG/JFIF, H.261, MPEG-1}$):** Chroma components ($\text{C}_{\text{B}}$ and $\text{C}_{\text{R}}$) are subsampled by a factor of 2 **both horizontally and vertically**.
*   **4:1:1 ($\text{DVC}$):** Chroma components are subsampled by a factor of 4 horizontally.

### III. Computer Video Format (Containers and Standards)

Video data is typically stored in a combined package using **container formats** alongside audio compression techniques. The most popular video formats use compression standards like **MPEG** and **H.26x**.

#### A. Video Standards (Codecs and Applications)
| Standard | Application | Key Characteristics |
| :--- | :--- | :--- |
| **H.261 ($\text{p} \times 64 \text{ kb/s}$)** | Video telephony and teleconferencing over ISDN. It was the first video coding format based on **DCT compression**. Uses I-frames and P-frames. | Bitrate in multiples of 64 kb/s. Supports CIF and QCIF. |
| **MPEG-1** | Video on digital storage media (CD-ROM). Optimized for noninterlaced video at $1.2-1.5 \text{ Mbits/s}$. | Video quality comparable to home VCRs. Uses 4:2:0 chroma subsampling. |
| **MPEG-2** | Digital Television, DVD video distribution. Used as the compression standard for over-the-air digital TV (ATSC, DVB, ISDB). | Supports interlacing and high definition. Bitrates range from 2–20 $\text{Mb/s}$. |
| **MPEG-4** | Object-based coding, synthetic content, interactivity. Designed to stream DVD quality video at lower data rates and smaller file sizes. | Supports video/audio "objects" and 3D content. Includes MPEG-4 AVC ($\text{H.264}$), which is a leading standard for HD video. |

#### B. Digital Container Formats
A **container format** is a multimedia file format that supports video, audio, subtitles, images, and text.

| Format | Extension | Owner/Creator | Key Characteristics |
| :--- | :--- | :--- | :--- |
| **MPEG-4 Part 14 ($\text{MP4}$)** | .mp4, .m4v | MPEG | Based on Apple QuickTime. Popular for video uploads due to high compression and good quality. |
| **QuickTime File Format ($\text{QTFF}$)** | .mov, .qt | Apple Inc. | A multimedia framework capable of handling various formats of digital video, sound, text, and animation. The .mov file format is an openly-documented media container. |
| **AVI** (Audio Video Interleave) | .avi | Microsoft | Supports both audio and video data in a file container that allows synchronous playback. |
| **Flash Video ($\text{FLV}$)** | .flv | Adobe Inc. | Flash Video files are played through an FLV-aware player. |
| **GIF** (Graphics Interchange Format) | .gif | CompuServe | Limited to 8-bit indexed color. Supports multiple images and text overlays, enabling animation. |

### IV. Computer-Based Animation

**Computer Animation** is a visual digital display technology that processes moving images on screen. It is the **rapid display of a sequence of images** (2D or 3D artwork) to create the illusion of movement, often seen in movies, films, and games. The key concept is playing defined images at a faster rate (e.g., around **12 frames per minute** for the illusion of movement, or 24 frames per second for traditional methods) to fool the viewer into interpreting a continuous motion.

#### A. Animation Language (Scripting and Authoring Tools)

While animation can be created using lower-level programming, specific languages and authoring tools simplify the process:

*   **Authoring Tools:** Programs with preprogrammed elements for developing **interactive multimedia** titles.
    *   **Scripting Languages:** Closest to traditional programming, specifying multimedia elements, sequencing, synchronization, and **hotspots**. Director's language, **Lingo**, is an object-oriented, event-driven scripting language. Scripting allows more powerful and complex interactivity.
    *   **Icon-Based Programs:** Easier to learn than scripting programs, using graphical icons as control features (e.g., Macromedia **Authorware** or Icon Author). These are best suited for **rapid prototyping**.
*   **Animation-Specific Languages/APIs:**
    *   **ActionScript (Flash):** An object-oriented language within Adobe Flash used by advanced users to create movement. ActionScript allows for advanced tweening properties like easing, elasticity, bouncing, and sequencing of movements. It is **non-linear** and timeline independent, meaning an action can be initiated at any point during a previous animation.
    *   **VRML (Virtual Reality Modeling Language):** Capable of representing static and animated objects, as well as hyperlinks to other media. VRML animation primarily uses **tweening** by slowly changing an object specified in an interpolator node.

#### B. Timeline and Frame-Based Animation

Animation fundamentally relies on frames, a snapshot in time.

1.  **Frame-by-Frame (Traditional Method):** The animator draws or sets up objects one frame at a time in order. Earlier, this required **24 frames per second** for animation, demanding significant effort. In Director, this amounts to using different cast members in different frames.
2.  **Timeline:** In authoring tools like Adobe Flash, the Timeline window manages the **layers and timelines** of the scene, similar to a musical score. The Timeline is composed of **keyframes** in different layers.
3.  **Keyframes:** A **keyframe** is a drawing that defines the **starting and ending points** of any smooth transition. Keyframes are frames where we define changes in an animation. Missing frames are filled in automatically by the computer via the **inbetween process**. Keyframe-based animation is also known as **Pose-to-Pose animation**, where key poses are planned out and the motion in between is generated by interpolation.

#### C. Timeline and Tween-Based Animation

**Tweening (Inbetweening)** is the process of generating **intermediate frames** between two keyframes to create the illusion of smooth motion. This is the job that the computer fulfills automatically in modern tools.

| Type of Tweening | Description | Control/Flexibility |
| :--- | :--- | :--- |
| **Timeline Tweening** | Tweening achieved easily using the timeline by selecting two keyframes and adding a motion or shape tween (e.g., in Adobe Flash). | Linear in movement; movement is difficult to change once defined. Good for basic tasks like fading images or moving type. |
| **ActionScript Tweening** | Using ActionScript (programming) to define complex movement parameters. | Non-linear and timeline independent. Allows advanced features like easing, elasticity, bouncing, sequencing of movements, and delays. Allows seamless course changes midway through an animation. |
| **Shape Tweening** | Allows a shape to continuously change into a different shape over time. | |
| **Movement Tweening** | Allows placing a symbol in different places on the Stage in different keyframes, with Flash automatically filling in the path. | Can involve control of the path and acceleration/deceleration ("ease-in" and "ease-out"). |
| **Palette Cycling (Palette Animation)** | For 8-bit images, cyclically changing the colors in the **Color Look-Up Table (CLUT)** to create effects. | Relatively fast, as only small LUT values need to be sent (takes less than 1 ms) instead of a full frame buffer transfer. |

#### D. Methods of Controlling Animation

Animation control determines how the movement and changes in attributes (position, form, color, etc.) are executed.

| Control Method | Description | Example |
| :--- | :--- | :--- |
| **Explicitly Declared Control** | The animator describes all events that could occur, typically specifying simple transformations (scaling, rotation, translation) or defining key frames and interpolation methods. | Specifying that an object rotate $30^{\circ}$ between frame 42 and frame 53. |
| **Procedural Control** | Uses a **set of rules** or mathematical equations, often based on real-world physics, to animate objects. The animator defines the initial rules, and the simulation runs based on them. | Using physics engines within a 3D application, such as for objects that fall. |
| **Constraint-Based Control** | Movement is determined by constraints, often defined by the environment or other objects. | Specifying that an object must follow the movement of another object it is in contact with. |
| **Control by Analyzing Live Action** | Creating animation sequences by examining the motions of real-world objects. | **Motion Capture (Mo-cap):** Recording the movements of human actors (using markers and video cameras) and applying that action to animate a digital character. |
| **Kinematic Control** | Specifies the **position and velocity** of points over time (e.g., the cube moves with constant acceleration in a specified direction). | |
| **Dynamic Control** | Takes into account the **physical laws** (e.g., gravity, mass, force) that govern kinematics. | Simulating a cube falling due to gravity. |

### E. Display and Transmission of Animation

#### Display of Animation

To display animations on raster systems, objects must be **scan-converted** and stored as a pixmap in the frame buffer.

*   For a reasonably smooth visual effect, scan-conversion must be done at least 10 (preferably 15 to 20) times per second, meaning a new image must be generated in at most 100 ms.
*   If the scan-conversion takes too long, the result can be a distracting **ghost effect**.
*   **Page Flipping and Back Buffering** are techniques used to create true animation: generating a frame on a secondary drawing surface (**back buffering**) and then cycling through drawing surfaces (**page flipping**) to display the new frame.

#### Transmission of Animation

Animation can be transmitted using two primary approaches, depending on how the objects are represented:

1.  **Symbolic Representation:** The symbolic description (e.g., circle) of the objects and the operations performed (e.g., roll the ball) are transmitted.
    *   **Advantages:** Since the byte size is much smaller than a pixmap (pixel map) representation, the **transmission time is short**.
    *   **Disadvantages:** The **display time is longer**, as the receiver must generate the corresponding pixmaps (rendering the circle).
2.  **Scan-Converted Pixmap Representation:** The animation is transmitted as a sequence of scan-converted images (frames).
    *   **Advantages:** Display time is short, as the receiver only needs to draw the pixmap.
    *   **Disadvantages:** Transmission time is much longer because the size of the pixmap data is large.

The required bandwidth for transmission depends on the size of the symbolic structure and command, and the number of animated objects and commands sent per second.
