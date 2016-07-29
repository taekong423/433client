 byte[] LoginStrToByte(string name, string pass)
        {
            byte[] bClientName = new byte[12];
            byte[] bPassword = new byte[18];

            Encoding.UTF8.GetBytes(name, 0, name.Length, bClientName, 0);
            Encoding.UTF8.GetBytes(pass, 0, pass.Length, bPassword, 0);

            byte[] bmsg = bClientName.Concat(bPassword).ToArray();

            return bmsg;
        }

       

        void SendMsg(byte[] bMsg, short comm, short code)
        {
            //CHECK: have to functionalize
            Header header;
            header.comm = comm;
            header.code = code;
            header.size = bMsg.Length;
            header.reserved = 0;

            Packet packet;
            packet.header = header;
            packet.data = bMsg;            

            //send start
            socket.Send(IrccHelper.packetToBytes(packet));
        }

        void SendMsg(byte[] bMsg, short comm, short code, int reserv)
        {
            //CHECK: have to functionalize
            Header header;
            header.comm = comm;
            header.code = code;
            header.size = bMsg.Length;
            header.reserved = reserv;

            Packet packet;
            packet.header = header;
            packet.data = bMsg;

            //send start
            socket.Send(IrccHelper.packetToBytes(packet));
        }
