﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.BitConverter;

namespace IrccServerTemp
{
    public struct Header
    {
        public short comm;
        public short code;
        public int size;
        public int reserved;

        public Header(short comm, short code, int size, int reserved)
        {
            this.comm = comm;
            this.code = code;
            this.size = size;
            this.reserved = reserved;
        }
    }
    public struct Packet
    {
        public Header header;
        public byte[] data;

        public Packet(Header header, byte[] data)
        {
            this.header = header;
            this.data = data;
        }
    }
    public static class IrccHelper
    {
        public const int HEADER_SIZE = 12;        

        public class FieldIndex
        {
            public const int COMM = 0;
            public const int CODE = 2;
            public const int SIZE = 4;
            public const int RSVD = 8;
            public const int DATA = 12;
        }

        public class Comm
        {
            public const int CS = 0;
            public const int SS = 1;
            public const int DUMMY = 3;
        }

        public class Code
        {
            // * -> placeholder code for future
            public const short FAIL = 000;
            public const short SUCCESS = 100;

            public const short MSG = 200; // normal msg
            public const short MSG_ERR = 205; // *
            public const short SMSG = 250; // server echo
            public const short SMSG_ERR = 255; // *

            public const short SIGNUP = 310; // signup req
            public const short SIGNUP_RES = 312; // signup res
            public const short SIGNUP_ERR = 315; // signup fail (duplicate name)

            public const short LOGIN = 320; // login req
            public const short LOGIN_RES = 322; // login res
            public const short LOGIN_ERR = 325; // no such name; name & pass mismatch

            public const short LIST = 400; // room list req
            public const short LIST_RES = 402; // room list res
            public const short LIST_ERR = 405; // *
            public const short SLIST = 450; // server list req
            public const short SLIST_RES = 452; // server list res
            public const short SLIST_ERR = 455; // *

            public const short JOIN = 500; // join room req
            public const short JOIN_FULL_ERR = 505;// target room full
            public const short JOIN_NULL_ERR = 510;// target room does not exist
            public const short SJOIN = 550; // server join room's echo list req
            public const short SJOIN_ERR = 555; // *

            public const short LEAVE = 600; // leave room req
            public const short LEAVE_ERR = 605; // not valid room / not in target room

            public const short CREATE = 700; // create room req
            public const short CREATE_DUPLICATE_ERR = 705; // room already exists
            public const short CREATE_FULL_ERR = 710; // room is full

            public const short HEARTBEAT = 800; // heartbeat
            public const short HEARTBEAT_RES = 802; // heartbeat res

            public const short DESTROY = 900; // destory room req
            public const short DESTROY_ERR = 905; // *
            public const short SDESTROY = 950; // server destroy room req
            public const short SDESTROY_ERR = 955; // *
        }

        public static byte[] packetToBytes(Packet p)
        {
            /*
            byte[] msg = new byte[2 + 2 + 4 + 4 + p.data.Length];
            Array.Copy(GetBytes(p.header.comm), 0, msg, FieldIndex.COMM, 2);
            Array.Copy(GetBytes(p.header.code), 0, msg, FieldIndex.CODE, 2);
            Array.Copy(GetBytes(p.header.size), 0, msg, FieldIndex.SIZE, 4);
            Array.Copy(GetBytes(p.header.reserved), 0, msg, FieldIndex.RSVD, 4);
            Array.Copy(p.data, 0, msg, FieldIndex.DATA, p.data.size);
            return msg;
            */

            byte[] bComm = GetBytes(p.header.comm);
            byte[] bCode = GetBytes(p.header.code);
            byte[] bSize = GetBytes(p.header.size);
            byte[] bRsvd = GetBytes(p.header.reserved);

            if (null == p.data)
                return bComm.Concat(bCode).Concat(bSize).Concat(bRsvd).ToArray();

            return bComm.Concat(bCode).Concat(bSize).Concat(bRsvd).Concat(p.data).ToArray();
        }

        public static Header bytesToHeader(byte[] b)
        {
            Header h = new Header();

            h.comm = ToInt16(b, FieldIndex.COMM);
            h.code = ToInt16(b, FieldIndex.CODE);
            h.size = ToInt32(b, FieldIndex.SIZE);
            h.reserved = ToInt32(b, FieldIndex.RSVD);

            return h;
        }
        public static Packet bytesToPacket(byte[] b)
        {
            Packet p = new Packet();

            p.header.comm = ToInt16(b, FieldIndex.COMM);
            p.header.code = ToInt16(b, FieldIndex.CODE);
            p.header.size = ToInt32(b, FieldIndex.SIZE);
            p.header.reserved = ToInt32(b, FieldIndex.RSVD);
            Array.Copy(b, FieldIndex.DATA, p.data, 0, p.header.size);

            return p;
        }


        public static byte[] CreateBytesPacket(short comm, short code, byte[] bMsg)
        {
            Header header;
            header.comm = comm;
            header.code = code;
            header.size = bMsg.Length;
            header.reserved = 0;

            Packet packet;
            packet.header = header;
            packet.data = bMsg;

            //you can send this packet bytes
            //using socket.send(CreateBytesPacket(comm, code, bytesMsg));
            return packetToBytes(packet);
        }

        public static byte[] CreateBytesPacket(short comm, short code, int reserv, byte[] bMsg)
        {
            Header header;
            header.comm = comm;
            header.code = code;
            header.size = bMsg.Length;
            header.reserved = reserv;

            Packet packet;
            packet.header = header;
            packet.data = bMsg;

            //you can send this packet bytes
            //using socket.send(CreateBytesPacket(comm, code, reserved, bytesMsg));
            return packetToBytes(packet);
        }
        public static byte[] CreateBytesPacket(short comm, short code)
        {
            Header header;
            header.comm = comm;
            header.code = code;
            header.size = 0;
            header.reserved = 0;

            Packet packet;
            packet.header = header;
            packet.data = null;

            //you can send this packet bytes
            //using socket.send(CreateBytesPacket(comm, code));
            return packetToBytes(packet);
        }
    }
}