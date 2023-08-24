using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace RfId.Core
{
    using DL_STATUS = System.UInt32;

    public enum ERRORCODES
    {

        DL_OK = 0x00,
        COMMUNICATION_ERROR = 0x01,
        CHKSUM_ERROR = 0x02,
        READING_ERROR = 0x03,
        WRITING_ERROR = 0x04,
        BUFFER_OVERFLOW = 0x05,
        MAX_ADDRESS_EXCEEDED = 0x06,
        MAX_KEY_INDEX_EXCEEDED = 0x07,
        NO_CARD = 0x08,
        COMMAND_NOT_SUPPORTED = 0x09,
        FORBIDEN_DIRECT_WRITE_IN_SECTOR_TRAILER = 0x0A,
        ADDRESSED_BLOCK_IS_NOT_SECTOR_TRAILER = 0x0B,
        WRONG_ADDRESS_MODE = 0x0C,
        WRONG_ACCESS_BITS_VALUES = 0x0D,
        AUTH_ERROR = 0x0E,
        PARAMETERS_ERROR = 0x0F,
        MAX_SIZE_EXCEEDED = 0x10,
        UNSUPPORTED_CARD_TYPE = 0x11,

        COMMUNICATION_BREAK = 0x50,
        NO_MEMORY_ERROR = 0x51,
        CAN_NOT_OPEN_READER = 0x52,
        READER_NOT_SUPPORTED = 0x53,
        READER_OPENING_ERROR = 0x54,
        READER_PORT_NOT_OPENED = 0x55,
        CANT_CLOSE_READER_PORT = 0x56,

        WRITE_VERIFICATION_ERROR = 0x70,
        BUFFER_SIZE_EXCEEDED = 0x71,
        VALUE_BLOCK_INVALID = 0x72,
        VALUE_BLOCK_ADDR_INVALID = 0x73,
        VALUE_BLOCK_MANIPULATION_ERROR = 0x74,
        WRONG_UI_MODE = 0x75,
        KEYS_LOCKED = 0x76,
        KEYS_UNLOCKED = 0x77,
        WRONG_PASSWORD = 0x78,
        CAN_NOT_LOCK_DEVICE = 0x79,
        CAN_NOT_UNLOCK_DEVICE = 0x7A,
        DEVICE_EEPROM_BUSY = 0x7B,
        RTC_SET_ERROR = 0x7C,
        ANTICOLLISION_DISABLED = 0x7D,
        NO_CARDS_ENUMERRATED = 0x7E,
        CARD_ALREADY_SELECTED = 0x7F,

        FT_STATUS_ERROR_1 = 0xA0,
        FT_STATUS_ERROR_2 = 0xA1,
        FT_STATUS_ERROR_3 = 0xA2,
        FT_STATUS_ERROR_4 = 0xA3,
        FT_STATUS_ERROR_5 = 0xA4,
        FT_STATUS_ERROR_6 = 0xA5,
        FT_STATUS_ERROR_7 = 0xA6,
        FT_STATUS_ERROR_8 = 0xA7,
        FT_STATUS_ERROR_9 = 0xA8

    }


    unsafe class uFCoder
    {
#if WIN64
        const string DLL_PATH = "..\\..\\..\\ufr-lib\\windows\\x86_64\\";
        const string NAME_DLL = "uFCoder-x86_64.dll";

#else
        const string DLL_PATH = "..\\..\\ufr-lib\\windows\\x86\\";
        const string NAME_DLL = "uFCoder-x86.dll";
#endif
        const string DLL_NAME = DLL_PATH + NAME_DLL;

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderOpen")]
        public static extern DL_STATUS ReaderOpen();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "ReaderOpenEx")]
        private static extern DL_STATUS ReaderOpenEx(UInt32 reader_type, [In] byte[] port_name, UInt32 port_interface, [In] byte[] arg);
        public static DL_STATUS ReaderOpenEx(UInt32 reader_type, string port_name, UInt32 port_interface, string arg)
        {

            byte[] port_name_p = Encoding.ASCII.GetBytes(port_name);
            byte[] port_name_param = new byte[port_name_p.Length + 1];
            Array.Copy(port_name_p, 0, port_name_param, 0, port_name_p.Length);
            port_name_param[port_name_p.Length] = 0;

            byte[] arg_p = Encoding.ASCII.GetBytes(arg);
            byte[] arg_param = new byte[arg_p.Length + 1];
            Array.Copy(arg_p, 0, arg_param, 0, arg_p.Length);
            arg_param[arg_p.Length] = 0;

            return ReaderOpenEx(reader_type, port_name_param, port_interface, arg_param);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderClose")]
        public static extern DL_STATUS ReaderClose();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderType")]
        public static extern DL_STATUS GetReaderType(ulong* get_reader_type);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderSerialNumber")]
        public static extern DL_STATUS GetReaderSerialNumber(ulong* serial_number);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardId")]
        public static extern DL_STATUS GetCardId(byte* card_type,
                                                 ulong* card_serial);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetDlogicCardType")]
        public static extern DL_STATUS GetDlogicCardType(byte* bCardType);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardIdEx")]
        public static extern DL_STATUS GetCardIdEx(byte* bCardType,
                                                   byte* bCardUID,
                                                   byte* bUidSize);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderUISignal")]
        public static extern DL_STATUS ReaderUISignal(int light_mode, int sound_mode);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "LinearRead")]
        public static extern DL_STATUS LinearRead([Out] byte[] data,
                                                  ushort linear_address,
                                                  ushort data_len,
                                                  ushort* bytes_written,
                                                  byte key_mode,
                                                  byte key_index);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "LinearWrite")]
        public static extern DL_STATUS LinearWrite(byte* aucData,
                                                   ushort linear_address,
                                                   ushort data_len,
                                                   ushort* bytes_written,
                                                   byte auth_mode,
                                                   byte key_index);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "LinearFormatCard")]
        public static extern DL_STATUS LinearFormatCard(byte* new_key_A,
                                                        byte blocks_access_bits,
                                                        byte sector_trailers_access_bits,
                                                        byte sector_trailers_byte9,
                                                        byte* new_key_B,
                                                        byte* sectors_formatted,
                                                        byte auth_mode,
                                                        byte key_index);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderKeyWrite")]
        public static extern DL_STATUS ReaderKeyWrite(byte* aucKey,
                                                      byte ucKeyIndex);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardSize")]
        public static extern DL_STATUS GetCardSize(ushort* linearSize, int* rawSize);

    }
}
