This document provides comprehensive study notes for Unit 2: Text / Sound / Audio System, covering all required topics for your exam preparation.

***

## Unit 2: Text / Sound / Audio System

### A. Text

Text is the **most widely used and flexible means** of presenting information on screen and conveying ideas. It is a fundamental component of multimedia, providing context and information. Text in multimedia is typically combined with other media types such as graphics, audio, and video.

Reading large volumes of text on a computer screen can be difficult and tiring, so text quantity should be minimized unless the application involves extensive reference material. For design purposes, text should ideally fill **less than half the screen**. Text is classified as a **discrete medium**, meaning it is time-independent.

#### 1. Types of Text

Text in digital form can be broadly classified into three categories:

| Type of Text | Description | Format/Use |
| :--- | :--- | :--- |
| **Plain (Unformatted) Text** | Raw text without any appearance changes like font style, size, or color. It is the most portable format and typically refers to textual data in **ASCII format**. | Used by simple editors like Notepad (.txt files). |
| **Formatted Text** | Text in which the appearance (font style, size, colors) is changed using text editors or word processing applications. | Typically associated with programs like Microsoft Word (.doc files). |
| **Rich Text Format (RTF)** | A document file format developed by Microsoft for **cross-platform document interchange**. RTF files can include extra information beyond plain text, such as **font style, formatting, images, pictures, objects, annotations (comments), and security information** (Password/Encryption). | Useful for basic formatted documents like resumes and instructions. |
| **Hypertext** | Text that contains **links to other texts** (invented by Ted Nelson in 1965). Traversal through hypertext pages is usually **non-linear**. | Used by the **Hypertext Markup Language (HTML)** to create web pages. |

#### 2. Character, Character Set, and Codes

*   **Character:** The basic unit of text includes alphanumeric characters and special symbols.
*   **Codes/ASCII:** Computers customarily represent text using the **ASCII (American Standard Code for Information Interchange)** system, which assigns a number (represented as a binary number) to each character found on a typical typewriter.
*   **Character Set:** A collection of letters and symbols used in a writing system.
*   **Character Encoding:** The key that maps a particular byte or sequence of bytes to specific characters that a font renders as text. If the wrong encoding is applied, the result will be unintelligible.

#### 3. Unicode

*   **Unicode** is a character set containing characters for **most of the living languages and scripts in the world**.
*   **UTF-8 (Unicode Transformation Format - 8)** is the recommended character encoding for content developers and programmers.
*   **Escapes** are a way of representing a character using only ASCII text, typically for characters not available in the current encoding or to avoid syntax conflicts.

#### 4. Encryption

Encryption is the process of converting **plaintext** (readable data) into **ciphertext** (scrambled data) using **algorithms and keys** to prevent unauthorized access.

The fundamental goal of encryption is to ensure **confidentiality, integrity, authentication, and non-repudiation**.

*   **Symmetric Encryption:** Uses the **same key** for both encryption and decryption (e.g., AES, 3DES, ChaCha20). It is fast and efficient for encrypting large volumes of data.
*   **Asymmetric Encryption (Public Key Cryptography):** Uses a mathematically related **pair of keys** (a public key for encryption and a private key for decryption). This solves the key distribution problem but is computationally intensive and slower (e.g., RSA, ECC).
*   **Hybrid Encryption Systems:** Most secure communications combine both, using **asymmetric encryption to securely exchange a symmetric session key**, which is then used for the fast encryption of the bulk data (e.g., HTTPS).

#### 5. Ways to Present Text and Aspects of Text Design (Typography)

**Typography** refers to the design and arrangement of text, including the selection of appropriate fonts, sizes, and spacing to ensure the text is readable and visually appealing.

##### Font and Typeface:

*   **Typeface** is a family of graphic characters that typically includes many sizes and styles (e.g., Times New Roman).
*   A **Font** is a collection of characters of a **single size and style** belonging to a particular typeface family.

##### Serif vs. Sans Serif:

*   **Serif Fonts:** Feature a small decoration (**serif**) at the end of a letter stroke (e.g., Times, Bookman). They are generally used for the **body of the text** for better readability.
*   **Sans Serif Fonts:** Lack serifs (e.g., Arial, Optima, Verdana). They are generally used for **headings**.

##### Font Characteristics:

*   **Size:** Measured in **points** (one point is approximately 1/72 of an inch).
*   **Leading:** The space between lines.
*   **Kerning:** The spacing between individual characters.
*   **Vector Font Format:** Character descriptions are stored **mathematically** and revealed as TrueType fonts (.TTF); scaling characters does not cause distortion.
*   **Bitmap Font Format:** Character description is a collection of pixels; scaling can cause distortion.
*   **Anti-aliasing** can be used to make text look gentle and blended.

##### Font Standards:

*   **Postscript Fonts (Adobe):** Designed to produce exceptionally good-looking type when printed on a high-resolution printer. Requires multiple files (printer font, screen fonts).
*   **TrueType Fonts:** Uses a variant of Postscript technology and requires **only one file** installed on the host computer. It is commonly used by both Macintosh and Windows platforms.

##### Presentation Guidelines:

*   For small type, use the most legible font.
*   Use concise information presented under clear, separate headings.
*   For subtitles in films, it is recommended to use **Sans Serif fonts** (such as Univers45, Antique Olive, or Tiresias) over dynamic content. A **black outline** or soft shadow should be used around subtitles to ensure readability against changing or contrasting backgrounds.

##### Font Editing Tools:

Tools can be used to create customized fonts or special characters and symbols for multimedia projects.
*   **Fontographer**.
*   **FontMonger:** A font conversion utility that allows for some font customization.
*   **Cool 3D Text**.

***

### B. Sound / Audio System

**Sound** refers to the analogue form, produced when waves of varying pressure travel through a medium (usually air). **Audio** is the terminology used for the digitized form of sound. Audio is a **continuous medium** because its validity depends on a time condition.

#### 1. Basic Sound Concepts and Types of Sound

*   **Key Sound Characteristics:** Frequency and Amplitude.
*   **Types of Audio in Multimedia:**
    1.  **Music:** Used to set the mood of the presentation, enhance emotion, and illustrate points.
    2.  **Sound effects:** Used to emphasize specific points (e.g., explosions, squeaky doors).
    3.  **Narration:** The most direct message, often very effective.

#### 2. Digitizing Sound (Digital Audio)

Digital audio is created when an analog sound wave is converted into digital numbers. The core technique used for this conversion is **Pulse Code Modulation (PCM)**.

The digitization process involves three main steps:

1.  **Sampling:** A sound wave is captured in its analog form (using microphones), and values are extracted (sampled) at specific points in time. The **Sampling Rate** is the number of audio samples carried per second (measured in Hz or kHz).
    *   **Nyquist Theorem:** The minimum sampling frequency used for analog-to-digital (A/D) conversion must be at least **twice the highest frequency** of the signal being measured.
2.  **Quantization:** This process maps a large set of continuous input amplitude values to a smaller, countable set (discrete values). This typically involves reducing precision, which leads to **lossy data compression** if subsequent steps exploit this.
3.  **Encoding:** A binary code is assigned to each quantized sample. During sound reproduction, digital pulses are decoded back into amplitude values, filtered, and converted back to analog form.

#### 3. MIDI (Musical Instrument Digital Interface)

**MIDI** is a communication **standard or protocol** developed for electronic musical instruments and computers.

*   **Functionality:** The MIDI standard defines how to code all the elements of **musical scores**, including the sequences of notes, timing conditions, and the instrument assigned to play each note.
*   **Data Structure:** A MIDI file is **not an audio file format** itself; it is simply a list of musical notes or commands that a synthesizer can play.
*   **Transmission:** MIDI data stream is a unidirectional asynchronous bit stream at 31.25 Kbits/sec. Data output is typically from a **MIDI controller** (like a keyboard) or a **MIDI sequencer**.

##### MIDI versus Digital Audio (Comparison Points):

*   **Data Type:** Digital audio (e.g., WAV, MP3) is a raw bit sequence representing pressure waves. MIDI is a set of instructions/commands (symbolic representation).
*   **Semantic Content:** MIDI preserves the **semantic description** of the sound (it knows what notes and instrument are being played). Digital audio does not, unless complex recognition techniques are used.
*   **File Size:** MIDI files are generally **small in size** because they only store instructions, not the actual sampled audio data.
*   **Editing:** Working on MIDI files requires knowledge of music theory.

#### 4. Audio Formats

Audio formats dictate how sound is compressed and stored. The way audio is compressed and stored is known as the **codec** (coder-decoder or compression/decompression).

*   **MP3 (MPEG Layer-3):** The most popular format for downloading and storing music. MP3 uses lossy DCT-based compression (source encoding) combined with Huffman coding (entropy encoding).
*   **WAV (Waveform Audio):** A common digital audio file format.
*   **AIFF (Audio Interchange File Format):** Another digital audio format.
*   **MPEG-4 AAC Audio (.m4a, .m4b, .m4p):** A modern audio format.
*   **Red Book Audio:** The main music format used for Audio CD playback.

#### 5. Audio Tools

*   **Sound Editing Tools:** Applications used for digital audio manipulation. Examples include:
    *   **Audacity**
    *   **Power Sound Editor**
    *   **Sound Forge**
    *   **Adobe Audition**
    *   **Cool Edit**
*   **Basic Sound Editing Operations:**
    *   **Trimming:** Removing dead air or unnecessary extra time from the beginning or end of a recording.
    *   **Equalization:** Digital capabilities that allow the modification of recording frequency content to make it sound brighter or darker.

#### 6. Speech Generation (Synthesis)

**Speech Generation** (or Text-to-Speech, TTS) systems convert linguistic representations into sound. A typical TTS system is composed of two main parts:

1.  **Front-End:**
    *   Performs **text normalization** (pre-processing) by converting raw text (numbers, abbreviations) into written-out words.
    *   Assigns phonetic transcriptions to each word (**grapheme-to-phoneme conversion**).
    *   Divides and marks the text into prosodic units (phrases, clauses).
2.  **Back-End (Synthesizer):** Converts the symbolic linguistic representation (phonetic transcriptions and prosody) into sound.

#### 7. Speech Analysis and Speech Recognition

*   **Speech Recognition:** The process used to **analyze and classify speech or vocal tract patterns** and convert them into digital codes for computer entry.
*   **Applications:** Voice recognition systems are used in industrial settings for inspection, inventory, and quality control, enabling hands-free data entry. It is expected to become very popular for word processing applications.
*   **Speech Input Goal:** To detect the speech contents themselves, often to generate a piece of text (e.g., speech-controlled typewriters).
*   **Voice Response Devices:** Software used to verbally guide an operator, typically employed by voice-messaging minicomputers and mainframe audio response units.

#### 8. Speech Transmission

**Speech Encoding** is an important category of audio data compression.

*   The **range of frequencies** required for human voice is generally much **narrower** and less complex than that needed for music. As a result, speech can be encoded at high quality using a relatively **low bit rate**.
*   Transmission techniques often rely on **perceptual models** to estimate which aspects of speech the human ear can hear.
*   **Differential Coding** techniques, such as **Adaptive Differential Pulse Code Modulation (ADPCM)**, are used in the quantization and transmission of audio to predict subsequent signal values based on previous ones, improving compression efficiency.

***

**Analogy for Digitizing Sound:**

Think of recording sound like taking photographs of a running race.

1.  **Sampling** is deciding *how often* you take a picture (the sampling rate). If you only take one picture per lap (low rate), you might miss the crucial moments. The Nyquist Theorem tells you to take pictures at least twice as fast as the fastest action (highest frequency) to capture all movement accurately.
2.  **Quantization** is deciding *how much detail* you record in each picture (the bit depth). If you use only black and white (low bit depth/quantization), you lose color information. Quantization intentionally reduces precision to save space, assuming the lost detail is negligible.
3.  **Encoding** is labeling and saving the digital photos into a format (like WAV or MP3) so the computer can understand and reproduce the race later.
