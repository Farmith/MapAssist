/**
 *   Copyright (C) 2021 okaygo
 *
 *   https://github.com/misterokaygo/MapAssist/
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 **/

using MapAssist.Types;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Numerics;

namespace MapAssist.Helpers
{
    class GameMemory
    {
        private static string ProcessName = Encoding.UTF8.GetString(new byte[] { 68, 50, 82 });
        private static IntPtr AdrPlayerUnit = IntPtr.Zero;
        private static IntPtr PtrPlayerUnit = IntPtr.Zero;

        public static GameData GetGameData()
        {
            var addressBuffer = new byte[8];
            var dwordBuffer = new byte[4];
            var byteBuffer = new byte[1];
            var stringBuffer = new byte[16];

            IntPtr processHandle = IntPtr.Zero;
            // Clean up and organize, add better exception handeling.
            try
            {
                Process[] process = Process.GetProcessesByName(ProcessName);
                Process gameProcess = process.Length > 0 ? process[0] : null;

                // Check which GameClient is in focus, and use that for the Overlay
                var windowInFocus = WindowsExternal.GetForegroundWindow();
                foreach (Process p in process)
                {
                    if (windowInFocus == p.MainWindowHandle)
                    {
                        gameProcess = p;
                    }
                }
                if (gameProcess == null)
                {
                    throw new Exception("Game process not found.");
                }

                processHandle =
                    WindowsExternal.OpenProcess((uint)WindowsExternal.ProcessAccessFlags.VirtualMemoryRead, false, gameProcess.Id);
                IntPtr processAddress = gameProcess.MainModule.BaseAddress;

                if (PtrPlayerUnit == IntPtr.Zero)
                {
                    IntPtr pUnitTable = IntPtr.Add(processAddress, Offsets.UnitTable);
                    for (var i = 0; i < 128; i++)
                    {
                        IntPtr pUnit = IntPtr.Add(pUnitTable, i * 8);
                        WindowsExternal.ReadProcessMemory(processHandle, pUnit, addressBuffer, addressBuffer.Length, out _);
                        IntPtr aUnit = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);

                        if (aUnit != IntPtr.Zero)
                        {
                            IntPtr aPlayerUnitCheck = IntPtr.Add(aUnit, 0xB8);
                            WindowsExternal.ReadProcessMemory(processHandle, aPlayerUnitCheck, addressBuffer, addressBuffer.Length, out _);
                            long playerUnitCheck = BitConverter.ToInt64(addressBuffer, 0);
                            if (playerUnitCheck == 0x0000000000000100)
                            {
                                AdrPlayerUnit = aUnit;
                                PtrPlayerUnit = pUnit;
                                break;
                            }
                        }
                    }
                }

                WindowsExternal.ReadProcessMemory(processHandle, PtrPlayerUnit, addressBuffer, addressBuffer.Length, out _);
                AdrPlayerUnit = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr pPlayer = IntPtr.Add(AdrPlayerUnit, 0x10);
                IntPtr pAct = IntPtr.Add(AdrPlayerUnit, 0x20);
                IntPtr pPath = IntPtr.Add(AdrPlayerUnit, 0x38);

                if (AdrPlayerUnit == IntPtr.Zero)
                {
                    PtrPlayerUnit = IntPtr.Zero;
                    throw new Exception("Player pointer is zero.");
                }

                WindowsExternal.ReadProcessMemory(processHandle, pPlayer, addressBuffer, addressBuffer.Length, out _);
                IntPtr aPlayer = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);

                WindowsExternal.ReadProcessMemory(processHandle, aPlayer, stringBuffer, stringBuffer.Length, out _);
                string playerName = Encoding.ASCII.GetString(stringBuffer);

                WindowsExternal.ReadProcessMemory(processHandle, pAct, addressBuffer, addressBuffer.Length, out _);
                IntPtr aAct = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr aMapSeed = IntPtr.Add(aAct, 0x14);
                IntPtr pActUnk1 = IntPtr.Add(aAct, 0x70);

                WindowsExternal.ReadProcessMemory(processHandle, pActUnk1, addressBuffer, addressBuffer.Length, out _);
                IntPtr aActUnk2 = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr aGameDifficulty = IntPtr.Add(aActUnk2, 0x830);

                WindowsExternal.ReadProcessMemory(processHandle, aGameDifficulty, byteBuffer, byteBuffer.Length, out _);
                ushort gameDifficulty = byteBuffer[0];

                // IntPtr aDwAct = IntPtr.Add(aAct, 0x20);
                // WindowsExternal.ReadProcessMemory(processHandle, aDwAct, dwordBuffer, dwordBuffer.Length, out _);

                WindowsExternal.ReadProcessMemory(processHandle, pPath, addressBuffer, addressBuffer.Length, out _);
                IntPtr aPath = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr aPositionX = IntPtr.Add(aPath, 0x02);
                IntPtr aPositionY = IntPtr.Add(aPath, 0x06);
                IntPtr pRoom1 = IntPtr.Add(aPath, 0x20);

                WindowsExternal.ReadProcessMemory(processHandle, aPositionX, addressBuffer, addressBuffer.Length, out _);
                ushort positionX = BitConverter.ToUInt16(addressBuffer, 0);

                WindowsExternal.ReadProcessMemory(processHandle, aPositionY, addressBuffer, addressBuffer.Length, out _);
                ushort positionY = BitConverter.ToUInt16(addressBuffer, 0);

                WindowsExternal.ReadProcessMemory(processHandle, pRoom1, addressBuffer, addressBuffer.Length, out _);
                IntPtr aRoom1 = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr pRoom2 = IntPtr.Add(aRoom1, 0x18);

                WindowsExternal.ReadProcessMemory(processHandle, pRoom2, addressBuffer, addressBuffer.Length, out _);
                IntPtr aRoom2 = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr pLevel = IntPtr.Add(aRoom2, 0x90);

                WindowsExternal.ReadProcessMemory(processHandle, pLevel, addressBuffer, addressBuffer.Length, out _);
                IntPtr aLevel = (IntPtr)BitConverter.ToInt64(addressBuffer, 0);
                IntPtr aLevelId = IntPtr.Add(aLevel, 0x1F8);

                if (aLevel == IntPtr.Zero)
                {
                    throw new Exception("Level address is zero.");
                }
 
                WindowsExternal.ReadProcessMemory(processHandle, aLevelId, dwordBuffer, dwordBuffer.Length, out _);
                uint levelId = BitConverter.ToUInt32(dwordBuffer, 0);

                WindowsExternal.ReadProcessMemory(processHandle, aMapSeed, dwordBuffer, dwordBuffer.Length, out _);
                uint mapSeed = BitConverter.ToUInt32(dwordBuffer, 0);

                IntPtr aUiSettingsPath = IntPtr.Add(processAddress, Offsets.InGameMap);
                WindowsExternal.ReadProcessMemory(processHandle, aUiSettingsPath, byteBuffer, byteBuffer.Length, out _);
                bool mapShown = BitConverter.ToBoolean(byteBuffer, 0);

                return new GameData
                {
                    PlayerPosition = new Point(positionX, positionY),
                    MapSeed = mapSeed,
                    Area = (Area)levelId,
                    Difficulty = (Difficulty)gameDifficulty,
                    MapShown = mapShown,
                    MainWindowHandle = gameProcess.MainWindowHandle,
                    PlayerName = playerName
                };
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    WindowsExternal.CloseHandle(processHandle);
                }
            }
        }
    }
}
