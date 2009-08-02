using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Threading;

namespace WPFGui
{
    class HotKey
    {
        [DllImport("user32", SetLastError = true)]
        private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
        [DllImport("user32", SetLastError = true)]
        private static extern int UnregisterHotKey(IntPtr hwnd, int id);
        [DllImport("kernel32", SetLastError = true)]
        private static extern short GlobalAddAtom(string lpString);
        [DllImport("kernel32", SetLastError = true)]
        private static extern short GlobalDeleteAtom(short nAtom);

        public const int MOD_ALT = 1;
        public const int MOD_CONTROL = 2;
        public const int MOD_SHIFT = 4;
        public const int MOD_WIN = 8;

        public const int WM_HOTKEY = 0x312;

        // the id for the hotkey
        private short _hotkeyID;
        private IntPtr _handle;

        public HotKey(IntPtr handle)
        {
            _handle = handle;
        }

        ~HotKey()
        {
            UnregisterGlobalHotKey();
        }

        // register a global hot key
        public void RegisterGlobalHotKey(Key hotkey, int modifiers)
        {
            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN docs)
                // XXX: :/
                string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + "WPFGUI";
                _hotkeyID = GlobalAddAtom(atomName);
                if (_hotkeyID == 0)
                {
                    throw new Exception("Unable to generate unique hotkey ID. Error code: " +
                       Marshal.GetLastWin32Error().ToString());
                }

                // register the hotkey, throw if any error
                if (RegisterHotKey(_handle, _hotkeyID, modifiers, (int)KeyInterop.VirtualKeyFromKey(hotkey)) == 0)
                {
                    throw new Exception("Unable to register hotkey. Error code: " + Marshal.GetLastWin32Error()
                       .ToString());
                }
            }
            catch (Exception)
            {
                // clean up if hotkey registration failed
                UnregisterGlobalHotKey();
            }
        }

        // unregister a global hotkey
        public void UnregisterGlobalHotKey()
        {
            if (this._hotkeyID != 0)
            {
                UnregisterHotKey(_handle, _hotkeyID);
                // clean up the atom list
                GlobalDeleteAtom(_hotkeyID);
                _hotkeyID = 0;
            }
        }

    }
}
