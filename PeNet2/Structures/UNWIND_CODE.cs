﻿/***********************************************************************
Copyright 2016 Stefan Hausotte

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*************************************************************************/

using System.Text;

namespace PeNet.Structures
{
    /// <summary>
    ///     The UNWIND_CODE is a struct in
    ///     the UNWIND_INFO used to describe
    ///     exception handling in x64 applications
    ///     and to walk the stack.
    /// </summary>
    public class UNWIND_CODE
    {
        private readonly byte[] _buff;
        private readonly uint _offset;

        /// <summary>
        ///     Create a new UNWIND_INFO object.
        /// </summary>
        /// <param name="buff">A PE file as a byte array.</param>
        /// <param name="offset">Raw offset of the UNWIND_INFO.</param>
        public UNWIND_CODE(byte[] buff, uint offset)
        {
            _buff = buff;
            _offset = offset;
        }

        /// <summary>
        ///     Code offset.
        /// </summary>
        public byte CodeOffset
        {
            get { return _buff[_offset]; }
            set { _buff[_offset] = value; }
        }

        /// <summary>
        ///     Unwind operation.
        /// </summary>
        public byte UnwindOp => (byte) (_buff[_offset + 0x1] & 0xF);

        /// <summary>
        ///     Operation information.
        /// </summary>
        public byte Opinfo => (byte) (_buff[_offset + 0x1] >> 0x4);

        /// <summary>
        ///     Frame offset.
        /// </summary>
        public ushort FrameOffset
        {
            get { return Utility.BytesToUInt16(_buff, _offset + 0x2); }
            set { Utility.SetUInt16(value, _offset + 0x2, _buff); }
        }

        /// <summary>
        ///     Creates a string representation of the objects
        ///     properties.
        /// </summary>
        /// <returns>UNWIND_CODE properties as a string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder("UNWIND_CODE\n");
            sb.Append(Utility.PropertiesToString(this, "{0,-20}:\t{1,10:X}\n"));
            return sb.ToString();
        }
    }
}