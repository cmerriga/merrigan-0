using System;
using System.Runtime.InteropServices;

namespace Merrigan0 {
    public static class WinMM {
        // msacm.h
        // MMRESULT is uint
        // public struct MMRESULT : uint { };

        // mmsyscom.h
        // HDRVR is UIntPtr
        [StructLayout(LayoutKind.Explicit)]
        public struct MMTIME {
            [FieldOffset(0)]
            public uint wType; // indicates the contents of the union

            [FieldOffset(4)]
            public uint ms; // milliseconds

            [FieldOffset(4)]
            public uint sample; // samples

            [FieldOffset(4)]
            public uint cb; // byte count

            [FieldOffset(4)]
            public uint ticks; // ticks in MIDI stream

            // SMPTE
            [FieldOffset(4)]
            public byte hour; // hours

            [FieldOffset(5)]
            public byte min; // minutes

            [FieldOffset(6)]
            public byte sec; // seconds

            [FieldOffset(7)]
            public byte frame; // frames

            [FieldOffset(8)]
            public byte fps; // frames per second

            [FieldOffset(9)]
            public byte dummy; // pad

            //public byte pad[2]; ////

            [FieldOffset(10)]
            public byte pad0;

            [FieldOffset(11)]
            public byte pad1;

            // MIDI
            [FieldOffset(4)]
            public uint songptrpos; // song pointer position
        };

        // types for wType field in MMTIME struct
        public const uint TIME_MS = 0x0001; // time in milliseconds 
        public const uint TIME_SAMPLES = 0x0002; // number of wave samples
        public const uint TIME_BYTES = 0x0004; // current byte offset
        public const uint TIME_SMPTE = 0x0008; // SMPTE time
        public const uint TIME_MIDI = 0x0010; // MIDI time
        public const uint TIME_TICKS = 0x0020; // Ticks within MIDI stream

        // Multimedia Extensions Window Messages
        public const uint MM_JOY1MOVE = 0x3A0; // joystick
        public const uint MM_JOY2MOVE = 0x3A1;
        public const uint MM_JOY1ZMOVE = 0x3A2;
        public const uint MM_JOY2ZMOVE = 0x3A3;
        public const uint MM_JOY1BUTTONDOWN = 0x3B5;
        public const uint MM_JOY2BUTTONDOWN = 0x3B6;
        public const uint MM_JOY1BUTTONUP = 0x3B7;
        public const uint MM_JOY2BUTTONUP = 0x3B8;

        public const uint MM_MCINOTIFY = 0x3B9; // MCI

        public const uint MM_WOM_OPEN = 0x3BB; // waveform output
        public const uint MM_WOM_CLOSE = 0x3BC;
        public const uint MM_WOM_DONE = 0x3BD;

        public const uint MM_WIM_OPEN = 0x3BE; // waveform input
        public const uint MM_WIM_CLOSE = 0x3BF;
        public const uint MM_WIM_DATA = 0x3C0;

        public const uint MM_MIM_OPEN = 0x3C1; // MIDI input
        public const uint MM_MIM_CLOSE = 0x3C2;
        public const uint MM_MIM_DATA = 0x3C3;
        public const uint MM_MIM_LONGDATA = 0x3C4;
        public const uint MM_MIM_ERROR = 0x3C5;
        public const uint MM_MIM_LONGERROR = 0x3C6;

        public const uint MM_MOM_OPEN = 0x3C7; // MIDI output
        public const uint MM_MOM_CLOSE = 0x3C8;
        public const uint MM_MOM_DONE = 0x3C9;

        // these are also in msvideo.h
        public const uint MM_DRVM_OPEN = 0x3D0; // installable drivers
        public const uint MM_DRVM_CLOSE = 0x3D1;
        public const uint MM_DRVM_DATA = 0x3D2;
        public const uint MM_DRVM_ERROR = 0x3D3;

        // these are used by msacm.h
        public const uint MM_STREAM_OPEN = 0x3D4;
        public const uint MM_STREAM_CLOSE = 0x3D5;
        public const uint MM_STREAM_DONE = 0x3D6;
        public const uint MM_STREAM_ERROR = 0x3D7;

        // For >= WinNT 4.0
        public const uint MM_MOM_POSITIONCB = 0x3CA; // Callback for MEVT_POSITIONCB
        public const uint MM_MCISIGNAL = 0x3CB;
        public const uint MM_MIM_MOREDATA = 0x3CC; // MIM_DONE w/ pending events

        public const uint MM_MIXM_LINE_CHANGE = 0x3D0; // mixer line change notify
        public const uint MM_MIXM_CONTROL_CHANGE = 0x3D1; // mixer control change notify

        // String resource number bases (internal use)
        public const uint MMSYSERR_BASE = 0;
        public const uint WAVERR_BASE = 32;
        public const uint MIDIERR_BASE = 64;
        public const uint TIMERR_BASE = 96;
        public const uint JOYERR_BASE = 160;
        public const uint MCIERR_BASE = 256;
        public const uint MIXERR_BASE = 1024;

        public const uint MCI_STRING_OFFSET = 512;
        public const uint MCI_VD_OFFSET = 1024;
        public const uint MCI_CD_OFFSET = 1088;
        public const uint MCI_WAVE_OFFSET = 1152;
        public const uint MCI_SEQ_OFFSET = 1216;

        public const uint MMSYSERR_NOERROR = 0; // no error
        public const uint MMSYSERR_ERROR = (MMSYSERR_BASE + 1); // unspecified error
        public const uint MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2); // device ID out of range
        public const uint MMSYSERR_NOTENABLED = (MMSYSERR_BASE + 3); // driver failed enable
        public const uint MMSYSERR_ALLOCATED = (MMSYSERR_BASE + 4); // device already allocated
        public const uint MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5); // device handle is invalid
        public const uint MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6); // no device driver present
        public const uint MMSYSERR_NOMEM = (MMSYSERR_BASE + 7); // memory allocation error
        public const uint MMSYSERR_NOTSUPPORTED = (MMSYSERR_BASE + 8); // function isn't supported
        public const uint MMSYSERR_BADERRNUM = (MMSYSERR_BASE + 9); // error value out of range
        public const uint MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10); // invalid flag passed
        public const uint MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11); // invalid parameter passed
        public const uint MMSYSERR_HANDLEBUSY = (MMSYSERR_BASE + 12); // handle being used simultaneously on another thread (eg callback)
        public const uint MMSYSERR_INVALIDALIAS = (MMSYSERR_BASE + 13); // specified alias not found
        public const uint MMSYSERR_BADDB = (MMSYSERR_BASE + 14); // bad registry database
        public const uint MMSYSERR_KEYNOTFOUND = (MMSYSERR_BASE + 15); // registry key not found
        public const uint MMSYSERR_READERROR = (MMSYSERR_BASE + 16); // registry read error
        public const uint MMSYSERR_WRITEERROR = (MMSYSERR_BASE + 17); // registry write error
        public const uint MMSYSERR_DELETEERROR = (MMSYSERR_BASE + 18); // registry delete error
        public const uint MMSYSERR_VALNOTFOUND = (MMSYSERR_BASE + 19); // registry value not found
        public const uint MMSYSERR_NODRIVERCB = (MMSYSERR_BASE + 20); // driver does not call DriverCallback
        public const uint MMSYSERR_MOREDATA = (MMSYSERR_BASE + 21); // more data to be returned
        public const uint MMSYSERR_LASTERROR = (MMSYSERR_BASE + 21); // last error in range

        // Driver callback support

        // flags used with waveOutOpen(), waveInOpen(), midiInOpen(), and midiOutOpen() to specify the type of the dwCallback parameter.
        public const uint CALLBACK_TYPEMASK = 0x00070000u; // callback type mask
        public const uint CALLBACK_NULL = 0x00000000u; // no callback
        public const uint CALLBACK_WINDOW = 0x00010000u; // dwCallback is a HWND
        public const uint CALLBACK_TASK = 0x00020000u; // dwCallback is a HTASK
        public const uint CALLBACK_FUNCTION = 0x00030000u; // dwCallback is a FARPROC
        public const uint CALLBACK_THREAD = (CALLBACK_TASK); // thread ID replaces 16 bit task
        public const uint CALLBACK_EVENT = 0x00050000u; // dwCallback is an EVENT Handle

        public delegate void DRVCALLBACK(
            UIntPtr hdrvr,
            uint uMsg,
            UIntPtr dwUser,
            UIntPtr dw1,
            UIntPtr dw2);

        public const uint WAVE_MAPPER = 0xFFFFFFFF;

        public const uint WAVE_FORMAT_PCM = 1;

        public const uint WAVERR_NONE = 0;
        public const uint WAVERR_BADFORMAT = 32;
        public const uint WAVERR_STILLPLAYING = 33;
        public const uint WAVERR_UNPREPARED = 34;
        public const uint WAVERR_SYNC = 35;
        public const uint WAVERR_LASTERROR = 35;

        public const uint WAVE_INVALIDFORMAT = 0x00000000;

        // 11025 samples/sec
        public const uint WAVE_FORMAT_1M08 = 0x00000001;
        public const uint WAVE_FORMAT_1S08 = 0x00000002;
        public const uint WAVE_FORMAT_1M16 = 0x00000004;
        public const uint WAVE_FORMAT_1S16 = 0x00000008;
        
        // 22050 samples/sec
        public const uint WAVE_FORMAT_2M08 = 0x00000010;
        public const uint WAVE_FORMAT_2S08 = 0x00000020;
        public const uint WAVE_FORMAT_2M16 = 0x00000040;
        public const uint WAVE_FORMAT_2S16 = 0x00000080;
        
        // 44100 samples/sec
        public const uint WAVE_FORMAT_4M08 = 0x00000100;
        public const uint WAVE_FORMAT_4S08 = 0x00000200;
        public const uint WAVE_FORMAT_4M16 = 0x00000400;
        public const uint WAVE_FORMAT_4S16 = 0x00000800;

        public const uint WAVECAPS_PITCH = 0x00000001;
        public const uint WAVECAPS_PLAYBACKRATE = 0x00000002;
        public const uint WAVECAPS_VOLUME = 0x00000004;
        public const uint WAVECAPS_LRVOLUME = 0x00000008;
        public const uint WAVECAPS_SYNC = 0x00000010;
        public const uint WAVECAPS_SAMPLEACCURATE = 0x00000020;
        public const uint WAVECAPS_DIRECTSOUND = 0x00000040;

        public const uint WHDR_DONE = 0x00000001;
        public const uint WHDR_PREPARED = 0x00000002;
        public const uint WHDR_BEGINLOOP = 0x00000004;
        public const uint WHDR_ENDLOOP = 0x00000008;
        public const uint WHDR_INQUEUE = 0x00000010;

        [StructLayout(LayoutKind.Sequential)]
        public class WAVEFORMATEX {
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitsPerSample;
            public ushort cbSize;

            public static WAVEFORMATEX Create(Int32 samplesPerSecond, Int32 bitsPerSample, Int32 nChannels) {
                return new WAVEFORMATEX() {
                    wFormatTag = (ushort)WAVE_FORMAT_PCM,
                    nChannels = (ushort)nChannels,
                    nSamplesPerSec = (uint)samplesPerSecond,
                    nAvgBytesPerSec = (uint)(samplesPerSecond * bitsPerSample * nChannels / 8),
                    nBlockAlign = (ushort)(bitsPerSample * nChannels / 8),
                    wBitsPerSample = (ushort)bitsPerSample,
                    cbSize = 0
                };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class WAVEHDR {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;
        };

        [DllImport("winmm.dll")]
        public static extern uint waveOutGetNumDevs();

        [DllImport("winmm.dll")]
        public static extern uint waveOutPrepareHeader(IntPtr hwo, IntPtr pwh, uint cbwh);

        [DllImport("winmm.dll")]
        public static extern uint waveOutUnprepareHeader(IntPtr hwo, IntPtr pwh, uint cbwh);

        [DllImport("winmm.dll")]
        public static extern uint waveOutWrite(IntPtr hwo, IntPtr pwh, uint cbwh);

        [DllImport("winmm.dll")]
        public static extern uint waveOutOpen(
            ref IntPtr hwo, 
            uint uDeviceID,
            // Winmm.WAVEFORMATEX pwfx,
            IntPtr pwfx,
            WaveOutProc dwCallback,
            IntPtr dwCallbackInstance,
            uint fdwOpen);

        [DllImport("winmm.dll")]
        public static extern uint waveOutReset(IntPtr hwo);

        [DllImport("winmm.dll")]
        public static extern uint waveOutClose(IntPtr hwo);

        public delegate void WaveOutProc(IntPtr hwo, uint uMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);
        public delegate void WaveInProc(IntPtr hwi, uint uMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);

        // https://learn.microsoft.com/en-us/windows/win32/multimedia/multimedia-timer-functions
        // https://pinvoke.net/default.aspx/winmm.timeSetEvent
        // timeapi.h, or maybe timerapi.h, mmsyscom.h

        // timeapi.h
        public const uint TIMERR_NOERROR = 0; // no error
        public const uint TIMERR_NOCANDO = TIMERR_BASE + 1; // request not completed
        public const uint TIMERR_STRUCT = TIMERR_BASE + 33; // time struct size

        public struct TIMECAPS {
            public uint wPeriodMin; // minimum period supported
            public uint wPeriodMax; // maximum period supported
        };

        [DllImport("winmm.dll")]
        public static extern uint timeGetSystemTime(
            ref MMTIME pmmt,
            uint cbmmt);

        [DllImport("winmm.dll")]
        public static extern uint timeGetTime();

        [DllImport("winmm.dll")]
        public static extern uint timeGetDevCaps(
            ref TIMECAPS ptc,
            uint cbtc);

        [DllImport("winmm.dll")]
        public static extern uint timeBeginPeriod(
            uint uPeriod); // milliseconds

        [DllImport("winmm.dll")]
        public static extern uint timeEndPeriod(
            uint uPeriod); // milliseconds

        // mmiscapi2.h
        public delegate void TIMECALLBACK(
            uint uTimerID,
            uint uMsg,
            UIntPtr dwUser,
            UIntPtr dw1,
            UIntPtr dw2);

        [DllImport("winmm.dll")]
        public static extern uint timeKillEvent(
            uint uTimerID);

        [DllImport("winmm.dll")]
        public static extern uint timeSetEvent(
            uint uDelay,
            uint uResolution,
            TIMECALLBACK lpTimeProc,
            UIntPtr dwUser,
            uint fuEvent);

        // mmiscapi2.h
        public const uint TIME_ONESHOT = 0x0000; // program timer for single event
        public const uint TIME_PERIODIC = 0x0001; // program for continuous periodic event

        // mmiscapi2.h
        public const uint TIME_CALLBACK_FUNCTION = 0x0000; // callback is function
        public const uint TIME_CALLBACK_EVENT_SET = 0x0010; // callback is event - use SetEvent
        public const uint TIME_CALLBACK_EVENT_PULSE = 0x0020; // callback is event - use PulseEvent

        // Only for >= Windows XP
        public const uint TIME_CALLBACK_KILL_SYNCHRONOUS = 0x0100; // This flag prevents the event from occurring after the user calls timeKillEvent() to destroy it.
    }
}
