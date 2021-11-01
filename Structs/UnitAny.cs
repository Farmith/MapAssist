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
using System.Runtime.InteropServices;

namespace MapAssist.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct UnitAny
    {
        [FieldOffset(0x00)] public uint UnitType;
        [FieldOffset(0x04)] public uint TxtFileNo;
        [FieldOffset(0x08)] public uint UnitId;
        [FieldOffset(0x0C)] public uint Mode;
        [FieldOffset(0x10)] public IntPtr UnitData;
        [FieldOffset(0x10)] public IntPtr IsPlayer;
        [FieldOffset(0x20)] public Act* pAct;
        [FieldOffset(0x38)] public Path* pPath;
        [FieldOffset(0x88)] public IntPtr StatsListEx;
        [FieldOffset(0x90)] public IntPtr Inventory;
        [FieldOffset(0xB8)] public uint OwnerType; // ?
        [FieldOffset(0xC4)] public ushort X;
        [FieldOffset(0xC6)] public ushort Y;
        [FieldOffset(0x150)] public UnitAny* pListNext;
        [FieldOffset(0x158)] public UnitAny* pRoomNext;

        public override bool Equals(object obj) => obj is UnitAny other && Equals(other);

        public bool Equals(UnitAny unit) => UnitId == unit.UnitId;

        public override int GetHashCode() => UnitId.GetHashCode();

        public static bool operator ==(UnitAny unit1, UnitAny unit2) => unit1.Equals(unit2);

        public static bool operator !=(UnitAny unit1, UnitAny unit2) => !(unit1 == unit2);
    }
}
