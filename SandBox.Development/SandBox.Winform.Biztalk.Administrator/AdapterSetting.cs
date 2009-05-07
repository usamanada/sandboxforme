using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SandBox.dll.Winform.Components;

namespace SandBox.Winform.Biztalk.Administrator
{
    public enum Direction { receive, send };

    [TypeConverter(typeof(PropertySorter))]
    [DefaultPropertyAttribute("Name")]
    public class AdapterSetting
    {
        private uint mConstraints;
        [CategoryAttribute("BizTalk Constraint"),
         DescriptionAttribute("Value retrieved from BizTalk."),
         ReadOnlyAttribute(true)]
        public uint Constraints
        {
            get { return mConstraints; }
            set
            {
                mConstraints = value;
                mReceiving = (value & 1 ) == 1? true : false;
                mSending = (value & 2 ) == 2? true : false;
                mInProcess = (value & 8 ) == 8? true : false;
                mRequestResponse = (value & 128 ) == 128? true : false;
                mSolicitResponse = (value & 256 ) == 256? true : false;
                mAdapterFrameworkSendHandlerInterface = (value & 1024 ) == 1024? true : false;
                mAdapterFrameworkReceiveHandlerInterface = (value & 2048 ) == 2048? true : false;
                mAdapterFrameworkReceiveLocationInterface = (value & 4096 ) == 4096? true : false;
                mAdapterFrameworkSendPortnterface = (value & 8192 ) == 8192? true : false;
                mOrdererDeliveryMessages = (value & 16384 ) == 16384? true : false;
                mTransmitterStartedWithHostInstance = (value & 32768 ) == 32768? true : false;
                mRunning32bitOnly = (value & 65536) == 65536? true : false;        
            }
        }

        private string mName;
        [BrowsableAttribute(false)]
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }
        #region Seperated Contraints
        //1
        bool mReceiving;
        [CategoryAttribute("Constraints"), 
         DescriptionAttribute("Indicates that the adapter supports receiving messages."), 
         ReadOnlyAttribute(true), PropertyOrder(1)]
        public bool Receiving
        {
            get
            {
                return mReceiving;
            }
            set
            {
                mReceiving = value;
            }
        }
        
        //2
        bool mSending;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter supports sending of messages."),
        ReadOnlyAttribute(true), PropertyOrder(2)]
        public bool Sending
        {
            get
            {
                return mSending;
            }
            set
            {
                mSending = value;
            }
        }

        //8
        bool mInProcess;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates which type of hosts can be receive handlers of the adapter. If bit is ON, then only In-Process hosts can be receive handlers of the adapter. If bit is OFF, then only Isolated hosts can be receive handlers of the adapter. This constraint only applies to the receive side. Send handlers are always created with In-Process hosts according to the BizTalk architecture."),
        ReadOnlyAttribute(true), PropertyOrder(3)]
        public bool InProcess
        {
            get
            {
                return mInProcess;
            }
            set
            {
                mInProcess = value;
            }
        }

        //128 
        bool mRequestResponse;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter supports request-response message exchanges."),
         ReadOnlyAttribute(true), PropertyOrder(4)]
        public bool RequestResponse
        {
            get
            {
                return mRequestResponse;
            }
            set
            {
                mRequestResponse = value;
            }
        }
        
        //256 .
        bool mSolicitResponse;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter supports solicit-response message exchanges."),
         ReadOnlyAttribute(true), PropertyOrder(5)]
        public bool SolicitResponse
        {
            get
            {
                return mSolicitResponse;
            }
            set
            {
                mSolicitResponse = value;
            }
        }

        //1024 .
        bool mAdapterFrameworkSendHandlerInterface;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter uses the Adapter Framework user interface for send handler configuration."),
         ReadOnlyAttribute(true), PropertyOrder(6)]
        public bool AdapterFrameworkSendHandlerInterface
        {
            get
            {
                return mAdapterFrameworkSendHandlerInterface;
            }
            set
            {
                mAdapterFrameworkSendHandlerInterface = value;
            }
        }

        //2048 
        bool mAdapterFrameworkReceiveHandlerInterface;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter uses adapter framework user interface for receive handler configuration."),
         ReadOnlyAttribute(true), PropertyOrder(7)]
        public bool AdapterFrameworkReceiveHandlerInterface
        {
            get
            {
                return mAdapterFrameworkReceiveHandlerInterface;
            }
            set
            {
                mAdapterFrameworkReceiveHandlerInterface = value;
            }
        }

        //4096 
        bool mAdapterFrameworkReceiveLocationInterface;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter uses adapter framework user interface for receive location configuration."),
         ReadOnlyAttribute(true), PropertyOrder(8)]
        public bool AdapterFrameworkReceiveLocationInterface
        {
            get
            {
                return mAdapterFrameworkReceiveLocationInterface;
            }
            set
            {
                mAdapterFrameworkReceiveLocationInterface = value;
            }
        }

        //8192 
        bool mAdapterFrameworkSendPortnterface;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter uses adapter framework user interface for send port configuration."),
         ReadOnlyAttribute(true), PropertyOrder(9)]
        public bool AdapterFrameworkSendPortnterface
        {
            get
            {
                return mAdapterFrameworkSendPortnterface;
            }
            set
            {
                mAdapterFrameworkSendPortnterface = value;
            }
        }

        //16384  
        bool mOrdererDeliveryMessages;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the adapter supports ordered delivery of messages."),
         ReadOnlyAttribute(true), PropertyOrder(10)]
        public bool OrdererDeliveryMessages
        {
            get
            {
                return mOrdererDeliveryMessages;
            }
            set
            {
                mOrdererDeliveryMessages = value;
            }
        }

        //32768
        bool mTransmitterStartedWithHostInstance;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that the transmitter component of the adapter will be initialized when the BizTalk runtime service (host instance) starts instead of when the first message is sent."),
         ReadOnlyAttribute(true), PropertyOrder(11)]
        public bool TransmitterStartedWithHostInstance
        {
            get
            {
                return mTransmitterStartedWithHostInstance;
            }
            set
            {
                mTransmitterStartedWithHostInstance = value;
            }
        }

        //65536
        bool mRunning32bitOnly;
        [CategoryAttribute("Constraints"),
         DescriptionAttribute("Indicates that adapter supports running only in 32bit hosts."),
         ReadOnlyAttribute(true), PropertyOrder(12)]
        public bool Running32bitOnly
        {
            get
            {
                return mRunning32bitOnly;
            }
            set
            {
                mRunning32bitOnly = value;
            }
        }
        #endregion

        public bool AvaliableToHost(Host h, Direction direction)
        {
            if (direction == Direction.send)
            {
                if (!mSending)
                    return false;
            }
            else
            {
                if (!mReceiving)
                    return false;
            }
            if (!mInProcess && h.Type == Constants.TYPE_INPROCESS)
                return false;
            if (mInProcess && h.Type == Constants.TYPE_ISOLATED)
                return false;
            if (mRunning32bitOnly && !h.ThirtyTwoBitOnly)
                return false;

            return true;
        }
    }
}
