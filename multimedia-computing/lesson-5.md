The following notes cover the concepts, techniques, and standards required for your exam preparation on Unit 5: Data Compression.

***

## Unit 5: Data Compression

### I. Introduction, Basics, Storage Space, and Need for Compression

#### A. Introduction and Basics of Compression

**Data compression**, also known as **source coding** or **bit-rate reduction**, is the process of **encoding information using fewer bits than the original representation**. Multimedia compression involves employing tools and techniques to reduce the file size of various media formats.

The theoretical basis for compression is provided by **information theory** and, more specifically, **Shannon's source coding theorem**.

#### B. Storage Space and Need for Coding

Multimedia systems, handling images, video, and audio, deal with **large volumes of data**. Uncompressed graphics, audio, and video require substantial storage capacity.

*   **Uncompressed Data Rates:** Uncompressed audio and video demand very high data rates. For example:
    *   CD-quality audio (44.1 KHz, 16 bit, 2 channels) requires about $\mathbf{1.4 \text{ Mbps}}$.
    *   Uncompressed high-definition video requires **more than 2 Gbps**.
    *   $\mathbf{640 \times 480}$ resolution, 24-bit color, $\mathbf{30 \text{ fps}}$ video requires $\mathbf{27.6 \text{ Mbytes per second}}$.
*   **Compression is necessary** because:
    1.  Low network bandwidth does not allow for real-time video transmission.
    2.  Slow storage or processing devices do not allow for fast playback.
    3.  Compression reduces storage requirements. Large $\mathbf{24 \text{-bit images}}$ necessitate compression due to the resulting large file sizes.

#### C. Codec and Evaluation Criteria

The term **Codec** is a short name for **“coder-decoder,”** **“compression/decompression,”** or **“compressor/decompressor”**. A codec is the computer program that shrinks large files and makes them playable.

**Data Redundancy** is a central issue in digital image compression. Data compression is performed by reducing the amount of data required to represent a given quantity of information.

**Compression Ratio ($\text{CR}$)** is defined as the ratio of the size of the original data ($\text{B}_o$) to the size of the compressed data ($\text{B}_1$):
$$\text{CR} = \frac{\text{B}_o}{\text{B}_1}$$
A compression ratio greater than $\mathbf{1}$ indicates positive compression.

### II. Lossy and Lossless Compression Techniques

Compression is broadly divided into two categories: **lossless** and **lossy** methods.

#### A. Lossless Compression

*   **Definition:** Lossless compression reduces bits by identifying and eliminating **statistical redundancy**. **No information is lost** in lossless compression.
*   **Reversibility:** The compressed data is an **exact replication of the original data** upon decompression. The decompression process is reversible.
*   **Use Cases:** Preferred when preserving the original quality is critical, such as for archiving data or when the data will undergo further editing.
*   **Compression Ratio:** Typically **lower** compared to lossy methods (e.g., typically $\mathbf{2:1}$ to $\mathbf{3:1}$).
*   **Examples of Algorithms/Formats:**
    *   **Huffman Coding**.
    *   **Run-Length Encoding (RLE)**.
    *   **Lempel Ziv Welch (LZW)**. LZW is used in the **GIF format** and UNIX compress.
    *   **Arithmetic Coding**.
    *   Audio formats like **FLAC** (Free Lossless Audio Codec), **WAV, AIFF, M4A, MQA, WMA,** and **ALAC** are examples of uncompressed or lossless formats. FLAC typically achieves a compression ratio of about $\mathbf{30\% \text{ to } 50\%}$ reduction in file size while retaining quality.

#### B. Lossy Compression

*   **Definition:** Lossy compression reduces bits by **removing unnecessary or less important information**. Some quality may be lost or degraded, but the files tend to be much smaller.
*   **Irreversibility:** Once compressed, the audio/image **will not decompress back to the original size**. The original file cannot be taken back after decompression.
*   **Mechanism:** Lossy schemes exploit perceptual differences (e.g., **psychoacoustics** for sound and **psychovisuals** for images/video). For instance, the human eye is more sensitive to luminance variations than to color variations.
*   **Use Cases:** Used extensively for multimedia streaming, digital cameras, DVDs, and web applications.
*   **Compression Ratio:** The compression ratio is generally **very high** (e.g., $\mathbf{10:1}$ or higher, sometimes up to $\mathbf{100:1}$).
*   **Examples of Algorithms/Formats:**
    *   **Discrete Cosine Transform (DCT)**.
    *   **Quantization** (the main lossy step).
    *   Audio formats like **MP3, AAC, and Ogg Vorbis (OGG)**.
    *   Image formats like **JPEG**.

### III. Source, Entropy, and Hybrid Coding

Multimedia compression techniques are classified into three types of coding.

#### A. Source Coding

*   **Definition:** Source coding is often **lossy** and is distinguished by taking into account the **semantics** (characteristics) of the data being compressed.
*   **Purpose:** The source encoder is responsible for reducing or eliminating **interpixel redundancy** or **psychovisual redundancy** in the input image.
*   **Process:** This process involves transforming the input data into a new format (often nonvisual) to reduce redundancy, followed by **quantization** (the irreversible, lossy step that reduces psychovisual redundancy).
*   **Examples:** DCT, DPCM (Differential Pulse Code Modulation), ADPCM (Adaptive Differential Pulse Code Modulation), and color model transforms.

#### B. Entropy Coding

*   **Definition:** Entropy coding is a **lossless** process that can be used for different media regardless of the medium's specific characteristics (the data is viewed as a sequence of digital values, and semantics are ignored).
*   **Purpose:** It performs a final, lossless compression step by eliminating **coding redundancy**. It assigns the shortest code words to the **most frequently occurring output values**.
*   **Variable-Length Coding (VLC):** Entropy coding typically uses VLC, where frequently appearing symbols are coded with fewer bits per symbol.
*   **Examples:** **Run-length Coding**, **Huffman Coding**, and **Arithmetic Coding**.

#### C. Hybrid Coding

*   **Definition:** Hybrid coding combines **entropy encoding** (lossless) with **source encoding** (often lossy).
*   **Mechanism:** Source encoding reduces the data size (e.g., via transformation and quantization), while entropy encoding optimally represents the remaining data using lossless techniques.
*   **Applications:** Most multimedia systems use hybrid techniques. **JPEG, MPEG-2, MPEG-4,** and **H.264** are all examples of hybrid coding schemes.

### IV. Lossy Sequential DCT-Based Mode and Expanded Lossy DCT-Based Mode (JPEG)

**JPEG (Joint Photographic Experts Group)** is a standardized compression technique for still images. It is typically a **lossy** compression method that employs a transform coding method using the **Discrete Cosine Transform (DCT)**. The objective is to remove information that is not easily visible to the human eye, especially high-frequency elements.

#### A. Lossy Sequential DCT-Based Mode (Baseline Process)

This is the **default JPEG mode** and the **baseline process** that every JPEG decoder must support.

The main steps in JPEG compression are performed in a sequential order:

1.  **Preparation (Image Block Division):** The image is divided into $\mathbf{8 \times 8}$ pixel blocks. For color images, the RGB space is typically converted to $\mathbf{YIQ}$ or $\mathbf{YUV}$ (or $\text{YCbCr}$), and chroma components are subsampled (e.g., 4:2:0 scheme).
2.  **DCT Transformation (Source Encoding):** Each pixel value in the $8 \times 8$ block is shifted (typically by subtracting 128). **Forward DCT (FDCT)** is applied to each block, converting the block into 64 coefficients in the frequency domain ($\text{S}_{uv}$).
    *   The **DC coefficient ($\mathbf{S}_{00}$)** corresponds to the lowest frequency and represents the fundamental color/average brightness of the block. The remaining 63 are **AC coefficients**.
    *   The DCT is highly effective because it concentrates most of the image energy into a few low-frequency coefficients.
3.  **Quantization (Lossy Step):** This is the **lossy phase**. Each DCT coefficient is divided by a corresponding value from a quantization table $\mathbf{(Q_{uv})}$. This reduces the accuracy of the coefficients, especially the high-frequency AC coefficients, as the human eye is less sensitive to them.
4.  **Entropy Encoding Preparation:**
    *   **DC Coefficients:** Encoded using **DPCM** (Differential Pulse Code Modulation). The difference between the current DC coefficient ($\text{DC}_i$) and the previous one ($\text{DC}_{i-1}$) is stored and further processed.
    *   **AC Coefficients:** Processed using **zig-zag ordering**. This orders the coefficients from low to high frequencies, creating long **runs of zeros**.
5.  **Entropy Coding (Final Lossless Step):**
    *   **Run-Length Encoding (RLE)** is applied to the zero values.
    *   For the baseline mode, **Huffman encoding** is used to compress the resulting data.

#### B. Expanded Lossy DCT-Based Mode

The expanded lossy DCT-based mode offers further enhancements to the baseline mode.

*   **Bit Depth:** This mode supports **12 bits per sample value** in addition to 8 bits.
*   **Progressive Coding:** It includes sequential coding and **progressive coding**. Progressive decoding refines a rough image over successive passes.
    *   **Spectral Selection:** DCT coefficients are divided into spectral bands, and lower-frequency coefficients are sent first to progressively refine the image borders and outlines.
    *   **Successive Approximation:** All DCT coefficients are encoded simultaneously, but the **Most Significant Bits (MSBs)** are sent first, and less significant bits are sent in later scans to refine quality.
*   **Entropy Coding:** This mode allows for both **Huffman coding** and **Arithmetic Coding** (though arithmetic coding is more complex and patented). Arithmetic encoding may achieve $\mathbf{5 \text{ to } 10 \text{ percent}}$ better compression than Huffman encoding.

### V. JPEG and MPEG Compression

MPEG (Moving Picture Experts Group) is a working group of ISO/IEC that develops video and audio encoding standards. MPEG compression is an attempt to overcome shortcomings of H.261 and JPEG.

| Standard | Focus | Compression Technique | Redundancy Targeted |
| :--- | :--- | :--- | :--- |
| **JPEG** | Still Images (Photographic) | **Lossy Hybrid:** DCT + Quantization + Huffman/Arithmetic Coding | Spatial (within-image) |
| **MPEG** | Video and General Audio | **Lossy Hybrid:** Motion Compensation + DCT + Quantization + Entropy Coding | Spatial and **Temporal (between frames)** |

#### A. MPEG Video Compression

MPEG video coding standards, such as MPEG-1 and MPEG-2, typically rely on the hybrid technique of **motion-compensated DCT video coding**.

*   **Motion Compensation (MC):** Video consists of a time-ordered sequence of frames. MPEG exploits **temporal redundancy** by using motion compensation, where parts of a previous (or future) picture can be reused in a subsequent picture. Instead of coding every new image independently, only the **difference** between the current frame and another frame is coded.
*   **DCT Application:** The residual error (difference macroblock) remaining after motion compensation is sent to a **DCT routine**, followed by quantization and variable-length coding (entropy coding).

#### B. MPEG Audio Compression

MPEG Audio, including MP3 (MPEG Layer 3), uses a waveform coding approach.

*   **Psychoacoustic Model:** The primary technique is **perceptual coding**. Compression relies on a **psychoacoustic model** of hearing, recognizing that the human auditory system is not accurate within a certain frequency range and often cannot perceive sounds masked by louder ones.
*   **Subbands:** The encoder uses a bank of filters to analyze the audio signal by calculating a frequency transform, decomposing the signal into subbands.
*   **Standards:** MPEG Audio Layers define different maximum bitrates: Layer 1 allows $\mathbf{448 \text{ Kbits/second}}$, Layer 2 allows $\mathbf{384 \text{ Kbits/second}}$, and **Layer 3** (MP3) allows $\mathbf{320 \text{ Kbits/second}}$, all sampled at 16 bits.

***

**Analogy for Lossy Compression (Quantization):**

Imagine you are hired to paint a mural based on a high-resolution photograph (the original image).

*   **Lossless compression** would be like having infinite time and a perfect cataloging system to record the exact color code of *every single atom* of paint. The reproduction is perfect, but the instructions are massive.
*   **Lossy compression (quantization)** is like being told to mix only 256 colors for the entire mural (reducing the color depth). You simplify millions of subtle shades into the closest 256 available colors. You are also told that people won't notice small details (high frequencies), so you ignore the tiniest specks of texture. The resulting instructions (the compressed file) are much shorter, and the final mural (the reconstructed image) looks almost identical to a casual observer, but it is mathematically impossible to restore the original, continuous range of shades.
