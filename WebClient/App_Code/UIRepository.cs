using ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UIRepository
/// </summary>
public class UIRepository
{
    private static UIRepository m_Instance;
    private static object m_Lock = new object();
    private List<RunPlanInfo> m_AllRunPlans;
    private List<RunInfo> m_AllRuns;
    public bool m_NeedToRefreshUICache;
    private SimulatorWCFServiceClient m_Service;

    private UIRepository()
    {
        m_NeedToRefreshUICache = true;
    }

    public static UIRepository Instance
    {
        get
        {
            if (m_Instance == null)
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new UIRepository();
                    }
                }
            }
            return m_Instance;
        }
    }

    public bool NeedToRefreshUICache
    {
        get { return m_NeedToRefreshUICache; }
        set { m_NeedToRefreshUICache = value; }
    }

    public List<RunPlanInfo> AllRunPlans
    {
        get { return m_AllRunPlans; }
        set { m_AllRunPlans = value; }
    }

    public List<RunInfo> AllRuns
    {
        get { return m_AllRuns; }
        set { m_AllRuns = value; }
    }

    public void FetchInitialDataFromServer()
    {
        try
        {
            m_Service = new SimulatorWCFServiceClient();
            m_AllRunPlans = m_Service.GetAllRunPlans().ToList();
            m_AllRuns = m_Service.GetAllRuns().ToList();
        }
        catch (Exception ex)
        {

        }
    }
}