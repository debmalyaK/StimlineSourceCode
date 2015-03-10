using Stimline.Data.RunDomain.Domain;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceCommon
{
    [ServiceContract]
    public interface ISimulatorWCFService
    {
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        bool UpdateSimulationConfiguration(GeneralSimulationConfiguration config);
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        List<RunPlanInfo> GetAllRunPlans();
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        bool AddRunPlan(RunPlan runPlan);
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        bool EditRunPlan(RunPlan runPlan);
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        bool DeleteRunPlan(Guid runPlanId);
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        List<RunInfo> GetAllRuns();
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        List<Run> GetAllRunsWithSpecifiedRunPlanId(Guid runPlanId);
        [OperationContract]
        [FaultContract(typeof(SimulatorException))]
        List<Run> GetAllRunsFilteredByStatus(RunStatusEnum runStatus);
    }
}
