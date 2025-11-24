Preparing for an exam on Unit 3 requires a detailed understanding of both static image processing and dynamic video standards and compression techniques.

Here are comprehensive notes covering all specified topics in Unit 3: Image and Video.

***

## Unit 3: Image and Video

### Part A: Image

The three-dimensional world is perceived by human vision as a two-dimensional image mapped onto the retina. In a camera, a lens projects the image onto a flat **image plane**.

#### 1. Digital Image Representation

A digital image is composed of **picture elements**, or **pixels**. It is represented by a matrix of numeric values, where each value is an intensity or gray level.

A digital image is created through **digitization**, which involves two processes applied to the captured signal:

1.  **Sampling:** Two-dimensional space is partitioned into small, discrete regions. This defines the spatial resolution.
2.  **Quantization:** An integer value is assigned to the amplitude (intensity or color) of the signal in each region.

##### Key Image Properties:

*   **Pixel Array:** A digital image is represented by a matrix of values. A single element in this matrix is a picture element (pixel). In a color system, a pixel includes information for all color components.
*   **Resolution:** Refers to the capability of an imaging system to reproduce fine detail in the picture. In computing, resolution is the number of pixels across the width and height of a frame. Resolution is typically measured in **pixels per inch (PPI)**. Higher resolution implies more image detail.
*   **Bit Depth (Color Depth):** The number of bits used to indicate the color of a single pixel.
    *   **1-Bit Images (Monochrome):** Store each pixel as a single bit (0 or 1), representing 2 colors (black and white).
    *   **8-Bit Grayscale:** Each pixel is represented by a single byte, allowing 256 gray values (0 for black, 255 for white).
    *   **8-Bit Indexed Color:** Each pixel stores an 8-bit index that points to a **color lookup table (LUT)** or **palette** containing 24-bit RGB values. This allows 256 colors to be displayed simultaneously.
    *   **24-Bit Color (Truecolor):** Uses **three 8-bit components** (Red, Green, Blue, or R'G'B'), totaling 24 bits per pixel. This scheme supports **16,777,216 possible colors**.

#### 2. Uses of Images

Images and graphics are important elements in multimedia systems, enhancing communication across various platforms. Key uses include:

*   **Enhanced Communication:** Representing ideas, concepts, and data visually.
*   **User Interface Design:** Icons and graphical buttons make interfaces intuitive.
*   **Information Visualization:** Charts, graphs, and infographics present complex data in an easily digestible format.
*   **Educational Tools:** Interactive learning experiences such as simulations and virtual labs.
*   **Entertainment:** Essential for gaming, animated movies, and special effects (VFX) in films.
*   **Medicine:** Used for archiving X-ray images, CT scans, and high-quality 3D magnetic resonance images (MRI) of human bodies.
*   **Scientific Research:** Used for modeling and simulation in mathematical and scientific research.

#### 3. Image Formats

Image file formats are standardized means of organizing and storing digital images.

##### Types of Image Representation:

*   **Bitmap Images (Raster Images):** Break the image into a series of colored dots called **pixels**. They are **resolution dependent**. Formats include GIF, JPEG, and PNG.
*   **Vector Images (Structured Graphics):** Defined by **mathematical relationships** between points, lines, curves, and shapes. They are **scalable** without looking jagged or pixelated. Ideal for logos and illustrations.

##### Popular File Formats:

| Format | Compression Type | Key Characteristics | Uses |
| :--- | :--- | :--- | :--- |
| **JPEG** (Joint Photographic Experts Group) | **Lossy** (DCT-based) | Supports millions of colors (up to 24-bit). Best for **photographs** and continuous-tone images. Compression efficiency relies on discarding frequency components irrelevant to human vision. | Web images, email, photographs. |
| **GIF** (Graphics Interchange Format) | **Lossless** (LZW) | Limited to **8-bit indexed color (256 colors)**. Supports transparency and simple animation. Was the first image format used on the World Wide Web. | Simple graphics, icons, animations. |
| **PNG** (Portable Network Graphics) | **Lossless** | Supports 24-bit color and 32-bit RGBA (alpha channel) transparency. Superior to GIF in many ways. | Web graphics, logos, images needing sharp detail. |
| **TIFF** (Tagged Image File Format) | Lossless or Lossy (supports JPEG compression) | High-quality raster format supporting high bit depths and multiple color spaces (RGB, CMYK). **International standard** for interchange between applications/platforms. | Professional photography, publishing, archiving. |
| **BMP/DIB** (Bitmap/Device Independent Bitmap) | Run-length encoding or uncompressed | Primary system graphics file format for Microsoft Windows. | Windows applications. |

#### 4. Image Color Scheme

Color in computer graphics is based on the Additive and Subtractive color models.

| Color Model | Components | Mixing Type | Primary Use | Characteristics |
| :--- | :--- | :--- | :--- | :--- |
| **RGB** | Red, Green, Blue | **Additive** | **Digital screens** (Monitors, TV, Web, Cameras). | Mixing R, G, B lights yields **White**. Offers the widest range of colors. |
| **CMYK** | Cyan, Magenta, Yellow, Key (Black) | **Subtractive** | **Printed materials** (Physical ink). | Mixing inks yields **Black** (if 100% saturation used). Colors are less vibrant than RGB. |
| **YCbCr** (Luminance-Chrominance) | Y (Luminance), Cb/Cr (Chrominance Blue/Red) | Transform | **Compression Standards** (JPEG, MPEG). | Separates brightness (Y) from color (Cb/Cr). This is ideal for compression because the human eye is less sensitive to color detail than brightness detail. |

#### 5. Image Enhancement

**Image Enhancement** involves manipulating the image to improve pictorial information for human interpretation.

Image enhancement techniques generally fall into two categories: Spatial domain methods (operating directly on pixels) and Frequency domain methods.

##### Common Enhancement Techniques (Digital Image Processing):

*   **Contrast and Brightness Correction:** Point processing techniques involve multiplication (gain) and addition (bias) to pixel values. **Contrast stretching** is a method to produce higher contrast by darkening levels below a specific value and brightening levels above it.
*   **Filtering:** Filters work with neighborhood operations to modify pixel values.
    *   **Low-Pass Filtering (Smoothing/Blurring):** Averages out rapid changes in intensity to reduce noise and blur the image.
    *   **High-Pass Filtering (Sharpening):** Emphasizes fine details and edges in the image. While it sharpens, it can also amplify noise.
    *   **Median Filtering:** A nonlinear process that is effective at reducing impulsive noise (salt-and-pepper noise) while preserving edges.
*   **Dithering:** A technique used to create the illusion of new colors or shading by varying the pattern of available colors (e.g., using a dither matrix of black and white dots to simulate grayscale). Dithering trades intensity resolution for spatial resolution.
*   **Histogram Equalization:** Used to increase the dynamic range of the pixels, which improves image appearance, especially contrast.
*   **Digital Data Compression:** While compression reduces file size, it is mandatory for large images. Lossy compression (like JPEG) achieves high ratios by discarding perceptually irrelevant data, often using the Discrete Cosine Transform (DCT).
*   **Other Features:** Resizing, cropping (selecting a desired rectangular portion to improve composition), and lens correction.

#### 6. Image Synthesis, Analysis, and Transmission

##### Image Synthesis (Generation)

Image synthesis is the process of generating digital images from computer-based models or by manipulating existing images using algorithms. Its goal is to produce realistic or creative images, such as generating photorealistic landscapes or creating special effects in movies.

##### Image Analysis

**Image Analysis** involves techniques to extract descriptions from images, which are necessary for higher-level scene analysis methods. It includes:

*   **Image Improvement/Enhancement:** Dealing with noise elimination or contrast enhancement.
*   **Pattern Detection and Recognition:** Detecting and clarifying standard patterns.
*   **Scene Analysis and Computer Vision:** Recognizing and reconstructing 3D models from several 2D images.

The process of **Image Recognition** typically involves five steps:
1.  **Formatting:** Capturing the image and transforming it into digital form (pixels).
2.  **Conditioning:** Suppressing noise and irrelevant variations (e.g., normalizing the background).
3.  **Labeling or Marking:** Determining the types of spatial events each pixel participates in.
4.  **Grouping:** Identifying objects by grouping interconnected pixels (e.g., grouping edges into lines).
5.  **Matching:** Determining the interpretation of events by comparing the pattern against stored models (templates).

##### Image Transmission

Image transmission involves sending digital images through computer networks.

*   **Data Characteristics:** Transmission must accommodate **bursty data transport** because image files are large. Unlike audio/video, **time-dependence is not a dominant characteristic** of image transmission.
*   **Transmission Methods:**
    1.  **Raw Image Data Transmission:** The size is computed as Spatial Resolution $\times$ Pixel Quantization.
    2.  **Compressed Image Data Transmission:** The image is compressed (e.g., using JPEG) before transmission, reducing the size based on the compression method and rate.
    3.  **Symbolic Image Data Transmission:** The image is represented through symbolic data (image primitives and control information); the size is equal to the size of the structure carrying the symbolic information.

***

### Part B: Video

**Video** is the technology that captures moving images electronically. These moving images are fundamentally a series of still images that change quickly enough to give the illusion of motion.

#### 1. Analogue and Digital Video

| Feature | Analogue Video | Digital Video |
| :--- | :--- | :--- |
| **Representation** | Stores continuous waves of R, G, and B intensities. | Stores information in a binary format (sequence of zeroes and ones). |
| **Scanning** | Uses a **fixed number of rows**. Analog video signals are sampled vertically by scanning. | Information is placed so close together that human senses perceive it as a continuous flow. |
| **Conversion** | Needs **Analog-to-Digital Conversion (ADC)** to be usable on computers. | Can be converted back to an analog signal for playback via DAC. |
| **Quality** | Prone to color errors (e.g., NTSC is jokingly called "Never The Same Colour"). | Digital formats (SDI, Firewire, DVI, HDMI) often offer higher quality than analog. |

##### Video Signals (Transmission Components):

*   **Component Video:** Uses three separate video signals, typically Red, Green, and Blue (RGB), or a luminance-chrominance transformation like YIQ or YUV. RGB is the preferred method for higher-quality and professional video work.
*   **Composite Video:** Combines luminance and chrominance information into a single analog signal, which can be transmitted over a single wire. NTSC is an example of a composite signal.
*   **S-Video (Separate Video):** An analog signal format that separates the brightness (Y or luminance) information from the color (C or chrominance) information.

#### 2. Recording Formats and Standards

##### Broadcast Video Standards (Analog):

*   **NTSC (National Television System Committee):** An analog color encoding system, used in the US and Japan.
*   **PAL (Phase Alternating Line):** An analog color encoding system used in broadcast television in many countries.
*   **SECAM (Systeme Electronic Pour Couleur Avee Memoire):** Another analog television standard.

##### Digital Video (DTV) Standards:

The transition to Digital Television (DTV) has led to competing standards worldwide:
*   **ATSC** (Advanced Television Systems Committee): Adopted by the USA, Canada, Mexico, and South Korea.
*   **DVB-T** (Digital Video Broadcast Terrestrial): Adopted by countries in Europe, Asia, Africa, and Oceania.
*   **ISDB-T** (Integrated Services Digital Broadcasting): Adopted by Japan.

#### 3. Recording Formats and Standards (Compression)

Because video data is so large (a typical uncompressed video sequence can exceed 1 Gbps for HDTV), compression is mandatory for storage and communication.

*   **JPEG (Joint Photographic Experts Group):** While primarily an image compression standard, JPEG uses DCT (Discrete Cosine Transform) which is a basis for many video standards.
    *   JPEG uses the lossy sequential DCT-based mode. The main steps involve transformation (e.g., RGB to YUV/YCbCr) and subsampling of color, DCT on image blocks, quantization, and entropy encoding.
*   **MPEG (Moving Picture Experts Group):** A working group of ISO/IEC that sets standards for compressing and transmitting audio and video. MPEG compression is an attempt to overcome shortcomings of H.261 and JPEG. It achieves compression by reducing both **spatial redundancy** (within a frame, like JPEG) and **temporal redundancy** (between consecutive frames).
    *   **MPEG-1:** Initial standard, suitable for video stored on CD-ROM.
    *   **MPEG-2:** Improved quality, used as the standard for **Digital Television** and **DVD**.
    *   **MPEG-4:** Developed for streaming DVD-quality video at lower data rates and smaller file sizes, supporting video/audio "objects" and 3D content.
*   **H.261 (Video Encoder/Decoder for audiovisual services at p x 64 kbps):** Designed for **video telephony and video conferencing applications** over ISDN lines.
    *   It supports video formats like CIF (Common Intermediate Format) and QCIF.
    *   H.261 uses **Intra-frames (I-frames)**, which are coded independently (like a still image using JPEG concepts).
    *   It uses **Inter-frames (P-frames)** for predictive coding, storing only the difference from a previous frame via **motion compensation**.

#### 4. Transmission of Video Signals

Transmission of video signals depends on the format, where luminance and chrominance components are managed.

*   For higher-quality transmission, the signal output is **RGB** (red, green, and blue) on separate conductors.
*   Alternatively, the output can be split into a **luma component channel (Y)** and two **chroma (color) channels (Cb/Cr or U/V)**.

Video transmission imposes requirements on networks, including:

*   **Real-time Synchronization:** The system must maintain precise **temporal relationships** between audio and video elements.
*   **High Data Throughput:** Due to the large size of video data, systems require high processing power and efficient I/O for real-time delivery.

#### 5. Video Capture

**Video Recording** is the process of converting an analog video signal (such as that produced by a video camera) into **digital video**.

*   **Devices:** Video capture uses a **video device** such as a digital video camera or an analog camera connected to a **video digitizing board**.
*   **Frame Grabber:** A video digitizing board often includes a **frame-grabber** feature, which captures the image and converts it into a color bitmap (e.g., PICT or TIFF).
*   **Process:** The analog signal captured by the camera is converted into a digital form (pixels). During digital capture, the image is represented by three signals produced at each pixel location (R, G, B). These analog signals are converted to digital, truncated to integers, and stored.
