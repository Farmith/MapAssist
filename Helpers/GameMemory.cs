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

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using MapAssist.Types;
using MapAssist.Structs;

namespace MapAssist.Helpers
{
    class GameMemory
    {
        private static readonly string ProcessName = Encoding.UTF8.GetString(new byte[] { 68, 50, 82 });
        private static IntPtr PlayerUnitPtr;
        private static UnitAny PlayerUnit = default;

        unsafe public static GameData GetGameData()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process[] process = Process.GetProcessesByName(ProcessName);
                Process gameProcess = process.Length > 0 ? process[0] : null;

                if (gameProcess == null)
                {
                    throw new Exception("Game process not found.");
                }

                processHandle =
                    WindowsExternal.OpenProcess((uint)WindowsExternal.ProcessAccessFlags.VirtualMemoryRead, false, gameProcess.Id);
                IntPtr processAddress = gameProcess.MainModule.BaseAddress;

                if (Equals(PlayerUnit, default(UnitAny)))
                {
                    var unitHashTable = Read<UnitHashTable>(processHandle, IntPtr.Add(processAddress, Offsets.UnitHashTable));
                    foreach(var pUnitAny in unitHashTable.UnitTable)
                    {
                        var pListNext = pUnitAny;

                        while(pListNext != IntPtr.Zero)
                        {
                            var unitAny = Read<UnitAny>(processHandle, pListNext);
                            if (unitAny.OwnerType == 256) // 0x100
                            {
                                PlayerUnitPtr = pUnitAny;
                                PlayerUnit = unitAny;
                                break;
                            }
                            pListNext = (IntPtr)unitAny.pListNext;
                        }

                        if (PlayerUnitPtr != IntPtr.Zero)
                        {
                            break;
                        }
                    }
                }

                if (PlayerUnitPtr == IntPtr.Zero)
                {
                    throw new Exception("Player pointer is zero.");
                }

                IntPtr aPlayerUnit = Read<IntPtr>(processHandle, PlayerUnitPtr); 
    
                if (aPlayerUnit == IntPtr.Zero)
                {
                    throw new Exception("Player address is zero.");
                }

                var playerName = Encoding.ASCII.GetString(Read<byte>(processHandle, PlayerUnit.UnitData, 16)).TrimEnd((char)0);
                var act = Read<Act>(processHandle, (IntPtr)PlayerUnit.pAct);
                var mapSeed = act.MapSeed;

                if (mapSeed <= 0 || mapSeed > 0xFFFFFFFF)
                {
                    throw new Exception("Map seed is out of bounds.");
                }

                var actId = act.ActId;
                var actMisc = Read<ActMisc>(processHandle, (IntPtr)act.ActMisc);
                var gameDifficulty = actMisc.GameDifficulty;

                if (!gameDifficulty.IsValid())
                {
                    throw new Exception("Game difficulty out of bounds.");
                }

                var path = Read<Path>(processHandle, (IntPtr)PlayerUnit.pPath);
                var positionX = path.DynamicX;
                var positionY = path.DynamicY;
                var room = Read<Room>(processHandle, (IntPtr)path.pRoom);
                var roomEx = Read<RoomEx>(processHandle, (IntPtr)room.pRoomEx);
                var level = Read<Level>(processHandle, (IntPtr)roomEx.pLevel);
                var levelId = level.LevelId;

                if (!levelId.IsValid())
                {
                    throw new Exception("Level id out of bounds.");
                }

                var mapShown = Read<UiSettings>(processHandle, IntPtr.Add(processAddress, Offsets.UiSettings)).MapShown;

                return new GameData
                {
                    PlayerPosition = new Point(positionX, positionY),
                    MapSeed = mapSeed,
                    Area = levelId,
                    Difficulty = gameDifficulty,
                    MapShown = mapShown,
                    MainWindowHandle = gameProcess.MainWindowHandle
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                PlayerUnit = default;
                PlayerUnitPtr = IntPtr.Zero;
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

        public static T[] Read<T>(IntPtr processHandle, IntPtr address, int count) where T : struct
        {
            var sz = Marshal.SizeOf<T>();
            var buf = new byte[sz * count];
            WindowsExternal.ReadProcessMemory(processHandle, address, buf, buf.Length, out _);

            var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            try
            {
                var result = new T[count];
                for (var i = 0; i < count; i++)
                {
                    result[i] = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject() + (i * sz), typeof(T));
                }

                return result;
            }
            finally
            {
                handle.Free();
            }
        }

        public static T Read<T>(IntPtr processHandle, IntPtr address) where T : struct
        {
            return Read<T>(processHandle, address, 1)[0];
        }
    }
}

