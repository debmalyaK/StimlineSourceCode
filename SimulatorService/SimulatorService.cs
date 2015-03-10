using ServiceCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorService
{
    public partial class SimulatorService : ServiceBase
    {
        ServiceHost m_host = null;

        public SimulatorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (m_host == null)
            {
                m_host = new ServiceHost(typeof(SimulatorWCFService));
            }
            m_host.Open();
        }

        protected override void OnStop()
        {
            if (m_host != null && m_host.State != CommunicationState.Closed)
            {
                m_host.Close();
            }
        }
    }
}
